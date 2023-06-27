using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.AspNetCore.Connections;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Serilog;

namespace SteamTradeBot.Backend.BusinessLogicLayer;

public class SteamAPI : IDisposable
{
    private readonly IWebDriver _chromeBrowser;
    private bool _logState;
    private const int DefaultImplicitWaitTime = 5;
    private const int RequestDelayMs = 5000;
    private const int RetryWaitTimeMs = 2000;
    private const int RetriesCount = 5;
    private readonly string _webDriverHost = Environment.GetEnvironmentVariable("SELENIUM_HOST") ?? "http://localhost:5051";

    public SteamAPI()
    {
        var chromeOptions = new ChromeOptions();
        chromeOptions.AddArgument("--disable-gpu");
        chromeOptions.AddArgument("--no-sandbox");
        chromeOptions.AddArgument("--disable-setuid-sandbox");
        chromeOptions.AddArgument("--disable-dev-shm-usage");
        chromeOptions.AddArgument("--start-maximized");
        chromeOptions.AddArgument("--window-size=1920,1080");
        chromeOptions.AddArgument("--headless");
        //_chromeBrowser = new RemoteWebDriver(new Uri(_webDriverHost), chromeOptions.ToCapabilities());
        _chromeBrowser = new ChromeDriver(chromeOptions);
        Log.Logger.Information("Steam Api created!");
    }

    #region GetToken

    public string GetToken(string secret)
    {
        return SafeConnect(() =>
        {
            SetPage("https://www.chescos.me/js-steam-authcode-generator/?");
            SendKey(By.Id("secret"), secret);
            ClickOnElement(By.Id("generate"));
            return ReadFromElement(By.Id("result"));
        }, true);
    }

    #endregion

    #region Balance

    public double GetBalance(string itemName)
    {
        return SafeConnect(() =>
        {
            SetPage(itemName);
            return ParsePrice(ReadFromElement(By.Id("header_wallet_balance"), true));
        });
    }

    #endregion

    #region OrderBooks

    public List<OrderBookItem> GetBuyOrdersBook(string itemUrl)
    {
        const int buyListingPageSize = 8;
        return SafeConnect(() =>
        {
            SetPage(itemUrl);
            var orderBook = new List<OrderBookItem>();

            ClickOnElement(By.CssSelector("#market_buyorder_info_show_details > span"));

            for (var itemIdx = 2; itemIdx < buyListingPageSize; itemIdx++)
            {
                var price = ParsePrice(ReadFromElement(By.CssSelector($"#market_commodity_buyreqeusts_table > table > tbody > tr:nth-child({itemIdx}) > td:nth-child(1)")));
                var quantity = int.Parse(ReadFromElement(By.CssSelector($"#market_commodity_buyreqeusts_table > table > tbody > tr:nth-child({itemIdx}) > td:nth-child(2)")));
                orderBook.Add(new OrderBookItem { Price = price, Quantity = quantity });
            }
            return orderBook.OrderBy(x => x.Price).ToList();
        });
    }

    public List<OrderBookItem> GetSellOrdersBook(string itemUrl, int listingFindRange)
    {
        const int sellListingPageSize = 10;
        SafeConnect(() =>
        {
            SetPage(itemUrl);
            return true;
        });

        var orderBook = new List<OrderBookItem>();
        for (var pageIdx = 1; pageIdx <= listingFindRange; pageIdx++)
        {
            for (var itemIdx = 0; itemIdx < sellListingPageSize; itemIdx++)
            {
                var sellPriceStr = SafeConnect(() => ReadFromElement(By.XPath($"//*[@id='searchResultsRows']/div[{itemIdx + 2}]/div[2]/div[2]/span[1]/span[1]")));
                var price = ParsePrice(sellPriceStr);
                if (price == 0)
                    continue;

                if (orderBook.Any(x => Math.Abs(x.Price - price) < 0.01))
                    orderBook.First(x => Math.Abs(x.Price - price) < 0.01).Quantity += 1;
                else
                    orderBook.Add(new OrderBookItem { Price = price, Quantity = 1 });
            }
            SafeConnect(() =>
            {
                ClickOnElement(By.XPath($"//*[@id='searchResults_links']/span[{pageIdx + 1}]"));
                return true;
            }, true);
        }
        return orderBook.OrderBy(x => x.Price).ToList();
    }

    private const string CurrencyName = "руб";
    private static double ParsePrice(string priceStr)
    {
        if (string.IsNullOrEmpty(priceStr))
            return 0;
        if (!priceStr.Contains(CurrencyName))
            return 0;
        priceStr = string.Join("", priceStr.SkipWhile(x => !char.IsDigit(x)).TakeWhile(x => !char.IsWhiteSpace(x)));
        return double.TryParse(priceStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var result) ? result : 0;
    }

