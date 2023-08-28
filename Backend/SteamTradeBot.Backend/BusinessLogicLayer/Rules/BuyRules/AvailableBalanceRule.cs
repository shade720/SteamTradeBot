using System.Linq;
using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;

public sealed class AvailableBalanceRule : IBuyRule
{
    private readonly IConfigurationManager _configurationManager;
    private readonly OrdersDbAccess _ordersDbAccess;

    public AvailableBalanceRule(IConfigurationManager configurationManager, OrdersDbAccess ordersDbAccess)
    {
        _configurationManager = configurationManager;
        _ordersDbAccess = ordersDbAccess;
    }
    public bool IsFollowed(ItemPage itemPage)
    {
        throw new System.NotImplementedException();
    }

    public async Task<bool> IsFollowedAsync(ItemPage itemPage)
    {
        var balanceInWork = (await _ordersDbAccess.GetOrdersAsync(_configurationManager.ApiKey, OrderType.BuyOrder)).Sum(buyOrder => buyOrder.BuyPrice);
        var availableBalance = (itemPage.CurrentBalance - balanceInWork ) * _configurationManager.AvailableBalance;

        if (availableBalance > (itemPage.EstimatedBuyPrice ?? 0))
        {
            Log.Information("Available balance is ok.");
            return true;
        }
        Log.Information("Available balance is bad. No money for this item. Available balance: {0} < Price: {1}", availableBalance, itemPage.EstimatedBuyPrice);
        return false;
    }
}