using Serilog;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.ProfitRules;

public class AvailableBalanceRule : IBuyRule
{
    private readonly Settings _settings;

    public AvailableBalanceRule(Settings settings)
    {
        _settings = settings;
    }
    public bool IsFollowed(ItemPage itemPage)
    {
        Log.Information("Checking available balance...");
        var availableBalance = itemPage.Balance * _settings.AvailableBalance;
        if (availableBalance > itemPage.BuyPrice) 
            return true;
        Log.Information("Item is not profitable. Reason: no money for this item. Available balance: {0} < Price: {1}", availableBalance, itemPage.BuyPrice);
        return false;
    }
}