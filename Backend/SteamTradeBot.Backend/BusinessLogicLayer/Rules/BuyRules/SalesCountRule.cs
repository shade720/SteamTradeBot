﻿using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.ItemModel;
using System.Linq;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;

public sealed class SalesCountRule : IBuyRule
{
    private readonly IConfigurationService _configurationService;

    public SalesCountRule(IConfigurationService configurationService)
    {
        _configurationService = configurationService;
    }
    public bool IsFollowed(ItemPage itemPage)
    {
        throw new System.NotImplementedException();
    }

    public async Task<bool> IsFollowedAsync(ItemPage itemPage)
    {
        return await Task.Run(() =>
        {
            var salesPerDayFromChart = SalesPerDayFromChart(itemPage.SalesChart);
            if (salesPerDayFromChart > _configurationService.SalesPerDay)
            {
                Log.Information("Sales count is ok.");
                return true;
            }
            Log.Information("Sales count is bad. Sales volume is lower than needed. Current sales: {0} < Required sales: {1}", 
                salesPerDayFromChart, _configurationService.SalesPerDay);
            return false;
        });
    }

    private static double SalesPerDayFromChart(Chart salesChart)
    {
        return salesChart
            .GroupBy(x => x.Date.Date)
            .Select(x => x.Sum(y => y.Quantity))
            .Average();
    }
}