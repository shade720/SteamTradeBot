using Serilog;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Factories;

public class ItemPageFactory
{
    private readonly ISteamApi _api;
    private readonly IConfigurationManager _configurationManager;

    public ItemPageFactory(
        ISteamApi api,
        IConfigurationManager configurationManager)
    {
        _api = api;
        _configurationManager = configurationManager;
    }

    public async Task<ItemPage> CreateAsync(string engItemName)
    {
        Log.Information("Get {0} page...", engItemName);
        var itemPage = new ItemPage { EngItemName = engItemName };
        itemPage.ItemUrl = await _api.GetItemUrlAsync(itemPage.EngItemName);
        itemPage.RusItemName = await _api.GetRusItemNameAsync(itemPage.ItemUrl);

        var quantity = await _api.GetBuyOrderQuantityAsync(itemPage.ItemUrl);
        var price = await _api.GetBuyOrderPriceAsync(itemPage.ItemUrl);

        if (quantity.HasValue && price.HasValue)
        {
            itemPage.MyBuyOrder = new BuyOrder
            {
                EngItemName = itemPage.EngItemName,
                RusItemName = itemPage.RusItemName,
                ItemUrl = itemPage.ItemUrl,
                Price = price.Value,
                Quantity = quantity.Value
            };
        }

        var fromDate = DateTime.Now.AddDays(-_configurationManager.AnalysisIntervalDays);
        itemPage.SalesChart = await _api.GetChartAsync(itemPage.ItemUrl, fromDate);

        itemPage.SellOrderBook = await _api.GetSellOrdersBookAsync(itemPage.ItemUrl, _configurationManager.SellListingFindRange);
        itemPage.BuyOrderBook = (await _api.GetBuyOrdersBookAsync(itemPage.ItemUrl, 5))
            .Aggregate(new List<OrderBookItem>(), (list, order) =>
            {
                if (list.Sum(x => x.Quantity) < _configurationManager.SalesPerDay * _configurationManager.SalesRatio)
                    list.Add(order);
                return list;
            })
            .SkipLast(1)
            .OrderByDescending(x => x.Price)
            .ToList();

        return itemPage;
    }
}