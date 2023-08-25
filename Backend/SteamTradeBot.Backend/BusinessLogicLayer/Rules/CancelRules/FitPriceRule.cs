using Serilog;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.CancelRules;

public class FitPriceRule : ICancelRule
{
    private readonly MarketDbAccess _marketDb;
    private readonly IConfigurationManager _configurationManager;

    public FitPriceRule(IConfigurationManager configurationManager, MarketDbAccess marketDb)
    {
        _configurationManager = configurationManager;
        _marketDb = marketDb;
    }

    public bool IsFollowed(ItemPage itemPage)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsFollowedAsync(ItemPage itemPage)
    {
        Log.Information("Checking if order obsolete...");
        if (itemPage.MyBuyOrder is null)
        {
            Log.Information("Can't check if order obsolete. Reason: buy order does not exist.");
            return false;
        }
        if (itemPage.BuyOrderBook.Count <= 0)
        {
            Log.Information("Can't check if order obsolete. Reason: buy order book is empty.");
            return false;
        }
        var existingBuyOrder = await _marketDb.GetBuyOrderAsync(itemPage.EngItemName, _configurationManager.ApiKey);
        return existingBuyOrder is not null && itemPage.BuyOrderBook.Any(x => Math.Abs(x.Price - existingBuyOrder.Price) > _configurationManager.FitPriceRange);
    }
}