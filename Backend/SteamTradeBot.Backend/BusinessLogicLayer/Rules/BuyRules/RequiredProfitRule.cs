using System.Linq;
using Serilog;
using SteamTradeBot.Backend.Models;
using SteamTradeBot.Backend.Services;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;

public class RequiredProfitRule : IBuyRule
{
    private readonly ConfigurationManager _configurationManager;

    public RequiredProfitRule(ConfigurationManager configurationManager)
    {
        _configurationManager = configurationManager;
    }

    public bool IsFollowed(ItemPage itemPage)
    {
        Log.Information("Finding profitable order...");
        var currentConfiguration = _configurationManager.GetConfiguration();

        var buyPrice = itemPage.BuyOrderBook.FirstOrDefault(buyOrder => itemPage.SellOrderBook.Any(sellOrder =>
            sellOrder.Price + currentConfiguration.RequiredProfit > buyOrder.Price * (1 + currentConfiguration.SteamCommission)));
        if (buyPrice is null)
        {
            Log.Information("Item is not profitable. Reason: profitable buy price is not found. Required price: {0}, Available price: {1}",
                itemPage.SellOrderBook.Max(sellOrder => sellOrder.Price) + currentConfiguration.RequiredProfit, itemPage.BuyOrderBook.Min(order => order.Price) + currentConfiguration.SteamCommission);
            return false;
        }

        var sellPrice = itemPage.SellOrderBook.FirstOrDefault(sellOrder =>
            sellOrder.Price + currentConfiguration.RequiredProfit > buyPrice.Price * (1 + currentConfiguration.SteamCommission));
        if (sellPrice is null)
            return false;

        itemPage.EstimatedBuyPrice = buyPrice.Price;
        itemPage.EstimatedSellPrice = sellPrice.Price;
        
        return false;
    }
}