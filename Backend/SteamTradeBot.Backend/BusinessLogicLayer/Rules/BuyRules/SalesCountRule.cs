using System.Linq;
using Serilog;
using SteamTradeBot.Backend.Models;
using SteamTradeBot.Backend.Services;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;

public class SalesCountRule : IBuyRule
{
    private readonly ConfigurationManager _configurationManager;

    public SalesCountRule(ConfigurationManager configurationManager)
    {
        _configurationManager = configurationManager;
    }
    public bool IsFollowed(ItemPage itemPage)
    {
        Log.Information("Checking sales per week...");
        var currentConfiguration = _configurationManager.GetConfiguration();

        var salesByDay = SalesByDay(itemPage.SalesChart);
        if (salesByDay > currentConfiguration.SalesPerWeek) 
            return true;
        Log.Information("Item is not profitable. Reason: sales volume is lower than needed. Current sales: {0} < Required sales: {1}", salesByDay, currentConfiguration.SalesPerWeek);
        return false;
    }

    private static double SalesByDay(Chart salesChart)
    {
        return salesChart
            .GroupBy(x => x.Date.Date)
            .Select(x => x.Sum(y => y.Quantity))
            .Average();
    }
}