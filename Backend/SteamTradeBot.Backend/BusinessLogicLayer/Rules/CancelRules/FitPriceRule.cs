﻿using Serilog;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.CancelRules;

public sealed class FitPriceRule : ICancelRule
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
        {
            Log.Information("Can't check if order obsolete. Buy order does not exist.");
            return false;
        }
        if (itemPage.BuyOrderBook.Count <= 0)
        {
            Log.Information("Can't check if order obsolete. Buy order book is empty.");
            return false;
        }
        var existedBuyOrder = await _marketDb.GetBuyOrderAsync(itemPage.EngItemName, _configurationManager.ApiKey);

        var isOrderObsolete = existedBuyOrder is not null && itemPage.BuyOrderBook.Any(x =>
            Math.Abs(x.Price - existedBuyOrder.BuyPrice) > _configurationManager.FitPriceRange);
        if (!isOrderObsolete)
        {
            Log.Information("Order is still relevant.");
            return false;
            
        }
        Log.Information("Order is obsolete. Cancelling...");
        return true;
    }
}