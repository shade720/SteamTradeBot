using Serilog;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;

public class OrderAlreadyExistRule : IBuyRule
{
    public bool IsFollowed(ItemPage itemPage)
    {
        Log.Information("Checking if order already exist...");
        return itemPage.MyBuyOrder is null;
    }
}