using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.SellRules;

public class CurrentQuantityCheckRule : ISellRule
{
    private readonly MarketDbAccess _marketDb;

    public CurrentQuantityCheckRule(MarketDbAccess marketDb)
    {
        _marketDb = marketDb;
    }

    public bool IsFollowed(ItemPage itemPage)
    {
        throw new System.NotImplementedException();
    }

    public async Task<bool> IsFollowedAsync(ItemPage itemPage)
    {
        Log.Information("Checking if order satisfied...");
        var localOrder = await _marketDb.GetBuyOrderAsync(itemPage.EngItemName, itemPage.UserName);
        return localOrder is not null && (itemPage.MyBuyOrder is null || itemPage.MyBuyOrder.Quantity < localOrder.Quantity);
    }
}