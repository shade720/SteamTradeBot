using Microsoft.Extensions.Configuration;
using Serilog;
using SteamTradeBot.Backend.Models;
using System.Globalization;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.ProfitRules;

public class AvailableBalanceRule : IBuyRule
{
    private readonly double _currentBalance;
    private readonly IConfiguration _configuration;

    public AvailableBalanceRule(double currentBalance, IConfiguration configuration)
    {
        _currentBalance = currentBalance;
        _configuration = configuration;
    }
    public bool IsFollowed(ItemPage itemPage)
    {
        Log.Information("Checking available balance...");
        var availableBalancePercent = double.Parse(_configuration["AvailableBalance"]!, NumberStyles.Any, CultureInfo.InvariantCulture);
        var availableBalance = _currentBalance * availableBalancePercent;
        if (!(availableBalance < itemPage.BuyPrice)) return true;
        Log.Information("Item is not profitable. Reason: no money for this item. Available balance: {0}, Price: {1}", availableBalance, itemPage.BuyPrice);
        return false;
    }
}