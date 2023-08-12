using System;
using System.Linq;
using Serilog;
using SteamTradeBot.Backend.Models;
using SteamTradeBot.Backend.Services;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;

public class TrendRule : IBuyRule
{
    private readonly ConfigurationManager _configurationManager;

    public TrendRule(ConfigurationManager configurationManager)
    {
        _configurationManager = configurationManager;
    }

    public bool IsFollowed(ItemPage itemPage)
    {
        Log.Information("Checking trend...");
        var currentConfiguration = _configurationManager.GetConfiguration();
        var trend = PriceTrend(itemPage.SalesChart);
        if (trend > currentConfiguration.Trend)
            return true;
        Log.Information("Item is not profitable. Reason: trend is lower than needed. Current trend: {0} < Required trend: {1}",
            trend.ToString("F10"), currentConfiguration.Trend);
        return false;
    }

    private static double PriceTrend(Chart salesChart)
    {
        //m = ∑ (x - AVG(x)(y - AVG(y)) / ∑ (x - AVG(x))²
        var localGraph = new Chart(salesChart);

        var avgX = localGraph.Average(x => x.Date.ToOADate());
        var avgY = localGraph.Average(x => x.Price * x.Quantity);

        var item1 = localGraph.Sum(x => (x.Date.ToOADate() - avgX) * ((x.Price * x.Quantity) - avgY));
        var item2 = localGraph.Sum(x => Math.Pow(x.Date.ToOADate() - avgX, 2));

        return item1 / item2;
    }
}