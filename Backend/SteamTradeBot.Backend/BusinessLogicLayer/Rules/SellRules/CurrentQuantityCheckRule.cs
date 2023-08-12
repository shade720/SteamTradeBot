using System.Linq;
using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Abstractions;
using SteamTradeBot.Backend.DataAccessLayer;
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
        Log.Information("Checking if order satisfied...");
        var localOrder = _marketDb.GetBuyOrders().FirstOrDefault(order => order.EngItemName == itemPage.EngItemName);
        return localOrder is not null && (itemPage.MyBuyOrder is null || itemPage.MyBuyOrder.Quantity < localOrder.Quantity);
    }
}