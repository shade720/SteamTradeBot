using System;
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

    public ItemPage Create(string engItemName)
    {
        Log.Information("Get {0} page...", engItemName);
        var itemPage = new ItemPage { EngItemName = engItemName };
        itemPage.ItemUrl = _api.GetItemUrl(itemPage.EngItemName);
        itemPage.RusItemName = _api.GetRusItemName(itemPage.ItemUrl);

        var quantity = _api.GetBuyOrderQuantity(itemPage.ItemUrl);
        var price = _api.GetBuyOrderPrice(itemPage.ItemUrl);

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
        itemPage.SalesChart = _api.GetGraph(itemPage.ItemUrl, fromDate);

        itemPage.BuyOrderBook = _api.GetBuyOrdersBook(itemPage.ItemUrl, _configurationManager.BuyListingFindRange);
        itemPage.SellOrderBook = _api.GetSellOrdersBook(itemPage.ItemUrl, _configurationManager.SellListingFindRange);

        return itemPage;
    }
}