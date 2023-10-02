using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.ItemModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;

public sealed class TrendRule : IBuyRule
{
    private readonly IConfigurationService _configurationService;

    public TrendRule(IConfigurationService configurationService)
    {
        _configurationService = configurationService;
    }

    public bool IsFollowed(ItemPage itemPage)
    {
        
        throw new NotImplementedException();
    }

    public async Task<bool> IsFollowedAsync(ItemPage itemPage)
    {
        return await Task.Run(() =>
        {
            var trend = PriceTrend(itemPage.SalesChart);
            if (trend > _configurationService.Trend)
            {
                Log.Information("Trend is ok.");
                return true;
            }
            Log.Information("Trend is bad. Lower than needed. Current trend: {0} < Required trend: {1}",
                trend.ToString("F10"), _configurationService.Trend);
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