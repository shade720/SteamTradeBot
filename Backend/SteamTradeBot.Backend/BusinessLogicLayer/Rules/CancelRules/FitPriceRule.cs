using System;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Serilog;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.CancelRules;

public class FitPriceRule : ICancelRule
{
    private readonly MarketDbAccess _marketDb;
    private readonly Settings _settings;

    public FitPriceRule(Settings settings, MarketDbAccess marketDb)
    {
        _settings = settings;
        _marketDb = marketDb;
    }
    public bool IsFollowed(ItemPage itemPage)
    {
        if (itemPage.MyBuyOrder is null) 
            return false;
        Log.Information("Checking if order obsolete...");
        var existingBuyOrder = _marketDb.GetBuyOrders().FirstOrDefault(order => order.EngItemName == itemPage.EngItemName);
        return existingBuyOrder is not null && itemPage.BuyOrderBook.Any(x => Math.Abs(x.Price - existingBuyOrder.Price) > _settings.FitPriceRange);
    }
}