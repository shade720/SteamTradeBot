using System;
using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Factories;

public class ItemPageFactory
{
    private readonly ISteamApi _api;
    private readonly IConfigurationManager _configurationManager;

    public ItemPageFactory(ISteamApi api, IConfigurationManager configurationManager)
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
        itemPage.SalesChart = await _api.GetGraphAsync(itemPage.ItemUrl, fromDate);

        itemPage.BuyOrderBook = await _api.GetBuyOrdersBookAsync(itemPage.ItemUrl, _configurationManager.BuyListingFindRange);
        itemPage.SellOrderBook = await _api.GetSellOrdersBookAsync(itemPage.ItemUrl, _configurationManager.SellListingFindRange);

        return itemPage;
    }
}