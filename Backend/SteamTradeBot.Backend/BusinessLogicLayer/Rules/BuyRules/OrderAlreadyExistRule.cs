using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;

public sealed class OrderAlreadyExistRule : IBuyRule
{
    public bool IsFollowed(ItemPage itemPage)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> IsFollowedAsync(ItemPage itemPage)
    {
        return Task.Run(() =>
        {
            var isOrderAlreadyExist = itemPage.MyBuyOrder is not null;
            if (!isOrderAlreadyExist) return true;
            Log.Information("Item is bad. Reason: Order already exist.");
            return false;
        });
    }
}