using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.SellRules;

public sealed class CurrentQuantityCheckRule : ISellRule
{
    private readonly IConfigurationManager _configurationManager;
    private readonly OrdersDbAccess _ordersDb;

    public CurrentQuantityCheckRule(IConfigurationManager configurationManager, OrdersDbAccess ordersDb)
    {
        _configurationManager = configurationManager;
        _ordersDb = ordersDb;
    }

    public bool IsFollowed(ItemPage itemPage)
    {
        throw new System.NotImplementedException();
    }

    public async Task<bool> IsFollowedAsync(ItemPage itemPage)
    {
        var localOrder = await _ordersDb.GetOrderAsync(itemPage.EngItemName, _configurationManager.ApiKey, OrderType.BuyOrder);
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