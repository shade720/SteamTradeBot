using Serilog;
using SteamTradeBot.Backend.Domain.Abstractions.Rules;
using SteamTradeBot.Backend.Domain.ItemModel;

namespace SteamTradeBot.Backend.Application.Rules.BuyRules;

internal sealed class OrderAlreadyExistRule : IBuyRule
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
            Log.Information("Item is bad. Order already exist.");
            return false;
        });
    }
}