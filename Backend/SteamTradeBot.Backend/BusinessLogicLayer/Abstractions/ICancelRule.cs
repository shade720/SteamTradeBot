using System.Globalization;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Abstractions;

public interface ICancelRule
{
    public bool IsFollowed(ItemPage itemPage);
}