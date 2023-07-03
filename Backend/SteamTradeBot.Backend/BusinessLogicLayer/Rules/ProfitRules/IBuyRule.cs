using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.ProfitRules;

public interface IBuyRule
{
    public bool IsFollowed(ItemPage itemPage);
}