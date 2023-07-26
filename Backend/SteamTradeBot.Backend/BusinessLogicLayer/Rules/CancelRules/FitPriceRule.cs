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
    private readonly IConfiguration _configuration;

    public FitPriceRule(IConfiguration configuration, MarketDbAccess marketDb)
    {
        _configuration = configuration;
        _marketDb = marketDb;
    }
    public bool IsFollowed(ItemPage itemPage)
    {
        if (itemPage.MyBuyOrder is null) 
            return false;
        Log.Information("Checking if order obsolete...");
        var fitPriceRange = double.Parse(_configuration["FitPriceRange"]!, NumberStyles.Any, CultureInfo.InvariantCulture);
        var listingPosition = int.Parse(_configuration["ListingPosition"]!);
        var existingBuyOrder = _marketDb.GetBuyOrders().FirstOrDefault(order => order.EngItemName == itemPage.EngItemName);
        return existingBuyOrder is not null && itemPage.BuyOrderBook.Take(listingPosition).Any(x => Math.Abs(x.Price - existingBuyOrder.Price) > fitPriceRange);
    }
}