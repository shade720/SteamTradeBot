using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.ItemModel;
using System.Threading.Tasks;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.Rules;

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
            Log.Information("Item is bad. Order already exist.");
            return false;
        });
    }
}