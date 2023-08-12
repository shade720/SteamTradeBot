using System.Threading.Tasks;
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

    public async Task ClearBuyOrdersAsync()
    {
        Log.Logger.Information("Start buy orders canceling...");
        var itemsWithPurchaseOrders = await _marketDb.GetBuyOrdersAsync();
        foreach (var order in itemsWithPurchaseOrders)
        {
            Log.Logger.Information("Cancelling item {0} with buy price {1}", order.EngItemName, order.Price);
            await _api.CancelBuyOrderAsync(order.ItemUrl);
            await _marketDb.RemoveBuyOrderAsync(order);
            Log.Logger.Information("Item {0} with buy price {1} was canceled", order.EngItemName, order.Price);
        }
        Log.Logger.Information("All buy orders have been canceled!");
    }
}