using Serilog;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.SellRules;

public class CurrentQuantityCheckRule : ISellRule
{
    private readonly SteamAPI _api;

    public CurrentQuantityCheckRule(SteamAPI api)
    {
        _api = api;
    }
    public bool IsFollowed(ItemPage itemPage)
    {
        if (itemPage.BuyOrderQuantity <= 0 || !itemPage.IsThereSellOrder)
            return false;
        Log.Information("Checking if order satisfied...");
        return itemPage.BuyOrderQuantity <= _api.GetBuyOrderQuantity(itemPage.ItemUrl);
    }
}