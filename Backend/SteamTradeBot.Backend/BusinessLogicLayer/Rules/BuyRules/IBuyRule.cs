using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;

public interface IBuyRule
{
    public bool IsFollowed(ItemPage itemPage);
}