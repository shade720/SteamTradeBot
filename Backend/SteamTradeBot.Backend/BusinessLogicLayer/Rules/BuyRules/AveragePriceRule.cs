using System.Linq;
using Serilog;
using SteamTradeBot.Backend.Models;
using SteamTradeBot.Backend.Services;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;

public class AveragePriceRule : IBuyRule
{
    private readonly ConfigurationManager _configurationManager;

    public AveragePriceRule(ConfigurationManager configurationManager)
    {
        _configurationManager = configurationManager;
    }
    public bool IsFollowed(ItemPage itemPage)
    {
        Log.Information("Checking average price...");

        var avgPriceFromChart = AveragePrice(itemPage.SalesChart);
        var currentConfiguration = _configurationManager.GetConfiguration();

        if (itemPage.SellOrderBook.Any(sellOrder => sellOrder.Price > avgPriceFromChart + currentConfiguration.AveragePrice)) 
            return true;
        Log.Information("Item is not profitable. Reason: average price is higher than needed. Max sell price: {0} < Required average price: {1}", 
            itemPage.SellOrderBook.Max(sellOrder => sellOrder.Price), avgPriceFromChart + currentConfiguration.AveragePrice);
        return false;
    }

    private static double AveragePrice(Chart salesChart)
    {
        return salesChart.Sum(x => x.Price * x.Quantity) / salesChart.Sum(x => x.Quantity);
    }
}