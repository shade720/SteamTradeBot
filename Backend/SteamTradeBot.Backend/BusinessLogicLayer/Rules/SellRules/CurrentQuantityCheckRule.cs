using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.SellRules;

public sealed class CurrentQuantityCheckRule : ISellRule
{
    private readonly IConfigurationManager _configurationManager;
    private readonly MarketDbAccess _marketDb;

    public CurrentQuantityCheckRule(IConfigurationManager configurationManager, MarketDbAccess marketDb)
    {
        _configurationManager = configurationManager;
        _marketDb = marketDb;
    }

    public bool IsFollowed(ItemPage itemPage)
    {
        throw new System.NotImplementedException();
    }

    public async Task<bool> IsFollowedAsync(ItemPage itemPage)
    {
        var localOrder = await _marketDb.GetBuyOrderAsync(itemPage.EngItemName, _configurationManager.ApiKey);
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