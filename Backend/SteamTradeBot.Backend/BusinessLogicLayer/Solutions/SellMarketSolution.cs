﻿using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models;
using System;
using System.Linq;
using Serilog;
using SteamTradeBot.Backend.Services;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Solutions;

public class SellMarketSolution : MarketSolution
{
    public SellMarketSolution(SteamAPI api, MarketDbAccess marketDb, ConfigurationManager configurationManager) : base(api, marketDb, configurationManager) { }

    public override void Perform(ItemPage itemPage)
    {
        var buyOrder = MarketDb.GetBuyOrders().FirstOrDefault(x => x.EngItemName == itemPage.EngItemName);
        if (buyOrder is null)
            throw new Exception("Can't find local buy order for sell order forming");
        var currentConfiguration = ConfigurationManager.GetConfiguration();
        var sellOrder = new SellOrder
        {
            EngItemName = itemPage.EngItemName,
            RusItemName = itemPage.RusItemName,
            ItemUrl = itemPage.ItemUrl,
            Price = buyOrder.Price * (1 + currentConfiguration.SteamCommission) + currentConfiguration.RequiredProfit,
            Quantity = itemPage.MyBuyOrder is null ? buyOrder.Quantity : buyOrder.Quantity - itemPage.MyBuyOrder.Quantity
        };

        for (var i = 0; i < sellOrder.Quantity; i++)
        {
            if (SteamApi.PlaceSellOrder(sellOrder.EngItemName, sellOrder.Price, currentConfiguration.SteamUserId))
                Log.Information("Place sell order {0} (Price: {1})", sellOrder.EngItemName, sellOrder.Price);
            else
                Log.Error("Can't place sell order {0} (Price: {1}). Item not found in inventory!", sellOrder.EngItemName, sellOrder.Price);
        }

        if (itemPage.MyBuyOrder is null)
        {
            MarketDb.RemoveBuyOrder(buyOrder);
        }
        if (itemPage.MyBuyOrder is not null && itemPage.MyBuyOrder.Quantity > 0)
        {
            buyOrder.Quantity = itemPage.MyBuyOrder.Quantity;
            MarketDb.AddOrUpdateBuyOrder(buyOrder);
        }
        MarketDb.AddOrUpdateSellOrder(sellOrder);
    }
}