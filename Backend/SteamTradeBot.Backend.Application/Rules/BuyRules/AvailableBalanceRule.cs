using Serilog;
using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.Abstractions.Rules;
using SteamTradeBot.Backend.Domain.ItemPageAggregate;
using SteamTradeBot.Backend.Domain.OrderAggregate;

namespace SteamTradeBot.Backend.Application.Rules.BuyRules;

internal sealed class AvailableBalanceRule : IBuyRule
{
    private readonly IConfigurationService _configurationService;
    private readonly IOrdersRepository _ordersRepository;

    public AvailableBalanceRule(IConfigurationService configurationService, IOrdersRepository ordersRepository)
    {
        _configurationService = configurationService;
        _ordersRepository = ordersRepository;
    }

    public bool IsFollowed(ItemPage itemPage)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsFollowedAsync(ItemPage itemPage)
    {
        var balanceInWork = (await _ordersRepository.GetOrdersAsync(_configurationService.ApiKey, OrderType.BuyOrder)).Sum(buyOrder => buyOrder.BuyPrice * buyOrder.Quantity);
        var availableBalance = (itemPage.CurrentBalance - balanceInWork) * _configurationService.AvailableBalance;

        if (availableBalance > (itemPage.EstimatedBuyPrice ?? 0))
        {
            Log.Information("Available balance is ok.");
            return true;
        }
        Log.Information("Available balance is bad. No money for this item. Available balance: {0} < Price: {1}", availableBalance, itemPage.EstimatedBuyPrice);
        return false;
    }
}