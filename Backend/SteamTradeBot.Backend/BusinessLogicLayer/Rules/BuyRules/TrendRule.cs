﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;

public class TrendRule : IBuyRule
{
    private readonly IConfigurationManager _configurationManager;

    public TrendRule(IConfigurationManager configurationManager)
    {
        _configurationManager = configurationManager;
    }

    public bool IsFollowed(ItemPage itemPage)
    {
        
        throw new NotImplementedException();
    }

    public async Task<bool> IsFollowedAsync(ItemPage itemPage)
    {
        return await Task.Run(() =>
        {
            Log.Information("Checking trend...");
            var trend = PriceTrend(itemPage.SalesChart);
            if (trend > _configurationManager.Trend)
                return true;
            Log.Information("Item is not profitable. Reason: trend is lower than needed. Current trend: {0} < Required trend: {1}",
                trend.ToString("F10"), _configurationManager.Trend);
            return false;
        });
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