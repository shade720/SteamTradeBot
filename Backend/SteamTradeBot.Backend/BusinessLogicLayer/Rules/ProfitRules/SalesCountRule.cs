using Microsoft.Extensions.Configuration;
using Serilog;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.ProfitRules;

public class SalesCountRule : IBuyRule
{
    private readonly IConfiguration _configuration;

    public SalesCountRule(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public bool IsFollowed(ItemPage itemPage)
    {
        Log.Information("Checking sales per week...");
        var sales = int.Parse(_configuration["SalesPerWeek"]!);
        if (itemPage.Sales > sales) 
            return true;
        Log.Information("Item is not profitable. Reason: sales volume is lower than needed. Current sales: {0} < Required sales: {1}", itemPage.Sales, sales);
        return false;
    }
}