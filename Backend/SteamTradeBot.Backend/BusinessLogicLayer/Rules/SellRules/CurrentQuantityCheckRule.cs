using System.Linq;
using Serilog;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.SellRules;

public class CurrentQuantityCheckRule : ISellRule
{
    private readonly DbAccess _db;

    public CurrentQuantityCheckRule(DbAccess db)
    {
        _db = db;
    }
    public bool IsFollowed(ItemPage itemPage)
    {
        Log.Information("Checking if order satisfied...");
        var localOrder = _db.GetBuyOrders().FirstOrDefault(order => order.EngItemName == itemPage.EngItemName);
        if (localOrder is null)
            return false;

        return itemPage.MyBuyOrder is null || itemPage.MyBuyOrder.Quantity < localOrder.Quantity;
    }
}