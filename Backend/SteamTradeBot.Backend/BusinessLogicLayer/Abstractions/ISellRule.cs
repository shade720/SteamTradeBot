using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Abstractions;

public interface ISellRule
{
    public bool IsFollowed(ItemPage itemPage);
}