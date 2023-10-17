using Serilog;
using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.Abstractions.Rules;
using SteamTradeBot.Backend.Domain.ItemModel;

namespace SteamTradeBot.Backend.Application.Rules.SellRules;

internal sealed class CurrentQuantityCheckRule : ISellRule
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