﻿using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.ItemModel;
using System;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Solutions;

public sealed class CancelMarketSolution : MarketSolution
{
    public CancelMarketSolution(
        ISteamApi api,
        IConfigurationService configurationService,
        IEventService eventService,
        OrdersRepository ordersRepository)
        : base(api, configurationService, eventService, ordersRepository) { }

    public override void Perform(ItemPage itemPage)
    {
        throw new NotImplementedException();
    }

    public override async Task PerformAsync(ItemPage itemPage)
    {
        Log.Information("Cancelling buy order ({0})...", itemPage.EngItemName);
        if (itemPage.MyBuyOrder is null)
            throw new Exception("Can't cancel buy order. MyBuyOrder is null.");

        var order = await OrdersRepository.GetOrderAsync(itemPage.EngItemName, ConfigurationService.ApiKey, OrderType.BuyOrder);
        if (order is null)
            throw new Exception("Can't load stored buy order from database.");

        var successfullyCanceled = await SteamApi.CancelBuyOrderAsync(itemPage.MyBuyOrder.ItemUrl);
        if (successfullyCanceled)
        {
            await OrdersRepository.RemoveOrderAsync(order);
            await EventService.OnItemCancellingAsync(order);
            Log.Information("Buy order {0} (Price: {1}, Quantity: {2}) has been canceled.",
                order.EngItemName, order.BuyPrice, order.Quantity);
        }
        else
        {
            Log.Logger.Warning("Can't cancel item {0} (Price: {1}, Quantity: {2})",
                order.EngItemName, order.BuyPrice, order.Quantity);
        }
    }
}