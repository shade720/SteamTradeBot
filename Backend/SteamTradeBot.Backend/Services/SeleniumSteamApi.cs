using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Serilog;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.Services;

public class SeleniumSteamApi : ISteamApi, IDisposable
{
    private readonly IWebDriver _chromeBrowser;
    private const int DefaultImplicitWaitTime = 5;
    private const int RequestDelayMs = 5000;
    private const int RetryWaitTimeMs = 2000;
    private const int RetriesCount = 5;

    public SeleniumSteamApi(Func<IWebDriver> webDriver)
    {
        _chromeBrowser = webDriver.Invoke();
        Log.Logger.Information("Steam Api created!");
    }

    #region Balance

    public async Task<double> GetBalanceAsync()
    {
        return await SafeConnect(() => ParsePrice(ReadFromElement(By.Id("header_wallet_balance"), true)));
    }

    #endregion

    #region OrderBooks

    public async Task<List<OrderBookItem>> GetBuyOrdersBookAsync(string itemUrl, int buyListingFindRange)
    {
        return await SafeConnect(() =>
        {
            SetPage(itemUrl);
            var orderBook = new List<OrderBookItem>();
            ClickOnElement(By.CssSelector("#market_buyorder_info_show_details > span"));
            for (var itemIdx = 2; itemIdx < buyListingFindRange + 2; itemIdx++)
            {
                var price = ParsePrice(ReadFromElement(By.CssSelector($"#market_commodity_buyreqeusts_table > table > tbody > tr:nth-child({itemIdx}) > td:nth-child(1)")));
                var quantity = int.Parse(ReadFromElement(By.CssSelector($"#market_commodity_buyreqeusts_table > table > tbody > tr:nth-child({itemIdx}) > td:nth-child(2)")));
                orderBook.Add(new OrderBookItem { Price = price, Quantity = quantity });
            }
            return orderBook.OrderByDescending(x => x.Price).ToList();
        });
    }

    public async Task<List<OrderBookItem>> GetSellOrdersBookAsync(string itemUrl, int sellListingFindRange)
    {
        const int sellListingPageSize = 10;
        await SafeConnect(() =>
        {
            SetPage(itemUrl);
            return true;
        });

        var orderBook = new List<OrderBookItem>();
        for (var pageIdx = 1; pageIdx <= sellListingFindRange; pageIdx++)
        {
            var idx = pageIdx;
            for (var itemIdx = 0; itemIdx < sellListingPageSize; itemIdx++)
            {
                var sellPriceStr = await SafeConnect(() => ReadFromElement(By.XPath($"//*[@id='searchResultsRows']/div[{idx + 2}]/div[2]/div[2]/span[1]/span[1]")));
                var price = ParsePrice(sellPriceStr);
                if (price == 0)
                    continue;

                if (orderBook.Any(x => Math.Abs(x.Price - price) < 0.01))
                    orderBook.First(x => Math.Abs(x.Price - price) < 0.01).Quantity += 1;
                else
                    orderBook.Add(new OrderBookItem { Price = price, Quantity = 1 });
            }

            await SafeConnect(() =>
            {
                ClickOnElement(By.XPath($"//*[@id='searchResults_links']/span[{idx + 1}]"));
                return true;
            }, true);
        }
        return orderBook.OrderBy(x => x.Price).ToList();
    }

    private static double ParsePrice(string priceStr)
    {
        if (string.IsNullOrEmpty(priceStr))
            return 0;
        priceStr = string.Join("", priceStr.SkipWhile(x => !char.IsDigit(x)).TakeWhile(x => !char.IsWhiteSpace(x))).Replace(',', '.');
        return double.TryParse(priceStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var result) ? result : 0;
    }

    #endregion

    #region Chart

    public async Task<Chart> GetChartAsync(string itemUrl, DateTime fromDate)
    {
        if (!await SafeConnect(() =>
            {
                SetPage(itemUrl);
                ClickOnElement(By.CssSelector("#mainContents > div.market_page_fullwidth.market_listing_firstsection > div > div.zoom_controls.pricehistory_zoom_controls > a:nth-child(3)"));
                return true;
            })) return new Chart();

        var graphHtmlString = string.Join("", _chromeBrowser.PageSource
            .Split("\n")
            .First(x => x.StartsWith("\t\t\tvar line1="))
            .SkipWhile(x => x != '['));

        var graphPoints = graphHtmlString
            .Split(",")
            .Select(DeleteServiceCharacters)
            .Chunk(3)
            .Select(x => new Chart.PointInfo
            {
                Date = DateTime.ParseExact(x[0], "MMM dd yyyy HH: z", CultureInfo.InvariantCulture),
                Price = double.Parse(x[1], NumberStyles.Any, CultureInfo.InvariantCulture),
                Quantity = int.Parse(x[2])
            })
            .Where(x => x.Date > fromDate);
        return new Chart(graphPoints);
    }

