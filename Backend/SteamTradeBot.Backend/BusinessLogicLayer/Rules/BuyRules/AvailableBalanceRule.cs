using Serilog;
using SteamTradeBot.Backend.Models;
using SteamTradeBot.Backend.Services;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;

public class AvailableBalanceRule : IBuyRule
{
    private readonly SteamAPI _api;
    private readonly ConfigurationManager _configurationManager;

    public AvailableBalanceRule(SteamAPI api, ConfigurationManager configurationManager)
    {
        _api = api;
        _configurationManager = configurationManager;
    }
    public bool IsFollowed(ItemPage itemPage)
    {
        Log.Information("Checking available balance...");
        var currentConfiguration = _configurationManager.GetConfiguration();
        var availableBalance = _api.GetBalance() * currentConfiguration.AvailableBalance;
        if (availableBalance > itemPage.EstimatedBuyPrice) 
            return true;
        Log.Information("Item is not profitable. Reason: no money for this item. Available balance: {0} < Price: {1}", availableBalance, itemPage.EstimatedBuyPrice);
        return false;
    }
}