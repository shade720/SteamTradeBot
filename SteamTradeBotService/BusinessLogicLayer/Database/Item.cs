using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Globalization;

namespace SteamTradeBotService.BusinessLogicLayer.Database;

public class Item
{
    [Key]
    public int Id { get; set; }
    public int ItemId { get; set; }
    public string EngItemName { get; set; }
    public string RusItemName { get; set; }
    public double BuyPrice { get; set; }
    public double SellPrice { get; set; }
    public double AvgPrice { get; set; }
    public double Trend { get; set; }
    public double Sales { get; set; }
    public bool IsBeingPurchased { get; set; }
    public int BuyOrderQuantity { get; set; }
    public static double Balance { get; set; }
    public Priority ItemPriority { get; private set; } = Priority.ForReview;

    #region NotMapped

    [NotMapped] 
    private IConfiguration _configuration;

    [NotMapped]
    private SteamAPI _steamApi;

    [NotMapped]
    private DatabaseClient _database;

    [NotMapped] 
    public List<SteamAPI.OrderBookItem> SellOrderBook;

    [NotMapped]
    public List<SteamAPI.OrderBookItem> BuyOrderBook;
    [NotMapped] 
    private string ItemUrl { get; set; }

    #endregion

    public Item SetExecutionProperties(IConfiguration configuration, SteamAPI steamApi, DatabaseClient database)
    {
        _configuration = configuration;
        _steamApi = steamApi;
        _database = database;
        return this;
    }

    public void CollectItemData()
    {
        Log.Information("Collect item data...");
        ItemUrl ??= _steamApi.GetUrl(EngItemName);
        RusItemName ??= _steamApi.GetRusItemName(ItemUrl);
        Balance = _steamApi.GetBalance(ItemUrl);

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
        
        Log.Information($"Collected item data EngItemName: {EngItemName}; Balance: {Balance}; Sales: {Sales}; AvgPrice: {AvgPrice}; Trend: {Trend}; BestSellPrice: {SellOrderBook[0].Price}; BestBuyPrice: {BuyOrderBook[0].Price};");

        _database.UpdateItem(this);
    }

    public bool IsProfitable()
    {
        Log.Information("Checking item profit...");
        if (Sales < int.Parse(_configuration["Sales"]!))
        {
            Log.Information("Item is not profitable. Reason: sales volume is lower than needed.");
            return false;
        }
        if (AvgPrice > double.Parse(_configuration["AvgPrice"]!, NumberStyles.Any, CultureInfo.InvariantCulture))
        {
            Log.Information("Item is not profitable. Reason: average price is higher than needed.");
            return false;
        }
        if (Trend < double.Parse(_configuration["Trend"]!, NumberStyles.Any, CultureInfo.InvariantCulture))
        {
            Log.Information("Item is not profitable. Reason: trend is lower than needed.");
            return false;
        }

        var profitableBuyOrder= BuyOrderBook?.Find(buyOrder => SellOrderBook.Any(sellOrder => buyOrder.Price < sellOrder.Price * (1 - 0.13)));
        if (profitableBuyOrder is null)
        {
            Log.Information("Item is not profitable. Reason: profitable buy order is not found");
            return false;
        }

        BuyPrice = profitableBuyOrder.Price;
        SellPrice = profitableBuyOrder.Price * (1 - 0.13) + 0.1;

        Log.Information($"Item is profitable! BuyPrice: {BuyPrice}; SellPrice: {SellPrice};");
        _database.UpdateItem(this);
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
        IsBeingPurchased = false;
        ItemPriority = Priority.SellOrder;
    }

    public void CancelBuyOrder()
    {
        Log.Information($"Cancel buy order of {BuyOrderQuantity} {RusItemName} at price {BuyPrice}");
        _steamApi.CancelBuyOrder(ItemUrl);
        BuyOrderQuantity = 0;
        BuyPrice = 0;
        SellPrice = 0;
        IsBeingPurchased = false;
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