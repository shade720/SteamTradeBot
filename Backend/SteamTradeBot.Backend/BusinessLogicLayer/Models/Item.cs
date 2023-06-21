using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Models;

public class Item
{
    [Key]
    public int Id { get; set; }
    public string EngItemName { get; set; }
    public string RusItemName { get; set; }
    private string ItemUrl { get; set; }
    public double BuyPrice { get; set; }
    public double SellPrice { get; set; }
    public double AvgPrice { get; set; }
    public double Trend { get; set; }
    public double Sales { get; set; }
    public bool IsTherePurchaseOrder { get; private set; }
    public int BuyOrderQuantity { get; set; }
    public Priority ItemPriority { get; private set; } = Priority.ForReview;

    #region NotMapped

    [NotMapped]
    private IConfiguration _configuration;

    [NotMapped]
    private SteamAPI _steamApi;

    [NotMapped]
    public List<SteamAPI.OrderBookItem> SellOrderBook;

    [NotMapped]
    public List<SteamAPI.OrderBookItem> BuyOrderBook;

    #endregion

    public Item ConfigureServiceProperties(IConfiguration configuration, SteamAPI steamApi)
    {
        _configuration = configuration;
        _steamApi = steamApi;
        return this;
    }

    public double CollectItemData()
    {
        Log.Information("Collect item data...");
        ItemUrl ??= _steamApi.GetItemUrl(EngItemName);
        RusItemName ??= _steamApi.GetRusItemName(ItemUrl);
        var graphAnalysisPeriod = int.Parse(_configuration["AnalysisPeriod"]!);
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

        Log.Information($"Collected item data:\r\nEngItemName: {EngItemName}; \r\nSales: {Sales}; \r\nAvgPrice: {AvgPrice}; \r\nTrend: {Trend}; \r\nBestSellPrice: {SellOrderBook[0].Price}; \r\nBestBuyPrice: {BuyOrderBook[0].Price};");

        return _steamApi.GetBalance(ItemUrl);
    }

    public bool IsProfitable(double balance)
    {
        Log.Information("Checking item profit...");
        if (Sales < int.Parse(_configuration["Sales"]!))
        {
            Log.Information("Item is not profitable. Reason: sales volume is lower than needed.");
            return false;
        }

        var avgPriceRange = double.Parse(_configuration["AvgPrice"]!, NumberStyles.Any, CultureInfo.InvariantCulture);
        if (SellOrderBook.All(sellOrder => sellOrder.Price < AvgPrice + avgPriceRange))
        {
            Log.Information("Item is not profitable. Reason: average price is higher than needed.");
            return false;
        }
        if (Trend < double.Parse(_configuration["Trend"]!, NumberStyles.Any, CultureInfo.InvariantCulture))
        {
            Log.Information("Item is not profitable. Reason: trend is lower than needed.");
            return false;
        }

        var steamCommission = double.Parse(_configuration["SteamCommission"]!, NumberStyles.Any, CultureInfo.InvariantCulture);
        var requiredProfit = double.Parse(_configuration["RequiredProfit"]!, NumberStyles.Any, CultureInfo.InvariantCulture);

        var profitableBuyOrder = SellOrderBook
            .FirstOrDefault(sellOrder => BuyOrderBook.Take(5).Any(buyOrder => buyOrder.Price < sellOrder.Price * (1 - steamCommission)));

        if (profitableBuyOrder is null)
        {
            Log.Information("Item is not profitable. Reason: profitable sell order is not found");
            return false;
        }
        if (balance < profitableBuyOrder.Price)
        {
            Log.Information("Item is not profitable. Reason: no money for this item");
            return false;
        }

        BuyPrice = profitableBuyOrder.Price;
        SellPrice = profitableBuyOrder.Price * (1 - steamCommission) + requiredProfit;

        Log.Information($"Item is profitable! BuyPrice: {BuyPrice}; SellPrice: {SellPrice};");
        return true;
    }

    public bool IsBuyOrderSatisfied()
    {
        if (BuyOrderQuantity <= 0)
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
        return sellOrderBook.Any(x => Math.Abs(x.Price - BuyPrice) < double.Parse(_configuration["PriceRangeToCancel"]!, NumberStyles.Any, CultureInfo.InvariantCulture));
    }

    public void Buy()
    {
        var quantity = int.Parse(_configuration["BuyQuantity"]!);
        Log.Information($"Buy {quantity} {RusItemName} at price {BuyPrice}");
        _steamApi.PlaceBuyOrder(ItemUrl, BuyPrice, quantity);
        BuyOrderQuantity = quantity;
        ItemPriority = Priority.BuyOrder;
    }

    public void Sell()
    {
        var currentQuantity = _steamApi.GetBuyOrderQuantity(ItemUrl);
        while (BuyOrderQuantity != currentQuantity)
        {
            Log.Information($"Sell {RusItemName} at price {SellPrice}");
            _steamApi.PlaceSellOrder(EngItemName, SellPrice, _configuration["SteamUserId"]!);
            BuyOrderQuantity -= 1;
        }
        if (BuyOrderQuantity != 0) return;
        Log.Information($"All {RusItemName} has sold");
        BuyPrice = 0;
        SellPrice = 0;
        IsTherePurchaseOrder = false;
        ItemPriority = Priority.SellOrder;
    }

    public void CancelBuyOrder()
    {
        Log.Information($"Cancel buy order of {BuyOrderQuantity} {RusItemName} at price {BuyPrice}");
        _steamApi.CancelBuyOrder(ItemUrl);
        BuyOrderQuantity = 0;
        BuyPrice = 0;
        SellPrice = 0;
        IsTherePurchaseOrder = false;
        ItemPriority = Priority.ForReview;
    }

    public override string ToString()
    {
        return EngItemName;
    }

    #region Private

    #region GraphAnalyze

    private static double AveragePrice(IEnumerable<SteamAPI.PointInfo> graph)
    {
        return graph.Average(x => x.Price);
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