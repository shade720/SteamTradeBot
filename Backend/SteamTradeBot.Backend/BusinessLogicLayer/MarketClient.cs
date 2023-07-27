using Serilog;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer;

public class MarketClient
{
    private readonly SteamAPI _api;
    private readonly Settings _settings;

    public MarketClient(SteamAPI api, Settings settings)
    {
        _api = api;
        _settings = settings;
    }

    public void Buy(BuyOrder order)
    {
        _api.PlaceBuyOrder(order.ItemUrl, order.Price, order.Quantity);
        Log.Information("Place buy order {0} (Price: {1}, Quantity: {2})", order.EngItemName, order.Price, order.Quantity);
    }

    public void Sell(SellOrder order)
    {
        for (var i = 0; i < order.Quantity; i++)
        {
            if (_api.PlaceSellOrder(order.EngItemName, order.Price, _settings.SteamUserId))
                Log.Information("Place sell order {0} (Price: {1})", order.EngItemName, order.Price);
            else
                Log.Error("Can't place sell order {0} (Price: {1}). Item not found in inventory!", order.EngItemName, order.Price);
        }
    }

    public void CancelBuyOrder(BuyOrder order)
    {
        _api.CancelBuyOrder(order.ItemUrl);
        Log.Information("Cancel buy order {0} (Price: {1}, Quantity: {2})", order.EngItemName, order.Price, order.Quantity);
    }
}