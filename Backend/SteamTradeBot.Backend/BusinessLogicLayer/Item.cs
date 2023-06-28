using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace SteamTradeBot.Backend.BusinessLogicLayer;

public class Item
{
    [Key]
    public int Id { get; set; }

    [Required(AllowEmptyStrings = true)]
    public string EngItemName { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = true)]
    public string RusItemName { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = true)]
    private string ItemUrl { get; set; } = string.Empty;
    public double BuyPrice { get; set; }
    public double SellPrice { get; set; }
    public double AvgPrice { get; set; }
    public double Trend { get; set; }
    public double Sales { get; set; }
    public bool IsTherePurchaseOrder { get; private set; }
    public int BuyOrderQuantity { get; set; }

    #region NotMapped

    [NotMapped]
    private IConfiguration? _configuration;

    [NotMapped]
    private SteamAPI? _steamApi;

    [NotMapped]
    public List<SteamAPI.OrderBookItem>? SellOrderBook;

    [NotMapped]
    public List<SteamAPI.OrderBookItem>? BuyOrderBook;

    #endregion

    public Item ConfigureServiceProperties(IConfiguration configuration, SteamAPI steamApi)
    {
        _configuration = configuration;
        _steamApi = steamApi;
        return this;
    }

    public void CollectItemData(out double balance)
    {
        if (_steamApi is null)
            throw new NullReferenceException("Steam Api client was null");
        if (_configuration is null)
            throw new NullReferenceException($"Configuration was null {nameof(CollectItemData)}");
        balance = 0;
        Log.Information("Collect item data...");
        if (string.IsNullOrEmpty(ItemUrl))
            ItemUrl = _steamApi.GetItemUrl(EngItemName);
        if (string.IsNullOrEmpty(RusItemName))
            RusItemName = _steamApi.GetRusItemName(ItemUrl);
        var graphAnalysisPeriod = int.Parse(_configuration["AnalysisIntervalDays"]!);
        var fromDate = DateTime.Now.AddDays(-graphAnalysisPeriod);
        var graph = _steamApi
            .GetGraph(ItemUrl)
            .SkipWhile(x => x.Date < fromDate)
            .ToList();

        Sales = SalesPerDay(graph);
        AvgPrice = AveragePrice(graph);
        Trend = PriceTrend(graph);

        BuyOrderBook = _steamApi.GetBuyOrdersBook(ItemUrl);
        SellOrderBook = _steamApi.GetSellOrdersBook(ItemUrl, int.Parse(_configuration["ListingFindRange"]!));

        Log.Information("Collected item data:\r\nEngItemName: {0}; \r\nSales: {1}; \r\nAvgPrice: {2}; \r\nTrend: {3}; \r\nBestSellPrice: {4}; \r\nBestBuyPrice: {5};", EngItemName, Sales, AvgPrice, Trend, SellOrderBook[0].Price, BuyOrderBook[0].Price);
        balance = _steamApi.GetBalance(ItemUrl);
    }

    public bool IsProfitable(double balance)
    {
        Log.Information("Checking item profit...");
        if (SellOrderBook is null || BuyOrderBook is null)
            throw new NullReferenceException("Can't check profit. SellOrderBook or BuyOrderBook was null. ");
        if (_configuration is null)
            throw new NullReferenceException($"Configuration was null {nameof(IsProfitable)}");

        Log.Information("Checking sales per week...");
        var salesForCompare = int.Parse(_configuration["SalesPerWeek"]!);
        if (Sales < salesForCompare)
        {
            Log.Information("Item is not profitable. Reason: sales volume is lower than needed. {0}", salesForCompare);
            return false;
        }

        Log.Information("Checking average price...");
        var avgPriceRange = double.Parse(_configuration["AveragePrice"]!, NumberStyles.Any, CultureInfo.InvariantCulture);
        if (SellOrderBook.All(sellOrder => sellOrder.Price > AvgPrice + avgPriceRange))
        {
            Log.Information("Item is not profitable. Reason: average price is higher than needed. {0}", avgPriceRange);
            return false;
        }

        Log.Information("Checking trend...");
        var trendForCompare = double.Parse(_configuration["Trend"]!, NumberStyles.Any, CultureInfo.InvariantCulture);
        if (Trend < trendForCompare)
        {
            Log.Information("Item is not profitable. Reason: trend is lower than needed. {0}", trendForCompare);
            return false;
        }

        var steamCommission = double.Parse(_configuration["SteamCommission"]!, NumberStyles.Any, CultureInfo.InvariantCulture);
        var requiredProfit = double.Parse(_configuration["RequiredProfit"]!, NumberStyles.Any, CultureInfo.InvariantCulture);

        Log.Information("Finding profitable order...");
        var profitableBuyOrder = SellOrderBook.FirstOrDefault(sellOrder => BuyOrderBook.Take(5).Any(buyOrder => buyOrder.Price < sellOrder.Price * (1 - steamCommission)));

        if (profitableBuyOrder is null)
        {
            Log.Information("Item is not profitable. Reason: profitable sell order is not found. Required profit: {0}, Commission: {1}", requiredProfit, steamCommission);
            return false;
        }

        Log.Information("Checking available balance...");
        var availableBalance = double.Parse(_configuration["AvailableBalance"]!, NumberStyles.Any, CultureInfo.InvariantCulture);
        if (balance - availableBalance < profitableBuyOrder.Price)
        {
            Log.Information("Item is not profitable. Reason: no money for this item. Available balance: {0}, Price: {1}", balance - availableBalance, profitableBuyOrder.Price);
            return false;
        }

        BuyPrice = profitableBuyOrder.Price;
        SellPrice = profitableBuyOrder.Price * (1 - steamCommission) + requiredProfit;

        Log.Information("Item is profitable! BuyPrice: {0}; SellPrice: {1};", BuyPrice, SellPrice);
        return true;
    }

    public bool IsBuyOrderSatisfied()
    {
        if (BuyOrderQuantity <= 0 || !IsTherePurchaseOrder)
            return false;
        Log.Information("Checking if order satisfied...");
        return BuyOrderQuantity <= _steamApi.GetBuyOrderQuantity(ItemUrl);
    }

    public bool IsBuyOrderObsolete()
    {
        if (BuyOrderQuantity <= 0)
            return false;
        Log.Information("Checking if order obsolete...");
        const int listingPageSize = 1;
        var sellOrderBook = _steamApi.GetSellOrdersBook(ItemUrl, listingPageSize);
        return sellOrderBook.Any(x => Math.Abs(x.Price - BuyPrice) < double.Parse(_configuration["FitPriceRange"]!, NumberStyles.Any, CultureInfo.InvariantCulture));
    }

    public void Buy()
    {
        var quantity = int.Parse(_configuration["OrderQuantity"]!);
        Log.Information($"Buy {quantity} {RusItemName} at price {BuyPrice}");
        _steamApi.PlaceBuyOrder(ItemUrl, BuyPrice, quantity);
        BuyOrderQuantity = quantity;
        IsTherePurchaseOrder = true;
    }

    public void Sell()
    {
        var currentQuantity = _steamApi.GetBuyOrderQuantity(ItemUrl);
        while (BuyOrderQuantity != currentQuantity)
        {
            for (var i = 0; i < BuyOrderQuantity - currentQuantity; i++)
            {
                Log.Information("Sell {0} at price {1}", EngItemName, SellPrice);
                _steamApi.PlaceSellOrder(EngItemName, SellPrice, _configuration["SteamUserId"]!);
                BuyOrderQuantity -= 1;
            }
        }
        if (BuyOrderQuantity != 0) return;
        Log.Information("All {0} has sold", RusItemName);
        BuyPrice = 0;
        SellPrice = 0;
        IsTherePurchaseOrder = false;
    }

    public void CancelBuyOrder()
    {
        Log.Information("Cancel buy order of {0} {1} at price {2}", BuyOrderQuantity, EngItemName, BuyPrice);
        _steamApi.CancelBuyOrder(ItemUrl);
        BuyOrderQuantity = 0;
        BuyPrice = 0;
        SellPrice = 0;
        IsTherePurchaseOrder = false;
    }

    public override string ToString()
    {
        return EngItemName;
    }

    #region Private

    #region GraphAnalyze

    private static double AveragePrice(List<SteamAPI.PointInfo> graph)
    {
        return graph.Sum(x => x.Price * x.Quantity) / graph.Sum(x => x.Quantity);
    }

    private static double SalesPerDay(IEnumerable<SteamAPI.PointInfo> graph)
    {
        return graph
            .GroupBy(x => x.Date.Date)
            .Select(x => x.Sum(y => y.Quantity))
            .Average();
    }

    private static double PriceTrend(IEnumerable<SteamAPI.PointInfo> graph)
    {
        //m = ∑ (x - AVG(x)(y - AVG(y)) / ∑ (x - AVG(x))²
        var localGraph = graph.ToList();

        var avgX = localGraph.Average(x => x.Date.AddYears(-2000).Ticks);
        var avgY = localGraph.Average(x => x.Price);

        var item1 = localGraph.Sum(x => (x.Date.Ticks - avgX) * (x.Price - avgY));
        var item2 = localGraph.Sum(x => Math.Pow(x.Date.Ticks - avgX, 2));

        return item1 / item2;
    }

    #endregion

    #endregion
}