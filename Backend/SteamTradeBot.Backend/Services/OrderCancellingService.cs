using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.Abstractions;

namespace SteamTradeBot.Backend.Services;

public class OrderCancellingService
{
    private readonly ISteamApi _api;
    private readonly MarketDbAccess _marketDb;

    public OrderCancellingService(ISteamApi api, MarketDbAccess marketDb)
    {
        _api = api;
        _marketDb = marketDb;
    }

    public async Task ClearBuyOrdersAsync(string apiKey)
    {
        Log.Logger.Information("Start buy orders canceling...");
        var itemsWithPurchaseOrders = await _marketDb.GetBuyOrdersAsync(apiKey);
        foreach (var order in itemsWithPurchaseOrders)
        {
            Log.Logger.Information("Cancelling item {0} with buy price {1}", order.EngItemName, order.BuyPrice);
            await _api.CancelBuyOrderAsync(order.ItemUrl);
            await _marketDb.RemoveBuyOrderAsync(order);
            Log.Logger.Information("Item {0} with buy price {1} was canceled", order.EngItemName, order.BuyPrice);
        }
        Log.Logger.Information("All buy orders have been canceled!");
    }
}