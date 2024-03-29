﻿using Serilog;
using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.OrderAggregate;

namespace SteamTradeBot.Backend.Application.Services;

public class OrderCancellingService
{
    private readonly ISteamApi _api;
    private readonly IOrdersRepository _ordersRepository;

    public OrderCancellingService(ISteamApi api, IOrdersRepository ordersRepository)
    {
        _api = api;
        _ordersRepository = ordersRepository;
    }

    public async Task ClearBuyOrdersAsync(string apiKey)
    {
        Log.Logger.Information("Start buy orders canceling...");
        var buyOrders = await _ordersRepository.GetOrdersAsync(apiKey, OrderType.BuyOrder);
        foreach (var order in buyOrders)
        {
            Log.Logger.Information("Cancelling item {0} (Price: {1}, Quantity: {2})",
                order.EngItemName, order.BuyPrice, order.Quantity);
            var successfullyCanceled = await _api.CancelBuyOrderAsync(order.ItemUrl);
            if (successfullyCanceled)
            {
                await _ordersRepository.RemoveOrderAsync(order);
                Log.Logger.Information("Item {0} (Price: {1}, Quantity: {2}) was canceled",
                    order.EngItemName, order.BuyPrice, order.Quantity);
            }
            else
            {
                Log.Logger.Warning("Can't cancel item {0} (Price: {1}, Quantity: {2})",
                    order.EngItemName, order.BuyPrice, order.Quantity);
            }
        }
        Log.Logger.Information("All buy orders have been canceled!");
    }
}