using System.Linq;
using Serilog;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.ProfitRules;

public class RequiredProfitRule : IBuyRule
{
    private readonly Settings _settings;

    public RequiredProfitRule(Settings settings)
    {
        _settings = settings;
    }

    public bool IsFollowed(ItemPage itemPage)
    {
        Log.Information("Finding profitable order...");
        if (itemPage.BuyOrderBook.Any(buyOrder => itemPage.SellOrderBook.Any(sellOrder => sellOrder.Price + _settings.RequiredProfit > buyOrder.Price * (1 + _settings.SteamCommission)))) 
            return true;
        Log.Information("Item is not profitable. Reason: profitable sell order is not found. Required price: {0}, Available price: {1}", 
            itemPage.SellOrderBook.Max(sellOrder => sellOrder.Price) + _settings.RequiredProfit, itemPage.BuyOrderBook.Min(order => order.Price) + _settings.SteamCommission);
        return false;
    }
}