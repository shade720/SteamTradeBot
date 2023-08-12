using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer;
using SteamTradeBot.Backend.DataAccessLayer;

namespace SteamTradeBot.Backend.Services;

public class OrderCancellingService
{
    private readonly SteamAPI _api;
    private readonly MarketDbAccess _marketDb;

    public OrderCancellingService(SteamAPI api, MarketDbAccess marketDb)
    {
        _api = api;
        _marketDb = marketDb;
    }

    public void ClearBuyOrders()
    {
        Log.Logger.Information("Start buy orders canceling...");
        var itemsWithPurchaseOrders = _marketDb.GetBuyOrders();
        foreach (var order in itemsWithPurchaseOrders)
        {
            Log.Logger.Information("Cancelling item {0} with buy price {1}", order.EngItemName, order.Price);
            _api.CancelBuyOrder(order.ItemUrl);
            _marketDb.RemoveBuyOrder(order);
            Log.Logger.Information("Item {0} with buy price {1} was canceled", order.EngItemName, order.Price);
        }
        Log.Logger.Information("All buy orders have been canceled!");
    }
}