using System;
using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.Models.ItemModel;
using SteamTradeBot.Backend.Services;
using ConfigurationManager = SteamTradeBot.Backend.Services.ConfigurationManager;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Factories;

public class ItemPageFactory
{
    private readonly SteamAPI _api;
    private readonly ConfigurationManager _configurationManager;

    public ItemPageFactory(SteamAPI api, ConfigurationManager configurationManager)
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