using System.Linq;
using Serilog;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.ProfitRules;

public class AveragePriceRule : IBuyRule
{
    private readonly Settings _settings;

    public AveragePriceRule(Settings settings)
    {
        _settings = settings;
    }
    public bool IsFollowed(ItemPage itemPage)
    {
        Log.Information("Checking average price...");

        if (itemPage.SellOrderBook.Any(sellOrder => sellOrder.Price > itemPage.AvgPrice + _settings.AveragePrice)) 
            return true;
        Log.Information("Item is not profitable. Reason: average price is higher than needed. Max sell price: {0} < Required average price: {1}", 
            itemPage.SellOrderBook.Max(sellOrder => sellOrder.Price), itemPage.AvgPrice + _settings.AveragePrice);
        return false;
    }
}