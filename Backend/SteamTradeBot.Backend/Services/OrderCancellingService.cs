using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.Services;

public class OrderCancellingService
{
    private readonly ISteamApi _api;
    private readonly OrdersDbAccess _ordersDb;

    public OrderCancellingService(ISteamApi api, OrdersDbAccess ordersDb)
    {
        _api = api;
        _ordersDb = ordersDb;
    }

    public async Task ClearBuyOrdersAsync(string apiKey)
    {
        Log.Logger.Information("Start buy orders canceling...");
        var buyOrders = await _ordersDb.GetOrdersAsync(apiKey, OrderType.BuyOrder);
        foreach (var order in buyOrders)
        {
            Log.Logger.Information("Cancelling item {0} (Price: {1}, Quantity: {2})", 
                order.EngItemName, order.BuyPrice, order.Quantity);
            var successfullyCanceled = await _api.CancelBuyOrderAsync(order.ItemUrl);
            if (successfullyCanceled)
            {
                await _ordersDb.RemoveOrderAsync(order);
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