using System.Collections.Generic;
using System.Threading.Tasks;
using SteamTradeBot.Backend.BusinessLogicLayer.Abstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules;
using SteamTradeBot.Backend.BusinessLogicLayer.Solutions;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.ItemModel;
using SteamTradeBot.Backend.Services;
using ConfigurationManager = SteamTradeBot.Backend.Services.ConfigurationManager;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Factories;

public class SolutionsFactory
{
    private readonly MarketRules _rules;
    private readonly StateManagerService _stateManager;
    private static Dictionary<string, MarketSolution> _solutions;

    public SolutionsFactory(SteamAPI api, MarketDbAccess marketDb, ConfigurationManager configurationManager, MarketRules rules, StateManagerService stateManager)
    {
        _rules = rules;
        _stateManager = stateManager;
        _solutions = new Dictionary<string, MarketSolution>
        {
            { nameof(BuyMarketSolution), new BuyMarketSolution(api, marketDb, configurationManager, stateManager) },
            { nameof(SellMarketSolution), new SellMarketSolution(api, marketDb, configurationManager, stateManager) },
            { nameof(CancelMarketSolution), new CancelMarketSolution(api, marketDb, configurationManager, stateManager) },
        };
    }

    public async Task<MarketSolution?> GetSolutionAsync(ItemPage itemPage)
    {
        _stateManager.OnItemAnalyzed(itemPage);

        if (await _rules.CanSellItemAsync(itemPage))
            return _solutions[nameof(SellMarketSolution)];

        if (await _rules.IsNeedCancelItemAsync(itemPage))
            return _solutions[nameof(CancelMarketSolution)];

        if (await _rules.CanBuyItemAsync(itemPage))
            return _solutions[nameof(BuyMarketSolution)];

        return null;
    }
}