    public class OrderBookItem
    {
        public double Price { get; init; }
        public int Quantity { get; set; }
    }

    #endregion

    #region Graph

    public IEnumerable<PointInfo> GetGraph(string itemUrl)
    {
        if (!SafeConnect(() =>
        {
            SetPage(itemUrl);
            ClickOnElement(By.CssSelector("#mainContents > div.market_page_fullwidth.market_listing_firstsection > div > div.zoom_controls.pricehistory_zoom_controls > a:nth-child(3)"));
            return true;
        })) return new List<PointInfo>();

        return string.Join("", _chromeBrowser.PageSource
                 .Split("\n")
                 .First(x => x.StartsWith("\t\t\tvar line1="))
                 .SkipWhile(x => x != '['))
             .Split(",")
             .Select(DeleteServiceCharacters)
             .Chunk(3)
             .Select(x => new PointInfo
             {
                 Date = DateTime.ParseExact(x[0], "MMM dd yyyy HH: z", CultureInfo.InvariantCulture),
                 Price = double.Parse(x[1], NumberStyles.Any, CultureInfo.InvariantCulture),
                 Quantity = int.Parse(x[2])
             });
    }

    private static string DeleteServiceCharacters(string line)
    {
        return line.Replace("\"", "").Replace("\r", "").Replace("\t", "").Replace(";", "").TrimStart('[').TrimEnd(']');
    }

    public class PointInfo
    {
        public DateTime Date { get; init; }
        public double Price { get; init; }
        public int Quantity { get; init; }
    }

    #endregion

    #region SellOrder

    public bool PlaceSellOrder(string itemNeedToSell, double price, string userId, int inventoryFindRange = 10)
    {
        return SafeConnect(() =>
        {
            SetPage($"https://steamcommunity.com/profiles/{userId}/inventory/#730");
            for (var i = 0; i < inventoryFindRange; i++)
            {
                ClickOnElement(By.XPath($"//*[@id='inventory_{userId}_730_2']/div[1]/div[{i + 1}]"));
                var itemName = ReadFromElement(By.Id("iteminfo0_item_name")).Trim();
                var itemQuality = string.Join("", ReadFromElement(By.XPath("//*[@id='iteminfo0_item_descriptors']/div[1]")).SkipWhile(x => x != ':').Skip(2)).Trim();
                var fullItemName = $"{itemName} ({itemQuality})";
                if (itemNeedToSell != fullItemName) continue;
                ExecuteJs("scroll(0, 20000000);");
                ClickOnElement(By.XPath("//*[@class='inventory_page_right']/div[2]/div[3]/div/a"));
                SendKey(By.Id("market_sell_buyercurrency_input"), $"\b\b\b\b\b{price}");
                ClickOnElement(By.Id("market_sell_dialog_accept_ssa"));
                ClickOnElement(By.Id("market_sell_dialog_accept"));
                ClickOnElement(By.Id("market_sell_dialog_ok"), true, 2);
                return true;
            }
            return false;
        });
    }

    public bool CancelSellOrder(string itemUrl)
    {
        return SafeConnect(() =>
        {
            SetPage(itemUrl);
            ClickOnElement(By.XPath("/html/body/div[1]/div[7]/div[2]/div[1]/div[4]/div[1]/div[2]/div/div[5]/div/div/div[2]/div/div[5]/div/a"));
            ClickOnElement(By.XPath("//*[@id='market_removelisting_dialog_accept']"));
            return true;
        });
    }

    #endregion

    #region BuyOrder

    public string GetRusItemName(string itemUrl)
    {
        return SafeConnect(() =>
        {
            SetPage(itemUrl);
            var itemName = ReadFromElement(By.Id("largeiteminfo_item_name"));
            var quality = string.Join("", ReadFromElement(By.XPath("//*[@id='largeiteminfo_item_descriptors']/div[1]")).SkipWhile(x => x != ':').Skip(2));
            return $"{itemName} ({quality})";
        });
    }

    public bool PlaceBuyOrder(string itemUrl, double price, int quantity)
    {
        return SafeConnect(() =>
        {
            SetPage(itemUrl);
            ClickOnElement(By.XPath("//*[@id='market_buyorder_info']/div/div/a"));
            SendKey(By.Id("market_buy_commodity_input_price"), $"\b\b\b\b\b\b\b\b\b\b\b{Math.Round(price + 0.01, 2, MidpointRounding.AwayFromZero)}");
            SendKey(By.Name("input_quantity"), $"\b{quantity}");
            ClickOnElement(By.Id("market_buyorder_dialog_accept_ssa"));
            ClickOnElement(By.Id("market_buyorder_dialog_purchase"));
            return true;
        }, true);
    }

