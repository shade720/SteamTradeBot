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
        var buyOrders = await _marketDb.GetBuyOrdersAsync(apiKey);
        foreach (var order in buyOrders)
        {
            Log.Logger.Information("Cancelling item {0} (Price: {1}, Quantity: {2})", 
                order.EngItemName, order.BuyPrice, order.Quantity);
            var successfullyCanceled = await _api.CancelBuyOrderAsync(order.ItemUrl);
            if (successfullyCanceled)
            {
                await _marketDb.RemoveBuyOrderAsync(order);
                Log.Logger.Information("Item {0} (Price: {1}, Quantity: {2}) was canceled", 
                    order.EngItemName, order.BuyPrice, order.Quantity);
            }
            else
            {
                Log.Logger.Warning("Can't cancel item {0} (Price: {1}, Quantity: {2})", 
                    order.EngItemName, order.BuyPrice, order.Quantity);
            }
        }
        Log.Logger.Information("All buy orders have been canceled!");
    }
}