    private static string DeleteServiceCharacters(string line)
    {
        return line
            .Replace("\"", "")
            .Replace("\r", "")
            .Replace("\t", "")
            .Replace(";", "")
            .TrimStart('[').TrimEnd(']');
    }


    #endregion

    #region SellOrder

    public async Task<bool> PlaceSellOrderAsync(string itemName, double price, string userId, int inventoryFindRange = 10)
    {
        return await SafeConnect(() =>
        {
            SetPage($"https://steamcommunity.com/profiles/{userId}/inventory/#730");
            for (var i = 0; i < inventoryFindRange; i++)
            {
                ClickOnElement(By.XPath($"//*[@id='inventory_{userId}_730_2']/div[{i + 1}]/div/div"));
                var currentItemName = ReadFromElement(By.Id("iteminfo0_item_name")).Trim();
                var currentItemQuality = string.Join("", ReadFromElement(By.XPath("//*[@id='iteminfo0_item_descriptors']/div[1]")).SkipWhile(x => x != ':').Skip(2)).Trim();
                var fullItemName = $"{currentItemName} ({currentItemQuality})";
                if (itemName != fullItemName) continue;
                ExecuteJs("scroll(0, 20000000);");
                ExecuteJs("javascript:SellCurrentSelection()");
                SendKey(By.Id("market_sell_buyercurrency_input"), $"\b\b\b\b\b{price}");
                ClickOnElement(By.Id("market_sell_dialog_accept_ssa"));
                ClickOnElement(By.Id("market_sell_dialog_accept"));
                ClickOnElement(By.Id("market_sell_dialog_ok"), true, 2);
                return true;
            }
            return false;
        });
    }

    public async Task<double?> GetSellOrderPriceAsync(string itemUrl)
    {
        return await SafeConnect(() =>
        {
            SetPage(itemUrl);
            try
            {
                var priceStr = ReadFromElement(By.XPath("/html/body/div[1]/div[7]/div[4]/div[1]/div[4]/div[1]/div[2]/div/div[5]/div/div[1]/div[2]/div/div[2]/span/span/span/span[1]"), true);
                return ParsePrice(priceStr);
            }
            catch
            {
                return new double?();
            }
        });
    }

    public async Task<bool> CancelSellOrderAsync(string itemUrl)
    {
        return await SafeConnect(() =>
        {
            SetPage(itemUrl);
            ClickOnElement(By.XPath("/html/body/div[1]/div[7]/div[4]/div[1]/div[4]/div[1]/div[2]/div/div[5]/div/div[1]/div[2]/div/div[5]/div/a/span[2]"));
            ClickOnElement(By.XPath("//*[@id='market_removelisting_dialog_accept']"));
            return true;
        });
    }

    #endregion

    #region BuyOrder

    public async Task<string> GetRusItemNameAsync(string itemUrl)
    {
        return await SafeConnect(() =>
        {
            SetPage(itemUrl);
            var itemName = ReadFromElement(By.Id("largeiteminfo_item_name"));
            var quality = string.Join("", ReadFromElement(By.XPath("//*[@id='largeiteminfo_item_descriptors']/div[1]")).SkipWhile(x => x != ':').Skip(2));
            return $"{itemName} ({quality})";
        });
    }

