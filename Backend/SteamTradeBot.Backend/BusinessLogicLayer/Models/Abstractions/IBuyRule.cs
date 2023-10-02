using System.Threading.Tasks;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.ItemModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;

public interface IBuyRule
{
    public bool IsFollowed(ItemPage itemPage);
    public Task<bool> IsFollowedAsync(ItemPage itemPage);
}