using Microsoft.Extensions.Configuration;
using Serilog;
using SteamTradeBot.Backend.Models;
using System.Globalization;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.ProfitRules;

public class AvailableBalanceRule : IBuyRule
{
    private readonly IConfiguration _configuration;

    public AvailableBalanceRule(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public bool IsFollowed(ItemPage itemPage)
    {
        Log.Information("Checking available balance...");
        var availableBalancePercent = double.Parse(_configuration["AvailableBalance"]!, NumberStyles.Any, CultureInfo.InvariantCulture);
        var availableBalance = itemPage.Balance * availableBalancePercent;
        if (!(availableBalance < itemPage.BuyPrice)) return true;
        Log.Information("Item is not profitable. Reason: no money for this item. Available balance: {0}, Price: {1}", availableBalance, itemPage.BuyPrice);
        return false;
    }
}