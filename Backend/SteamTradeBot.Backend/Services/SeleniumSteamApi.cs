using OpenQA.Selenium;
using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.Services;

public sealed class SeleniumSteamApi : ISteamApi, IDisposable
{
    private readonly SeleniumWebDriver _webDriver;

    public SeleniumSteamApi(SeleniumWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    #region Balance

    private const string BalanceLabelId = "header_wallet_balance";
    private static readonly By BalanceLabel = By.Id(BalanceLabelId);

    public async Task<double> GetBalanceAsync()
    {
        return await _webDriver.SafeConnect(() => ParsePrice(_webDriver.ReadFromElement(BalanceLabel)));
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
        return await _webDriver.SafeConnect(() =>
        {
            _webDriver.SetPage(itemUrl);
            var orderBook = new List<OrderBookItem>();
            _webDriver.ClickOnElement(BuyListingDetailsButton);

            for (var rowIdx = HtmlElementOffset; rowIdx < buyListingFindRange + HtmlElementOffset; rowIdx++)
            {
                var price = ParsePrice(_webDriver.ReadFromElement(GetBuyListingPriceElementByRow(rowIdx)));
                var quantity = int.Parse(_webDriver.ReadFromElement(GetBuyListingQuantityElementByRow(rowIdx)));
                orderBook.Add(new OrderBookItem(price, quantity));
            }
            return orderBook.OrderByDescending(x => x.Price).ToList();
        });
    }

    private const int SellListingPagesOffset = 1;

    private static By GetSellListingPriceElementByRow(int rowIndex)
        => By.XPath($"//*[@id='searchResultsRows']/div[{rowIndex}]/div[2]/div[2]/span[1]/span[1]");

    private static By GetSellListingPageButton(int pageNum)
        => By.XPath($"//*[@id='searchResults_links']/span[{pageNum + SellListingPagesOffset}]");

    public async Task<List<OrderBookItem>> GetSellOrdersBookAsync(string itemUrl, int sellListingFindRange)
    {
        await _webDriver.SafeConnect(() =>
        {
            _webDriver.SetPage(itemUrl);
            return true;
        });

        var orderBook = new List<OrderBookItem>();
        for (var pageIdx = 1; pageIdx <= sellListingFindRange; pageIdx++)
        {
            for (var rowIdx = HtmlElementOffset; rowIdx < SellListingPageSize + HtmlElementOffset; rowIdx++)
            {
                var sellPriceStr = await _webDriver.SafeConnect(() => _webDriver.ReadFromElement(GetSellListingPriceElementByRow(rowIdx)));
                var parsedSellPrice = ParsePrice(sellPriceStr);
                if (parsedSellPrice == 0)
                    continue;
                orderBook.Add(new OrderBookItem(parsedSellPrice, 1));
            }

            await _webDriver.SafeConnect(() =>
            {
                _webDriver.ClickOnElement(GetSellListingPageButton(pageIdx));
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
        private readonly double _delta;

        public ClosenessComparer(double delta)
        {
            _delta = delta;
        }

        public bool Equals(double x, double y)
        {
            return Math.Abs((x + y) / 2f - y) < _delta;
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
        var isChartReceivedSuccessfully = await _webDriver.SafeConnect(() =>
        {
            _webDriver.SetPage(itemUrl);
            _webDriver.ClickOnElement(MonthlyChartScaleButton);
            return true;
        });
        if (!isChartReceivedSuccessfully) return new Chart();

        var chartHtmlString = string.Join("", _webDriver.GetPageSource()
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

    private const string ItemNameLabelId1 = "//*[@id='iteminfo0_item_name']";
    private static readonly By ItemNameLabel1 = By.XPath(ItemNameLabelId1);

    private const string ItemNameLabelId2 = "//*[@id='iteminfo1_item_name']";
    private static readonly By ItemNameLabel2 = By.XPath(ItemNameLabelId2);

    private const string ItemDescriptionId1 = "//*[@id='iteminfo0_item_descriptors']/div[1]";
    private static readonly By ItemDescriptionLabel1 = By.XPath(ItemDescriptionId1);

    private const string ItemDescriptionId2 = "//*[@id='iteminfo1_item_descriptors']/div[1]";
    private static readonly By ItemDescriptionLabel2 = By.XPath(ItemDescriptionId2);

    private const string ScrollPageJScript = "scroll(0, 20000000);";
    private const string SellSelectedItemJScript = "javascript:SellCurrentSelection()";

    private const string SellPriceTextBoxId = "//*[@id='market_sell_buyercurrency_input']";
    private static readonly By SellPriceTextBox = By.XPath(SellPriceTextBoxId);

    private const string SellAgreementCheckBoxId = "//*[@id='market_sell_dialog_accept_ssa']";
    private static readonly By SellAgreementCheckBox = By.XPath(SellAgreementCheckBoxId);

    private const string SellAgreementButtonId = "//*[@id='market_sell_dialog_accept']";
    private static readonly By SellAgreementButton = By.XPath(SellAgreementButtonId);

    private const string SellConfirmationButtonId = "//*[@id='market_sell_dialog_ok']";
    private static readonly By SellConfirmationButton = By.XPath(SellConfirmationButtonId);

    private static string GetInventoryPageAddress(string userId) =>
        $"https://steamcommunity.com/profiles/{userId}/inventory/#730";

    private static By GetInventoryItemElement(string userId, int itemNum) =>
        By.XPath($"//*[@id='inventory_{userId}_730_2']/div/div[{itemNum + 1}]/div/a");

    private static string GetItemQuality(string itemDescription) =>
        string.Join("", itemDescription.SkipWhile(x => x != ':').Skip(2)).Trim();

    public async Task<bool> PlaceSellOrderAsync(string itemName, double price, string userId, int inventoryFindRange = 15)
    {
        return await _webDriver.SafeConnect(() =>
        {
            _webDriver.SetPage(GetInventoryPageAddress(userId));
            for (var itemNum = 0; itemNum < inventoryFindRange; itemNum++)
            {
                _webDriver.ClickOnElement(GetInventoryItemElement(userId, itemNum), true, 1);
                var currentItemName = _webDriver.ReadFromElement(ItemNameLabel1).Trim() != string.Empty 
                    ? _webDriver.ReadFromElement(ItemNameLabel1).Trim()
                    : _webDriver.ReadFromElement(ItemNameLabel2).Trim();

                var description = _webDriver.ReadFromElement(ItemDescriptionLabel1).Trim() != string.Empty
                    ? _webDriver.ReadFromElement(ItemDescriptionLabel1).Trim()
                    : _webDriver.ReadFromElement(ItemDescriptionLabel2).Trim();

                var currentItemQuality = GetItemQuality(description);
                var fullItemName = $"{currentItemName} ({currentItemQuality})";
                if (itemName != fullItemName) continue;
                _webDriver.ExecuteJs(ScrollPageJScript);
                _webDriver.ExecuteJs(SellSelectedItemJScript);
                _webDriver.Clear(SellPriceTextBox);
                _webDriver.SendKey(SellPriceTextBox, price.ToString());
                _webDriver.ClickOnElement(SellAgreementCheckBox);
                _webDriver.ClickOnElement(SellAgreementButton);
                if (!JavaScriptErrorRetry(() =>_webDriver.ClickOnElement(SellConfirmationButton, true)))
                    throw new JavaScriptException("Message: javascript error: this.each is not a function.");
                return true;
            }
            return false;
        });
    }

    /// <summary>
    /// Temp.
    /// </summary>
    /// <param name="dangerAction"></param>
    /// <returns></returns>
    private static bool JavaScriptErrorRetry(Action dangerAction)
    {
        for (var i = 0; i < 10; i++)
        {
            try
            {
                dangerAction();
                return true;
            }
            catch (JavaScriptException)
            {
                Thread.Sleep(1000);
            }
        }
        return false;
    }

    public async Task<double?> GetSellOrderPriceAsync(string itemUrl)
    {
        return await _webDriver.SafeConnect(() =>
        {
            _webDriver.SetPage(itemUrl);
            try
            {
                var priceStr = _webDriver.ReadFromElement(By.XPath("/html/body/div[1]/div[7]/div[4]/div[1]/div[4]/div[1]/div[2]/div/div[5]/div/div[1]/div[2]/div/div[2]/span/span/span/span[1]"), true);
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
        return await _webDriver.SafeConnect(() =>
        {
            _webDriver.SetPage(itemUrl);
            _webDriver.ClickOnElement(By.XPath("/html/body/div[1]/div[7]/div[4]/div[1]/div[4]/div[1]/div[2]/div/div[5]/div/div[1]/div[2]/div/div[5]/div/a/span[2]"));
            _webDriver.ClickOnElement(By.XPath("//*[@id='market_removelisting_dialog_accept']"));
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

    private const string BuyOrderQuantityTextBoxId = "market_buy_commodity_input_quantity";
    private static readonly By BuyOrderQuantityTextBox = By.Id(BuyOrderQuantityTextBoxId);

    private const string BuyAgreementCheckBoxId = "market_buyorder_dialog_accept_ssa";
    private static readonly By BuyAgreementCheckBox = By.Id(BuyAgreementCheckBoxId);

    private const string BuyConfirmationButtonId = "market_buyorder_dialog_purchase";
    private static readonly By BuyConfirmationButton = By.Id(BuyConfirmationButtonId);

    public async Task<string> GetRusItemNameAsync(string itemUrl)
    {
        return await _webDriver.SafeConnect(() =>
        {
            _webDriver.SetPage(itemUrl);
            var itemName = _webDriver.ReadFromElement(RusItemNameLabel);
            var quality = GetItemQuality(_webDriver.ReadFromElement(RusItemDescriptionLabel));
            return $"{itemName} ({quality})";
        });
    }

    public async Task<bool> PlaceBuyOrderAsync(string itemUrl, double price, int quantity)
    {
        return await _webDriver.SafeConnect(() =>
        {
            _webDriver.SetPage(itemUrl);
            _webDriver.ClickOnElement(PlaceOrderButton);
            _webDriver.SendKey(BuyOrderPriceTextBox, $"\b\b\b\b\b\b\b\b\b\b\b{Math.Round(price + 0.01, 2, MidpointRounding.AwayFromZero)}", true);
            _webDriver.SendKey(BuyOrderQuantityTextBox, $"\b{quantity}");
            _webDriver.ClickOnElement(BuyAgreementCheckBox);
            _webDriver.ClickOnElement(BuyConfirmationButton);
            return true;
        }, true);
    }

    private const string ExistingBuyOrderPriceLabelId = "//*[@id='tabContentsMyListings']/div/div[2]/div[3]/span/span";
    private static readonly By ExistingBuyOrderPriceLabel = By.XPath(ExistingBuyOrderPriceLabelId);

    public async Task<int?> GetBuyOrderQuantityAsync(string itemUrl)
    {
        return await _webDriver.SafeConnect(() =>
        {
            _webDriver.SetPage(itemUrl);
            try
            {
                var quantity = int.Parse(_webDriver.ReadFromElement(ExistingBuyOrderPriceLabel, true));
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
        return await _webDriver.SafeConnect(() =>
        {
            _webDriver.SetPage(itemUrl);
            try
            {
                var price = _webDriver.ReadFromElement(ExistingBuyOrderQuantityLabel, true);
                return ParsePrice(price);
            }
            catch
            {
                return new double?();
            }
        });
    }

    private const string ExistingBuyOrderCancelButtonId = "//*[@id='tabContentsMyListings']/div/div[2]/div[5]/div/a";
    private static readonly By ExistingBuyOrderCancelButton = By.XPath(ExistingBuyOrderCancelButtonId);

    public async Task<bool> CancelBuyOrderAsync(string itemUrl)
    {
        return await _webDriver.SafeConnect(() =>
        {
            _webDriver.SetPage(itemUrl);
            _webDriver.ClickOnElement(ExistingBuyOrderCancelButton);
            return true;
        }, true);
    }

    #endregion

    #region ItemUrl

    public async Task<string> GetItemUrlAsync(string itemName)
    {
        return await _webDriver.SafeConnect(() =>
        {
            _webDriver.SetPage($"https://steamcommunity.com/market/listings/730/{itemName}");
            return _webDriver.GetCurrentPageUrl();
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
    private static readonly By LoginCheckTextBox = By.XPath("//*[@id='account_dropdown']/div/a[2]/span");

    #endregion

    public async Task<bool> LogIn(string login, string password, string secret)
    {
        Log.Information("Signing in...");
        Log.Information($"User {login}");

        if (IsAuthenticated(login))
            return true;

        LogOut();

        var token = await GetTokenAsync(secret);

        await _webDriver.SafeConnect(() =>
        {
            _webDriver.SetPage("https://steamcommunity.com/login/home/?goto=");
            _webDriver.WriteToElement(LoginTextBox, login);
            _webDriver.WriteToElement(PasswordTextBox, password);
            _webDriver.ClickOnElement(LoginButton);
            return true;
        }, true);

        try
        {
            _webDriver.WriteToElement(TwoFactorTextBox, token, true);
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
            _webDriver.ClickOnElement(LoginCheckButton, true);
            var currentLogin = _webDriver.ReadFromElement(LoginCheckTextBox, true);
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
        return await _webDriver.SafeConnect(() =>
        {
            _webDriver.SetPage("https://www.chescos.me/js-steam-authcode-generator/?");
            _webDriver.SendKey(SecretTextBox, secret);
            _webDriver.ClickOnElement(GenerateTokenButton);
            return _webDriver.ReadFromElement(TokenLabel);
        }, true);
    }

    private const string LogOutJScript = "javascript:Logout()";

    public void LogOut()
    {
        Log.Information("Signing out...");
        _webDriver.SetPage("https://steamcommunity.com");
        _webDriver.ExecuteJs(LogOutJScript);
        Log.Information("Successful sign out");
    }

    #endregion

    public void Dispose()
    {
        _webDriver.Dispose();
    }
}