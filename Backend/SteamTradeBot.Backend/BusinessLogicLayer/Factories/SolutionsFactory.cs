using System.Collections.Generic;
using System.Threading.Tasks;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules;
using SteamTradeBot.Backend.BusinessLogicLayer.Solutions;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Factories;

public sealed class SolutionsFactory
{
    private readonly IStateManager _stateManager;
    private readonly MarketRules _rules;
    private readonly Dictionary<string, MarketSolution> _solutions;

    public SolutionsFactory(
        ISteamApi api, 
        IConfigurationManager configurationManager, 
        IStateManager stateManager, 
        OrdersDbAccess ordersDb, 
        MarketRules rules)
    {
        _rules = rules;
        _stateManager = stateManager;
        _solutions = new Dictionary<string, MarketSolution>
        {
            { nameof(BuyMarketSolution), new BuyMarketSolution(api, configurationManager, stateManager, ordersDb) },
            { nameof(SellMarketSolution), new SellMarketSolution(api, configurationManager, stateManager, ordersDb) },
            { nameof(CancelMarketSolution), new CancelMarketSolution(api, configurationManager, stateManager, ordersDb) }
        };
    }

    public async Task<MarketSolution?> GetSolutionAsync(ItemPage itemPage)
    {
        await _stateManager.OnItemAnalyzingAsync(itemPage);

        if (await _rules.CanSellItemAsync(itemPage))
            return _solutions[nameof(SellMarketSolution)];

        if (await _rules.IsNeedCancelItemAsync(itemPage))
            return _solutions[nameof(CancelMarketSolution)];

        if (await _rules.CanBuyItemAsync(itemPage))
            return _solutions[nameof(BuyMarketSolution)];

        return null;
    }
}