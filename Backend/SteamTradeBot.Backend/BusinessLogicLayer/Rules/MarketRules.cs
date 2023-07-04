using System.Collections.Generic;
using System.Linq;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules.CancelRules;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules.ProfitRules;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules.SellRules;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules;

public class MarketRules
{
    private readonly List<IBuyRule> _buyRules;
    private readonly List<ISellRule> _sellRules;
    private readonly List<ICancelRule> _cancelRules;

    public MarketRules(List<IBuyRule> buyRules, List<ISellRule> sellRules, List<ICancelRule> cancelRules)
    {
        _buyRules = buyRules;
        _sellRules = sellRules;
        _cancelRules = cancelRules;
    }

    public bool CanBuyItem(ItemPage itemPage)
    {
        return _buyRules.All(rule => rule.IsFollowed(itemPage));
    }

    public bool CanSellItem(ItemPage itemPage)
    {
        return _sellRules.All(rule => rule.IsFollowed(itemPage));
    }

    public bool IsNeedCancelItem(ItemPage itemPage)
    {
        return _cancelRules.All(rule => rule.IsFollowed(itemPage));
    }
}