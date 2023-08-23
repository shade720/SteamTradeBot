using System;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;

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
        if (itemPage.MyBuyOrder is null) 
            return false;
        Log.Information("Checking if order obsolete...");
        var existingBuyOrder = await _marketDb.GetBuyOrderAsync(itemPage.EngItemName, _configurationManager.ApiKey);
        return existingBuyOrder is not null && itemPage.BuyOrderBook.Any(x => Math.Abs(x.Price - existingBuyOrder.Price) > _configurationManager.FitPriceRange);
    }
}