    public int GetBuyOrderQuantity(string itemUrl)
    {
        return SafeConnect(() =>
        {
            SetPage(itemUrl);
            var quantity = int.Parse(ReadFromElement(By.XPath("/html/body/div[1]/div[7]/div[2]/div[1]/div[4]/div[1]/div[2]/div/div[5]/div/div/div[2]/div[3]/span/span"), true));
            return quantity;
        });
    }

    public bool CancelBuyOrder(string itemUrl)
    {
        return SafeConnect(() =>
        {
            SetPage(itemUrl);
            ClickOnElement(By.XPath("/html/body/div[1]/div[7]/div[2]/div[1]/div[4]/div[1]/div[2]/div/div[5]/div/div/div[2]/div[5]/div/a/span[2]"));
            return true;
        }, true);
    }

    #endregion

    #region RefreshWorkingSet

    public List<string> GetItemNamesList(double startPrice, double endPrice, double salesVolumeByWeek, int listSize)
    {
        return SafeConnect(() =>
        {
            var result = new List<string>();
            SetPage("https://skins-table.xyz/");
            ClickOnElement(By.XPath("/html/body/div[1]/div[4]/div/div/center/div/div/div[2]/form/div[2]/a"));
            ClickOnElement(By.Id("imageLogin"));

            SetPage("https://skins-table.xyz/table/");

            ClickOnElement(By.CssSelector("#scroll > div > div.sites.first > div:nth-child(30)"));
            ClickOnElement(By.CssSelector("#scroll > div > div.sites.second > div:nth-child(29)"));
            SendKey(By.Id("price1_from"), $"\b\b\b\b\b\b\b{startPrice}");
            SendKey(By.Id("price1_to"), $"\b\b\b\b\b\b\b{endPrice}");
            SendKey(By.Id("price2_from"), $"\b\b\b\b\b\b\b{startPrice}");
            SendKey(By.Id("price2_to"), $"\b\b\b\b\b\b\b{endPrice}");
            SendKey(By.Id("sc1"), $"\b\b\b\b{salesVolumeByWeek}");
            ClickOnElement(By.Id("change1"));

            var js = _chromeBrowser as IJavaScriptExecutor;
            Thread.Sleep(15000);
            for (var i = 1; i < 10; i++)
            {
                js?.ExecuteScript("scroll(0, 20000000);");
            }

            var htmlCode = _chromeBrowser.PageSource;
            var regex = new Regex(@"data-clipboard-text=\p{P}[^\p{P}]*\p{P}?[^\p{P}]*\p{P}?[^\p{P}]*\p{P}?[^\p{P}]*\p{P}[^\p{P}]*\p{P}?[^\p{P}]*\p{P}\p{P}");
            var matches = regex.Matches(htmlCode);

            if (matches.Count <= 0)
                return new List<string>();

            for (var i = 0; i < listSize; i++)
            {
                var itemName = string.Join("", matches[i].ToString().SkipWhile(x => x != '=').Skip(2).SkipLast(1));
                if (itemName.Contains("Sealed Graffiti") || itemName.Contains("Sticker") || itemName.Contains("Case"))
                    continue;
                result.Add(itemName);
            }
            return result;
        });
    }

    public string GetItemUrl(string itemName)
    {
        return SafeConnect(() =>
        {
            _chromeBrowser.Navigate().GoToUrl($"https://steamcommunity.com/market/listings/730/{itemName}");
            return _chromeBrowser.Url;
        }, true);
    }

    #endregion

    #region LogIn

    #region LoginLocators

    private static readonly By LoginField = By.XPath("//*[@id='responsive_page_template_content']/div[1]/div[1]/div/div/div/div[2]/div/form/div[1]/input");
    private static readonly By PasswordField = By.XPath("//*[@id='responsive_page_template_content']/div[1]/div[1]/div/div/div/div[2]/div/form/div[2]/input");
    private static readonly By LoginButton = By.XPath("//*[@id='responsive_page_template_content']/div[1]/div[1]/div/div/div/div[2]/div/form/div[4]/button");
    private static readonly By TwoFactorField = By.XPath("//*[@id='responsive_page_template_content']/div[1]/div[1]/div/div/div/div[2]/form/div/div[2]/div[1]/div/input[1]");
    private static readonly By LoginCheck = By.Id("account_pulldown");

    #endregion

