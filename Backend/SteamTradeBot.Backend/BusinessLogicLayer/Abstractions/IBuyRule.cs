using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Abstractions;

public interface IBuyRule
{
    public bool IsFollowed(ItemPage itemPage);
}