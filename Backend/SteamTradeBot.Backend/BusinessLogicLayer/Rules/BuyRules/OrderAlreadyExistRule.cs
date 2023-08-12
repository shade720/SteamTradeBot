using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;

public class OrderAlreadyExistRule : IBuyRule
{
    public bool IsFollowed(ItemPage itemPage)
    {
        Log.Information("Checking if order already exist...");
        return itemPage.MyBuyOrder is null;
    }
}