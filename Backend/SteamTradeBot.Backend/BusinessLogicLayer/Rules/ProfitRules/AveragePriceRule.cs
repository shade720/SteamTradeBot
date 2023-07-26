﻿using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Serilog;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.ProfitRules;

public class AveragePriceRule : IBuyRule
{
    private readonly IConfiguration _configuration;

    public AveragePriceRule(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public bool IsFollowed(ItemPage itemPage)
    {
        Log.Information("Checking average price...");

        var averagePriceRange = double.Parse(_configuration["AveragePrice"]!, NumberStyles.Any, CultureInfo.InvariantCulture);
        if (!itemPage.SellOrderBook.All(sellOrder => sellOrder.Price > itemPage.AvgPrice + averagePriceRange)) return true;
        Log.Information("Item is not profitable. Reason: average price is higher than needed. {0}", itemPage.AvgPrice + averagePriceRange);
        return false;
    }
}