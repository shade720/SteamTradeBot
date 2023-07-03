using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.SellRules;

public interface ISellRule
{
    public bool IsFollowed(ItemPage itemPage);
}