using Serilog;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.ProfitRules;

public class TrendRule : IBuyRule
{
    private readonly Settings _settings;

    public TrendRule(Settings settings)
    {
        _settings = settings;
    }

    public bool IsFollowed(ItemPage itemPage)
    {
        Log.Information("Checking trend...");
        if (itemPage.Trend > _settings.Trend)
            return true;
        Log.Information("Item is not profitable. Reason: trend is lower than needed. Current trend: {0} < Required trend: {1}", 
            itemPage.Trend.ToString("F10"), _settings.Trend);
        return false;
    }
}