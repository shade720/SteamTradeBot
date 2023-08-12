using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models;
using SteamTradeBot.Backend.Services;
using System;
using System.Linq;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Solutions;

public class BuyMarketSolution : MarketSolution
{
    public BuyMarketSolution(SteamAPI api, MarketDbAccess marketDb, ConfigurationManager configurationManager) : base(api, marketDb, configurationManager) { }

    public override void Perform(ItemPage itemPage)
    {
        var currentConfiguration = ConfigurationManager.GetConfiguration();
        var price = itemPage.BuyOrderBook.FirstOrDefault(buyOrder => itemPage.SellOrderBook.Any(sellOrder => sellOrder.Price + currentConfiguration.RequiredProfit > buyOrder.Price * (1 + currentConfiguration.SteamCommission)));
        if (price is null)
            throw new Exception("Sell order not found");
        var buyOrder = new BuyOrder
        {
            EngItemName = itemPage.EngItemName,
            RusItemName = itemPage.RusItemName,
            ItemUrl = itemPage.ItemUrl,
            Price = price.Price,
            Quantity = currentConfiguration.OrderQuantity
        };
        SteamApi.PlaceBuyOrder(buyOrder.ItemUrl, buyOrder.Price, buyOrder.Quantity);
        MarketDb.AddOrUpdateBuyOrder(buyOrder);
    }
}