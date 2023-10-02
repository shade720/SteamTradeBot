using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.ItemModel;
using System.Linq;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;

public sealed class AvailableBalanceRule : IBuyRule
{
    private readonly IConfigurationService _configurationService;
    private readonly OrdersRepository _ordersRepository;

    public AvailableBalanceRule(IConfigurationService configurationService, OrdersRepository ordersRepository)
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