    public void LogIn(string login, string password, string token, string secret)
    {
        Log.Information("Signing in...");
        Log.Information($"Incoming user data {login}, {password}, {token}");
        //if (CheckIncomingData(login, password, token))
        //{
        //    Log.Error("Authorization failed. Fields was null or empty.");
        //    throw new ArgumentException("Authorization failed. Fields was null or empty.");
        //}

        token = GetToken(secret);

        if (_logState)
        {
            Log.Error("Authorization failed. You're already logged in.");
            throw new AuthenticationException("Authorization failed. You're already logged in.");
        }

        SafeConnect(() =>
        {
            SetPage("https://steamcommunity.com/login/home/?goto=");
            WriteToElement(LoginField, login);
            WriteToElement(PasswordField, password);
            ClickOnElement(LoginButton);
            return true;
        }, true);

        try
        {
            WriteToElement(TwoFactorField, token, true);
        }
        catch
        {
            Log.Error("Authorization failed. Login or password are incorrect.");
            throw new AuthenticationException("Authorization failed. Login or password are incorrect.");
        }

        if (!IsAuthenticationSuccessful())
        {
            Log.Error("Authorization failed. User data are incorrect.");
            throw new AuthenticationException("Authorization failed. User data are incorrect.");
        }
        Log.Information("Authentication completed successful");
        _logState = true;
    }

    private static bool CheckIncomingData(string login, string password, string token)
    {
        Log.Information("Check incoming data...");
        return string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(token);
    }

    private bool IsAuthenticationSuccessful()
    {
        Log.Information("Checking if authentication successful...");
        try
        {
            ReadFromElement(LoginCheck, true);
            Log.Information("Successfully authenticated");
            return true;
        }
        catch
        {
            Log.Information("Authentication failed");
            return false;
        }
    }

    public void LogOut()
    {
        Log.Information("Signing out...");
        _logState = false;
        ExecuteJs("javascript:Logout()");
        Log.Information("Successful sign out");
    }

    #endregion

    #region SeleniumMethods

    private void WriteToElement(By by, string message, bool explicitly = false, int waitTime = 5)
    {
        if (explicitly)
        {
            ExplicitWait(by, waitTime).SendKeys(message);
            return;
        }
        ImplicitWait(by, DefaultImplicitWaitTime).SendKeys(message);
    }
    private string ReadFromElement(By by, bool explicitly = false, int waitTime = 5)
    {
        return explicitly ? ExplicitWait(by, waitTime).Text : ImplicitWait(by, DefaultImplicitWaitTime).Text;
    }
    private void ClickOnElement(By by, bool explicitly = false, int waitTime = 5)
    {
        if (explicitly)
        {
            ExplicitWait(by, waitTime).Click();
            return;
        }
        ImplicitWait(by, DefaultImplicitWaitTime).Click();
    }
    private void SendKey(By by, string key, bool explicitly = false, int waitTime = 5)
    {
        if (explicitly)
        {
            ExplicitWait(by, waitTime).SendKeys(key);
            return;
        }
        ImplicitWait(by, DefaultImplicitWaitTime).SendKeys(key);
    }

    private IWebElement ExplicitWait(By by, int waitTime)
    {
        _chromeBrowser.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
        return new WebDriverWait(_chromeBrowser, TimeSpan.FromSeconds(waitTime)).Until(ExpectedConditions.ElementIsVisible(by));
    }
    private IWebElement ImplicitWait(By by, int waitTime)
    {
        _chromeBrowser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(waitTime);
        return _chromeBrowser.FindElement(by);
    }

    private void SetPage(string url)
    {
        if (_chromeBrowser.Url != url)
            _chromeBrowser.Navigate().GoToUrl(url);
    }

    private void ExecuteJs(string script)
    {
        ((IJavaScriptExecutor)_chromeBrowser).ExecuteScript(script);
    }

    #endregion

    private T SafeConnect<T>(Func<T> unsafeFunc, bool isDelayNeeded = false)
    {
        var retryWaitTimeMs = isDelayNeeded ? RequestDelayMs : 0;
        for (var attempt = 0; attempt < RetriesCount; attempt++)
        {
            try
            {
                Thread.Sleep(retryWaitTimeMs);
                return unsafeFunc();
            }
            catch (Exception e)
            {
                Log.Error($"Connection error! Attempt: {attempt + 1}/{RetriesCount}\r\nMessage: {e.Message}\r\nStack trace: {e.StackTrace}");
                retryWaitTimeMs = RetryWaitTimeMs;
                _chromeBrowser.Navigate().Refresh();
            }
        }
        Log.Error("Attempts expired!");
        throw new ConnectionAbortedException();
    }

    public void Dispose()
    {
        Log.Logger.Information("Disposing steam Api...");
        _chromeBrowser?.Quit();
        Log.Logger.Information("Steam Api disposed!");
    }
}