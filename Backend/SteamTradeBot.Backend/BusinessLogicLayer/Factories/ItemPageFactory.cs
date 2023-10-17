using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.ItemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Factories;

public sealed class ItemPageFactory
{
    private readonly ISteamApi _api;
    private readonly IConfigurationService _configurationService;

    public ItemPageFactory(
        ISteamApi api,
        IConfigurationService configurationService)
    {
        _api = api;
        _configurationService = configurationService;
    }

    public async Task<ItemPage> CreateAsync(string engItemName)
    {
        var itemPage = new ItemPage { EngItemName = engItemName };
        itemPage.ItemUrl = await _api.GetItemUrlAsync(itemPage.EngItemName);
        itemPage.RusItemName = await _api.GetRusItemNameAsync(itemPage.ItemUrl);

        var quantity = await _api.GetBuyOrderQuantityAsync(itemPage.ItemUrl);
        var price = await _api.GetBuyOrderPriceAsync(itemPage.ItemUrl);

        if (quantity.HasValue && price.HasValue)
        {
            itemPage.MyBuyOrder = new Order
            {
                EngItemName = itemPage.EngItemName,
                RusItemName = itemPage.RusItemName,
                ItemUrl = itemPage.ItemUrl,
                BuyPrice = price.Value,
                Quantity = quantity.Value
            };
        }

        var fromDate = DateTime.Now.AddDays(-_configurationService.AnalysisIntervalDays);
        itemPage.SalesChart = await _api.GetChartAsync(itemPage.ItemUrl, fromDate);

        itemPage.SellOrderBook = await _api.GetSellOrdersBookAsync(itemPage.ItemUrl, _configurationService.SellListingFindRange);

        var salesPerDay = itemPage.SalesChart
            .GroupBy(x => x.Date.Date)
            .Select(x => x.Sum(y => y.Quantity))
            .Average();

        itemPage.BuyOrderBook = (await _api.GetBuyOrdersBookAsync(itemPage.ItemUrl, 5))
            .OrderByDescending(x => x.Price)
            .Aggregate(new List<OrderBookItem>(), (resultOrderBook, orderBookItem) =>
            {
                var salesToCompare = resultOrderBook.Sum(x => x.Quantity);
                if (salesToCompare < salesPerDay * _configurationService.SalesRatio)
                    resultOrderBook.Add(orderBookItem);
                return resultOrderBook;
            });
        itemPage.CurrentBalance = await _api.GetBalanceAsync();
        return itemPage;
    }
}