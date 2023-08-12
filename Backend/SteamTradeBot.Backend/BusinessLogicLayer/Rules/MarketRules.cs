using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SteamTradeBot.Backend.BusinessLogicLayer.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules;

public class MarketRules
{
    private readonly IEnumerable<IBuyRule> _buyRules;
    private readonly IEnumerable<ISellRule> _sellRules;
    private readonly IEnumerable<ICancelRule> _cancelRules;

    public MarketRules(IEnumerable<IBuyRule> buyRules, IEnumerable<ISellRule> sellRules, IEnumerable<ICancelRule> cancelRules)
    {
        _buyRules = buyRules;
        _sellRules = sellRules;
        _cancelRules = cancelRules;
    }

    public async Task<bool> CanBuyItemAsync(ItemPage itemPage)
    {
        var tasks = _buyRules.Select(rule => rule.IsFollowedAsync(itemPage));
        var results = await Task.WhenAll(tasks);
        return results.All(x => x);
    }

    public async Task<bool> CanSellItemAsync(ItemPage itemPage)
    {
        var tasks = _sellRules.Select(rule => rule.IsFollowedAsync(itemPage));
        var results = await Task.WhenAll(tasks);
        return results.All(x => x);
    }

    public async Task<bool> IsNeedCancelItemAsync(ItemPage itemPage)
    {
        var tasks = _cancelRules.Select(rule => rule.IsFollowedAsync(itemPage));
        var results = await Task.WhenAll(tasks);
        return results.All(x => x);
    }
}