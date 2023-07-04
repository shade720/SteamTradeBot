using System;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Serilog;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.CancelRules;

public class FitPriceRule : ICancelRule
{
    private readonly SteamAPI _api;
    private readonly IConfiguration _configuration;

    public FitPriceRule(SteamAPI api, IConfiguration configuration)
    {
        _api = api;
        _configuration = configuration;
    }
    public bool IsFollowed(ItemPage itemPage)
    {
        if (itemPage.BuyOrderQuantity <= 0) return false;
        Log.Information("Checking if order obsolete...");
        const int listingPageSize = 1;
        var sellOrderBook = _api.GetSellOrdersBook(itemPage.ItemUrl, listingPageSize);
        var fitPriceRange = double.Parse(_configuration["FitPriceRange"]!, NumberStyles.Any, CultureInfo.InvariantCulture);
        return sellOrderBook.Any(x => Math.Abs(x.Price - itemPage.BuyOrderPrice) < fitPriceRange);
    }
}