using System;
using System.Collections.Generic;
using System.Linq;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer;

public class ItemBuilder
{
    private readonly SteamAPI _api;
    private ItemPage _itemPage;

    public ItemBuilder(SteamAPI api)
    {
        _api = api;
    }

    public ItemBuilder Create(string engItemName)
    {
        _itemPage = new ItemPage { EngItemName = engItemName };
        return this;
    }

    public ItemPage Build()
    {
        return _itemPage;
    }

    public ItemBuilder SetMyBuyOrder()
    {
        var quantity = _api.GetBuyOrderQuantity(_itemPage.ItemUrl);
        var price = _api.GetBuyOrderPrice(_itemPage.ItemUrl);

        if (!quantity.HasValue || !price.HasValue)
            _itemPage.MyBuyOrder = null;

        _itemPage.MyBuyOrder = new BuyOrder
        {
            EngItemName = _itemPage.EngItemName,
            RusItemName = _itemPage.RusItemName,
            ItemUrl = _itemPage.ItemUrl,
            Price = price!.Value,
            Quantity = quantity!.Value
        };
        return this;
    }

    public ItemBuilder SetMySellOrders(int quantity)
    {
        
        return this;
    }

    public ItemBuilder SetBalance()
    {
        _itemPage.Balance = _api.GetBalance();
        return this;
    }

    public ItemBuilder SetItemUrl()
    {
        _itemPage.ItemUrl = _api.GetItemUrl(_itemPage.EngItemName);
        return this;
    }

    public ItemBuilder SetRusItemName()
    {
        _itemPage.RusItemName = _api.GetRusItemName(_itemPage.ItemUrl);
        return this;
    }

    public ItemBuilder SetGraph(int analysisInterval)
    {
        var fromDate = DateTime.Now.AddDays(-analysisInterval);
        _itemPage.Graph = _api
            .GetGraph(_itemPage.ItemUrl)
            .SkipWhile(x => x.Date < fromDate)
            .ToList();
        return this;
    }

    public ItemBuilder SetItemSales()
    {
        _itemPage.Sales = SalesPerDay(_itemPage.Graph);
        return this;
    }

    private static double SalesPerDay(IEnumerable<SteamAPI.PointInfo> graph)
    {
        return graph
            .GroupBy(x => x.Date.Date)
            .Select(x => x.Sum(y => y.Quantity))
            .Average();
    }

    public ItemBuilder SetAvgPrice()
    {
        _itemPage.AvgPrice = AveragePrice(_itemPage.Graph);
        return this;
    }

    private static double AveragePrice(IReadOnlyCollection<SteamAPI.PointInfo> graph)
    {
        return graph.Sum(x => x.Price * x.Quantity) / graph.Sum(x => x.Quantity);
    }

    public ItemBuilder SetTrend()
    {
        _itemPage.Trend = PriceTrend(_itemPage.Graph);
        return this;
    }

    private static double PriceTrend(IEnumerable<SteamAPI.PointInfo> graph)
    {
        //m = ∑ (x - AVG(x)(y - AVG(y)) / ∑ (x - AVG(x))²
        var localGraph = graph.ToList();

        var avgX = localGraph.Average(x => x.Date.ToOADate());
        var avgY = localGraph.Average(x => x.Price * x.Quantity);

        var item1 = localGraph.Sum(x => (x.Date.ToOADate() - avgX) * ((x.Price * x.Quantity) - avgY));
        var item2 = localGraph.Sum(x => Math.Pow(x.Date.ToOADate() - avgX, 2));

        return item1 / item2;
    }

    public ItemBuilder SetBuyOrderBook()
    {
        _itemPage.BuyOrderBook = _api.GetBuyOrdersBook(_itemPage.ItemUrl);
        return this;
    }

    public ItemBuilder SetSellOrderBook(int listingFindRange)
    {
        _itemPage.SellOrderBook = _api.GetSellOrdersBook(_itemPage.ItemUrl, listingFindRange);
        return this;
    }
}