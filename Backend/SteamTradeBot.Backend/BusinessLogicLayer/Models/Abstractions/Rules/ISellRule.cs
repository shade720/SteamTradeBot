using System.Threading.Tasks;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.ItemModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.Rules;

public interface ISellRule
{
    public bool IsFollowed(ItemPage itemPage);
    public Task<bool> IsFollowedAsync(ItemPage itemPage);
}