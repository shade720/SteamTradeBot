using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;

public class AvailableBalanceRule : IBuyRule
{
    private readonly ISteamApi _api;
    private readonly IConfigurationManager _configurationManager;

    public AvailableBalanceRule(ISteamApi api, IConfigurationManager configurationManager)
    {
        _api = api;
        _configurationManager = configurationManager;
    }
    public bool IsFollowed(ItemPage itemPage)
    {
        throw new System.NotImplementedException();
    }

    public async Task<bool> IsFollowedAsync(ItemPage itemPage)
    {
        Log.Information("Checking available balance...");
        var availableBalance = await _api.GetBalanceAsync() * _configurationManager.AvailableBalance;
        if (availableBalance > itemPage.EstimatedBuyPrice)
            return true;
        Log.Information("Item is not profitable. Reason: no money for this item. Available balance: {0} < Price: {1}", availableBalance, itemPage.EstimatedBuyPrice);
        return false;
    }
}