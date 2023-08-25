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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.Services;

public sealed class SeleniumSteamApi : ISteamApi, IDisposable
{
    private readonly IWebDriver _chromeBrowser;

    public SeleniumSteamApi(Func<IWebDriver> webDriver)
    {
        Log.Logger.Information("Creating selenium steam API...");
        _chromeBrowser = webDriver.Invoke();
        Log.Logger.Information("Selenium steam API created!");
    }

    #region Balance

    private const string BalanceLabelId = "header_wallet_balance";
    private static readonly By BalanceLabel = By.Id(BalanceLabelId);
    public async Task<double> GetBalanceAsync()
    {
        return await SafeConnect(() => ParsePrice(ReadFromElement(BalanceLabel)));
    }

    #endregion

    #region OrderBooks

    private const int HtmlElementOffset = 2;
    private const int SellListingPageSize = 10;

    private const string BuyListingDetailButtonCssPath = "#market_buyorder_info_show_details > span";
    private static readonly By BuyListingDetailsButton = By.CssSelector(BuyListingDetailButtonCssPath);

    private static By GetBuyListingPriceElementByRow(int rowIndex)
        => By.CssSelector($"#market_commodity_buyreqeusts_table > table > tbody > tr:nth-child({rowIndex}) > td:nth-child(1)");
    private static By GetBuyListingQuantityElementByRow(int rowIndex)
        => By.CssSelector($"#market_commodity_buyreqeusts_table > table > tbody > tr:nth-child({rowIndex}) > td:nth-child(2)");

    public async Task<List<OrderBookItem>> GetBuyOrdersBookAsync(string itemUrl, int buyListingFindRange)
    {
        return await SafeConnect(() =>
        {
            SetPage(itemUrl);
            var orderBook = new List<OrderBookItem>();
            ClickOnElement(BuyListingDetailsButton);

            for (var rowIdx = HtmlElementOffset; rowIdx < buyListingFindRange + HtmlElementOffset; rowIdx++)
            {
                var price = ParsePrice(ReadFromElement(GetBuyListingPriceElementByRow(rowIdx)));
                var quantity = int.Parse(ReadFromElement(GetBuyListingQuantityElementByRow(rowIdx)));
                orderBook.Add(new OrderBookItem(price, quantity));
            }
            return orderBook.OrderByDescending(x => x.Price).ToList();
        });
    }

    private const int SellListingPagesOffset = 1;

    private static By GetSellListingPriceElementByRow(int rowIndex)
        => By.CssSelector($"//*[@id='searchResultsRows']/div[{rowIndex}]/div[2]/div[2]/span[1]/span[1]");

    private static By GetSellListingPageButton(int pageNum)
        => By.XPath($"//*[@id='searchResults_links']/span[{pageNum + SellListingPagesOffset}]");

    public async Task<List<OrderBookItem>> GetSellOrdersBookAsync(string itemUrl, int sellListingFindRange)
    {
        await SafeConnect(() =>
        {
            SetPage(itemUrl);
            return true;
        });

        var orderBook = new List<OrderBookItem>();
        for (var pageIdx = 1; pageIdx <= sellListingFindRange; pageIdx++)
        {
            for (var rowIdx = HtmlElementOffset; rowIdx < SellListingPageSize + HtmlElementOffset; rowIdx++)
            {
                var sellPriceStr = await SafeConnect(() => ReadFromElement(GetSellListingPriceElementByRow(rowIdx)));
                var parsedSellPrice = ParsePrice(sellPriceStr);
                if (parsedSellPrice == 0)
                    continue;
                orderBook.Add(new OrderBookItem(parsedSellPrice, 1));
            }

            await SafeConnect(() =>
            {
                ClickOnElement(GetSellListingPageButton(pageIdx));
                return true;
            }, true);
        }

        return orderBook
            .GroupBy(x => x.Price, new ClosenessComparer(0.01))
            .Select(x => new OrderBookItem(x.Key, x.Count()))
            .OrderBy(x => x.Price)
            .ToList();
    }

    private class ClosenessComparer : IEqualityComparer<double>
    {
        private readonly double delta;

        public ClosenessComparer(double delta)
        {
            this.delta = delta;
        }

