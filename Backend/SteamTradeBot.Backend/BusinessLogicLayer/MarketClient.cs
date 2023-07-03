using Microsoft.Extensions.Configuration;
using Serilog;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer;

public class MarketClient
{
    private readonly SteamAPI _api;
    private readonly IConfiguration _configuration;

    public MarketClient(SteamAPI api, IConfiguration configuration)
    {
        _api = api;
        _configuration = configuration;
    }

    public void Buy(BuyOrder order)
    {
        _api.PlaceBuyOrder(order.ItemUrl, order.Price, order.Quantity);
        Log.Information($"Buy {order.Quantity} {order.RusItemName} at price {order.Price}");
    }

    public void Sell(SellOrder order)
    {
        for (var i = 0; i < order.Quantity; i++)
            _api.PlaceSellOrder(order.EngItemName, order.Price, _configuration["SteamUserId"]!);
        Log.Information("Item {0} has sold", order.RusItemName);
    }

    public void CancelBuyOrder(BuyOrder itemPage)
    {
        _api.CancelBuyOrder(itemPage.ItemUrl);
        Log.Information("Cancel buy order of {0} {1} at price {2}", itemPage.Quantity, itemPage.EngItemName, itemPage.Price);
    }
}