using Microsoft.Extensions.Configuration;
using Serilog;
using SteamTradeBot.Backend.Models;
using System.Globalization;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.ProfitRules;

public class TrendRule : IBuyRule
{
    private readonly IConfiguration _configuration;

    public TrendRule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public bool IsFollowed(ItemPage itemPage)
    {
        Log.Information("Checking trend...");
        var trendForCompare = double.Parse(_configuration["Trend"]!, NumberStyles.Any, CultureInfo.InvariantCulture);
        if (itemPage.Trend > trendForCompare)
            return true;
        Log.Information("Item is not profitable. Reason: trend is lower than needed. Current trend: {0} < Required trend: {1}", 
            itemPage.Trend.ToString("F10"), trendForCompare);
        return false;
    }
}