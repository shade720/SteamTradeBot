﻿using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Serilog;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.ProfitRules;

public class RequiredProfitRule : IBuyRule
{
    private readonly IConfiguration _configuration;

    public RequiredProfitRule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public bool IsFollowed(ItemPage itemPage)
    {
        var steamCommission = double.Parse(_configuration["SteamCommission"]!, NumberStyles.Any,
            CultureInfo.InvariantCulture);
        var requiredProfit = double.Parse(_configuration["RequiredProfit"]!, NumberStyles.Any,
            CultureInfo.InvariantCulture);
        
        Log.Information("Finding profitable order...");
        if (itemPage.BuyOrderBook.Any(buyOrder => itemPage.SellOrderBook.Any(sellOrder => sellOrder.Price + requiredProfit > buyOrder.Price * (1 + steamCommission)))) 
            return true;
        Log.Information("Item is not profitable. Reason: profitable sell order is not found. Required price: {0}, Available price: {1}", 
            itemPage.SellOrderBook.Max(sellOrder => sellOrder.Price) + requiredProfit, itemPage.BuyOrderBook.Min(order => order.Price) + steamCommission);
        return false;
    }
}