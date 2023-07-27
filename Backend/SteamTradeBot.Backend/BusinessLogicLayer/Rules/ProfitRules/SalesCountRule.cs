using Serilog;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.ProfitRules;

public class SalesCountRule : IBuyRule
{
    private readonly Settings _settings;

    public SalesCountRule(Settings settings)
    {
        _settings = settings;
    }
    public bool IsFollowed(ItemPage itemPage)
    {
        Log.Information("Checking sales per week...");
        if (itemPage.Sales > _settings.SalesPerWeek) 
            return true;
        Log.Information("Item is not profitable. Reason: sales volume is lower than needed. Current sales: {0} < Required sales: {1}", itemPage.Sales, _settings.SalesPerWeek);
        return false;
    }
}