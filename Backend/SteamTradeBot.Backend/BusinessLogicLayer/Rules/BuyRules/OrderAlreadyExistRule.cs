using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;

public class OrderAlreadyExistRule : IBuyRule
{
    public bool IsFollowed(ItemPage itemPage)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> IsFollowedAsync(ItemPage itemPage)
    {
        return Task.Run(() =>
        {
            Log.Information("Checking if order already exist...");
            return itemPage.MyBuyOrder is null;
        });
    }
}