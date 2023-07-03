using System.Globalization;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.CancelRules;

public interface ICancelRule
{
    public bool IsFollowed(ItemPage itemPage);
}