        public bool Equals(double x, double y)
        {
            return Math.Abs((x + y) / 2f - y) < delta;
        }

        public int GetHashCode(double obj)
        {
            return 0;
        }
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

    private const int ChartItemSize = 3;
    private const string MonthlyChartScaleButtonId =
        "#mainContents > div.market_page_fullwidth.market_listing_firstsection > div > div.zoom_controls.pricehistory_zoom_controls > a:nth-child(2)";

    private static readonly By MonthlyChartScaleButton = By.CssSelector(MonthlyChartScaleButtonId);

    public async Task<Chart> GetChartAsync(string itemUrl, DateTime fromDate)
    {
        var isChartReceivedSuccessfully = await SafeConnect(() =>
        {
            SetPage(itemUrl);
            ClickOnElement(MonthlyChartScaleButton);
            return true;
        });
        if (!isChartReceivedSuccessfully) return new Chart();

        var chartHtmlString = string.Join("", _chromeBrowser.PageSource
            .Split("\n")
            .First(x => x.StartsWith("\t\t\tvar line1="))
            .SkipWhile(x => x != '['));

        var chartPoints = chartHtmlString
            .Split(",")
            .Select(DeleteServiceCharacters)
            .Chunk(ChartItemSize)
            .Select(x => new Chart.PointInfo
            {
                Date = DateTime.ParseExact(x[0], "MMM dd yyyy HH: z", CultureInfo.InvariantCulture),
                Price = double.Parse(x[1], NumberStyles.Any, CultureInfo.InvariantCulture),
                Quantity = int.Parse(x[2])
            })
            .Where(x => x.Date > fromDate);
        return new Chart(chartPoints);
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

    private const string ItemNameLabelId = "iteminfo0_item_name";
    private static readonly By ItemNameLabel = By.Id(ItemNameLabelId);

    private const string ItemDescriptionId = "//*[@id='iteminfo0_item_descriptors']/div[1]";
    private static readonly By ItemDescriptionLabel = By.XPath(ItemDescriptionId);

    private const string ScrollPageJScript = "scroll(0, 20000000);";
    private const string SellSelectedItemJScript = "javascript:SellCurrentSelection()";

    private const string SellPriceTextBoxId = "market_sell_buyercurrency_input";
    private static readonly By SellPriceTextBox = By.Id(SellPriceTextBoxId);

    private const string SellAgreementCheckBoxId = "market_sell_dialog_accept_ssa";
    private static readonly By SellAgreementCheckBox = By.Id(SellAgreementCheckBoxId);

    private const string SellAgreementButtonId = "market_sell_dialog_accept";
    private static readonly By SellAgreementButton = By.Id(SellAgreementButtonId);

    private const string SellConfirmationButtonId = "market_sell_dialog_ok";
    private static readonly By SellConfirmationButton = By.Id(SellConfirmationButtonId);

    private static string GetInventoryPageAddress(string userId) => 
        $"https://steamcommunity.com/profiles/{userId}/inventory/#730";

    private static By GetInventoryItemElement(string userId, int itemNum) =>
        By.XPath($"//*[@id='inventory_{userId}_730_2']/div[{itemNum + 1}]/div/div");

    private static string GetItemQuality(string itemDescription) =>
        string.Join("", itemDescription.SkipWhile(x => x != ':').Skip(2)).Trim();

    public async Task<bool> PlaceSellOrderAsync(string itemName, double price, string userId, int inventoryFindRange = 10)
    {
        return await SafeConnect(() =>
        {
            SetPage(GetInventoryPageAddress(userId));
            for (var itemNum = 0; itemNum < inventoryFindRange; itemNum++)
            {
                ClickOnElement(GetInventoryItemElement(userId, itemNum));
                var currentItemName = ReadFromElement(ItemNameLabel).Trim();
                var currentItemQuality = GetItemQuality(ReadFromElement(ItemDescriptionLabel));
                var fullItemName = $"{currentItemName} ({currentItemQuality})";
                if (itemName != fullItemName) continue;
                ExecuteJs(ScrollPageJScript);
                ExecuteJs(SellSelectedItemJScript);
                SendKey(SellPriceTextBox, $"\b\b\b\b\b{price}");
                ClickOnElement(SellAgreementCheckBox);
                ClickOnElement(SellAgreementButton);
                ClickOnElement(SellConfirmationButton, true, 2);
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

    private const string RusItemNameLabelId = "largeiteminfo_item_name";
    private static readonly By RusItemNameLabel = By.Id(RusItemNameLabelId);

    private const string RusItemDescriptionLabelId = "//*[@id='largeiteminfo_item_descriptors']/div[1]";
    private static readonly By RusItemDescriptionLabel = By.XPath(RusItemDescriptionLabelId);

    private const string PlaceOrderButtonId = "//*[@id='market_buyorder_info']/div/div/a";
    private static readonly By PlaceOrderButton = By.XPath(PlaceOrderButtonId);

    private const string BuyOrderPriceTextBoxId = "market_buy_commodity_input_price";
    private static readonly By BuyOrderPriceTextBox = By.Id(BuyOrderPriceTextBoxId);

    private const string BuyOrderQuantityTextBoxId = "input_quantity";
    private static readonly By BuyOrderQuantityTextBox = By.Id(BuyOrderQuantityTextBoxId);

    private const string BuyAgreementCheckBoxId = "market_buyorder_dialog_accept_ssa";
    private static readonly By BuyAgreementCheckBox = By.Id(BuyAgreementCheckBoxId);

    private const string BuyConfirmationButtonId = "market_buyorder_dialog_purchase";
    private static readonly By BuyConfirmationButton = By.Id(BuyConfirmationButtonId);

    public async Task<string> GetRusItemNameAsync(string itemUrl)
    {
        return await SafeConnect(() =>
        {
            SetPage(itemUrl);
            var itemName = ReadFromElement(RusItemNameLabel);
            var quality = GetItemQuality(ReadFromElement(RusItemDescriptionLabel));
            return $"{itemName} ({quality})";
        });
    }

    public async Task<bool> PlaceBuyOrderAsync(string itemUrl, double price, int quantity)
    {
        return await SafeConnect(() =>
        {
            SetPage(itemUrl);
            ClickOnElement(PlaceOrderButton);
            SendKey(BuyOrderPriceTextBox, $"\b\b\b\b\b\b\b\b\b\b\b{Math.Round(price + 0.01, 2, MidpointRounding.AwayFromZero)}");
            SendKey(BuyOrderQuantityTextBox, $"\b{quantity}");
            ClickOnElement(BuyAgreementCheckBox);
            ClickOnElement(BuyConfirmationButton);
            return true;
        }, true);
    }

    private const string ExistingBuyOrderPriceLabelId = "//*[@id='tabContentsMyListings']/div/div[2]/div[3]/span/span";
    private static readonly By ExistingBuyOrderPriceLabel = By.XPath(ExistingBuyOrderPriceLabelId);

    public async Task<int?> GetBuyOrderQuantityAsync(string itemUrl)
    {
        return await SafeConnect(() =>
        {
            SetPage(itemUrl);
            try
            {
                var quantity = int.Parse(ReadFromElement(ExistingBuyOrderPriceLabel, true));
                return quantity;
            }
            catch
            {
                return new int?();
            }
        });
    }

    private const string ExistingBuyOrderQuantityLabelId = "//*[@id='tabContentsMyListings']/div/div[2]/div[2]/span/span";
    private static readonly By ExistingBuyOrderQuantityLabel = By.XPath(ExistingBuyOrderQuantityLabelId);

    public async Task<double?> GetBuyOrderPriceAsync(string itemUrl)
    {
        return await SafeConnect(() =>
        {
            SetPage(itemUrl);
            try
            {
                var price = ReadFromElement(ExistingBuyOrderQuantityLabel, true);
                return ParsePrice(price);
            }
            catch
            {
                return new double?();
            }
        });
    }

    private const string ExistingBuyOrderCancelButtonId = "//*[@id='tabContentsMyListings']/div/div[2]/div[2]/span/span";
    private static readonly By ExistingBuyOrderCancelButton = By.XPath(ExistingBuyOrderCancelButtonId);

    public async Task<bool> CancelBuyOrderAsync(string itemUrl)
    {
        return await SafeConnect(() =>
        {
            SetPage(itemUrl);
            ClickOnElement(ExistingBuyOrderCancelButton);
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

    private static readonly By LoginTextBox = By.XPath("//*[@id='responsive_page_template_content']/div[1]/div[1]/div/div/div/div[2]/div/form/div[1]/input");
    private static readonly By PasswordTextBox = By.XPath("//*[@id='responsive_page_template_content']/div[1]/div[1]/div/div/div/div[2]/div/form/div[2]/input");
    private static readonly By LoginButton = By.XPath("//*[@id='responsive_page_template_content']/div[1]/div[1]/div/div/div/div[2]/div/form/div[4]/button");
    private static readonly By TwoFactorTextBox = By.XPath("//*[@id='responsive_page_template_content']/div[1]/div[1]/div/div/div/div[2]/form/div/div[2]/div[1]/div/input[1]");
    private static readonly By LoginCheckButton = By.XPath("//*[@id='account_pulldown']");
    private static readonly By LoginCheckTextBox = By.XPath("//*[@id='account_dropdown']/div/a[3]/span");

    #endregion

    public async Task<bool> LogIn(string login, string password, string secret)
    {
        Log.Information("Signing in...");
        Log.Information($"Incoming user data {login}, {password}, {secret}");

        if (IsAuthenticated(login))
            return true;

        LogOut();

        var token = await GetTokenAsync(secret);

        await SafeConnect(() =>
        {
            SetPage("https://steamcommunity.com/login/home/?goto=");
            WriteToElement(LoginTextBox, login);
            WriteToElement(PasswordTextBox, password);
            ClickOnElement(LoginButton);
            return true;
        }, true);

        try
        {
            WriteToElement(TwoFactorTextBox, token, true);
        }
        catch
        {
            Log.Error("Authorization failed. Login or password are incorrect.");
            return false;
        }

        if (!IsAuthenticated(login))
        {
            Log.Error("Authorization failed. User data are incorrect.");
            return false;
        }
        Log.Information("Authentication completed successful");
        return true;
    }

    private bool IsAuthenticated(string loginToCompare)
    {
        Log.Information("Checking authentication state...");
        try
        {
            ClickOnElement(LoginCheckButton, true);
            var currentLogin = ReadFromElement(LoginCheckTextBox, true);
            if (currentLogin == loginToCompare)
            {
                Log.Information("Successfully authenticated");
                return true;
            }
            Log.Logger.Error("Current login: {0}, login to compare: {1}", currentLogin, loginToCompare);
        }
        catch (Exception exception)
        {
            Log.Logger.Error("Exception while auth checking: {0}\r\n{1}", exception.Message, exception.StackTrace);
        }
        Log.Information("Not authenticated");
        return false;
    }

    private const string SecretTextBoxId = "secret";
    private static readonly By SecretTextBox = By.Id(SecretTextBoxId);

    private const string GenerateTokenButtonId = "generate";
    private static readonly By GenerateTokenButton = By.Id(GenerateTokenButtonId);

    private const string TokenLabelId = "result"; 
    private static readonly By TokenLabel = By.Id(TokenLabelId);

    private async Task<string> GetTokenAsync(string secret)
    {
        return await SafeConnect(() =>
        {
            SetPage("https://www.chescos.me/js-steam-authcode-generator/?");
            SendKey(SecretTextBox, secret);
            ClickOnElement(GenerateTokenButton);
            return ReadFromElement(TokenLabel);
        }, true);
    }

    private const string LogOutJScript = "javascript:Logout()";

    public void LogOut()
    {
        Log.Information("Signing out...");
        SetPage("https://steamcommunity.com");
        ExecuteJs(LogOutJScript);
        Log.Information("Successful sign out");
    }

    #endregion

    #region SeleniumMethods

    private const int DefaultImplicitWaitTime = 5;

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

    #region SafeConnect

    private const int RequestDelayMs = 5000;
    private const int RetryWaitTimeMs = 2000;
    private const int RetriesCount = 5;

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

    #endregion

    public void Dispose()
    {
        Log.Logger.Information("Disposing steam Api...");
        _chromeBrowser.Quit();
        _chromeBrowser.Dispose();
        Log.Logger.Information("Steam Api disposed!");
    }
}