    public async Task<bool> PlaceBuyOrderAsync(string itemUrl, double price, int quantity)
    {
        return await SafeConnect(() =>
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

    public async Task<int?> GetBuyOrderQuantityAsync(string itemUrl)
    {
        return await SafeConnect(() =>
        {
            SetPage(itemUrl);
            try
            {
                var quantity = int.Parse(ReadFromElement(By.XPath("//*[@id='tabContentsMyListings']/div/div[2]/div[3]/span/span"), true));
                return quantity;
            }
            catch
            {
                return new int?();
            }
        });
    }

    public async Task<double?> GetBuyOrderPriceAsync(string itemUrl)
    {
        return await SafeConnect(() =>
        {
            SetPage(itemUrl);
            try
            {
                var price = ReadFromElement(By.XPath("//*[@id='tabContentsMyListings']/div/div[2]/div[2]/span/span"), true);
                return ParsePrice(price);
            }
            catch
            {
                return new double?();
            }
        });
    }

    public async Task<bool> CancelBuyOrderAsync(string itemUrl)
    {
        return await SafeConnect(() =>
        {
            SetPage(itemUrl);
            ClickOnElement(By.XPath("//*[@id='tabContentsMyListings']/div/div[2]/div[5]/div/a/span[2]"));
            return true;
        }, true);
    }

    #endregion

    #region GetItemList

    public async Task<List<string>> GetItemNamesListAsync(double startPrice, double endPrice, double salesVolumeByWeek, int listSize)
    {
        return await SafeConnect(() =>
        {
            var result = new List<string>();
            SetPage("https://skins-table.xyz/table/");
            try
            {
                Log.Logger.Information("Check if we are already logged in...");
                ClickOnElement(By.XPath("/html/body/div[1]/div[4]/div/div/center/div/div/div[2]/form/div[2]/a"), true);
                ClickOnElement(By.Id("imageLogin"));
                Log.Logger.Information("Logged in successfully on skins-table.xyz");
            }
            catch
            {
                Log.Logger.Information("Already logged in on skins-table.xyz");
            }

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
                Log.Logger.Information("Get {0} item", itemName);
                result.Add(itemName);
            }
            return result;
        });
    }

    public async Task<string> GetItemUrlAsync(string itemName)
    {
        return await SafeConnect(() =>
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

    public async Task LogIn(string login, string password, string secret)
    {
        Log.Information("Signing in...");
        Log.Information($"Incoming user data {login}, {password}, {secret}");

        if (IsAuthenticated(login))
            return;
        
        LogOut();

        var token = await GetTokenAsync(secret);

        await SafeConnect(() =>
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

        if (!IsAuthenticated(login))
        {
            Log.Error("Authorization failed. User data are incorrect.");
            throw new AuthenticationException("Authorization failed. User data are incorrect.");
        }
        Log.Information("Authentication completed successful");
    }

    private bool IsAuthenticated(string loginToCompare)
    {
        Log.Information("Checking if authentication successful...");
        try
        {
            if (ReadFromElement(LoginCheck, true) != loginToCompare)
            {
                Log.Information("Successfully authenticated");
                return true;
            }
        }
        catch 
        {
            // ignored
        }
        Log.Information("Authentication failed");
        return false;
    }

    private async Task<string> GetTokenAsync(string secret)
    {
        return await SafeConnect(() =>
        {
            SetPage("https://www.chescos.me/js-steam-authcode-generator/?");
            SendKey(By.Id("secret"), secret);
            ClickOnElement(By.Id("generate"));
            return ReadFromElement(By.Id("result"));
        }, true);
    }

    public void LogOut()
    {
        Log.Information("Signing out...");
        SetPage("https://steamcommunity.com");
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

    private async Task<T> SafeConnect<T>(Func<T> unsafeFunc, bool isDelayNeeded = false)
    {
        var retryWaitTimeMs = isDelayNeeded ? RequestDelayMs : 0;
        for (var attempt = 0; attempt < RetriesCount; attempt++)
        {
            try
            {
                Thread.Sleep(retryWaitTimeMs);
                return await Task.Run(unsafeFunc);
            }
            catch (Exception e)
            {
                Log.Error("Connection error! Attempt: {0}/{1}\r\nMessage: {2}\r\nStack trace: {3}", attempt + 1, RetriesCount, e.Message, e.StackTrace);
                retryWaitTimeMs = RetryWaitTimeMs;
                _chromeBrowser.Navigate().Refresh();
            }
        }
        Log.Error("Number of attempts are expired!");
        throw new Exception("Number of attempts are expired!");
    }

    public void Dispose()
    {
        Log.Logger.Information("Disposing steam Api...");
        _chromeBrowser.Quit();
        _chromeBrowser.Dispose();
        Log.Logger.Information("Steam Api disposed!");
    }
}