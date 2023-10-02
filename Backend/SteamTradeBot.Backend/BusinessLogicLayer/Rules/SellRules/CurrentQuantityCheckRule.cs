﻿using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.ItemModel;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.SellRules;

public sealed class CurrentQuantityCheckRule : ISellRule
{
    private readonly IConfigurationService _configurationService;
    private readonly OrdersRepository _ordersRepository;

    public CurrentQuantityCheckRule(IConfigurationService configurationService, OrdersRepository ordersRepository)
    {
        _configurationService = configurationService;
        _ordersRepository = ordersRepository;
    }

    public bool IsFollowed(ItemPage itemPage)
    {
        throw new System.NotImplementedException();
    }

    public async Task<bool> IsFollowedAsync(ItemPage itemPage)
    {
        var localOrder = await _ordersRepository.GetOrderAsync(itemPage.EngItemName, _configurationService.ApiKey, OrderType.BuyOrder);
        if (localOrder is null)
        {
            Log.Logger.Information("Can't check if buy order was executed. No stored buy orders for this item.");
            return false;
        }

        if (itemPage.MyBuyOrder is not null && itemPage.MyBuyOrder.Quantity >= localOrder.Quantity)
        {
            Log.Logger.Information("Buy order not executed");
            return false;
        }

        Log.Logger.Information("Buy order partially or fully executed!!!. Current order quantity is lower than stored.");
        return true;
    }
}