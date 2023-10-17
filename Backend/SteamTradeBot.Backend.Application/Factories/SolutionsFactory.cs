using SteamTradeBot.Backend.Application.Rules;
using SteamTradeBot.Backend.Application.Solutions;
using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.ItemModel;

namespace SteamTradeBot.Backend.Application.Factories;

public sealed class SolutionsFactory
{
    private readonly MarketRules _rules;
    private readonly Dictionary<string, MarketSolution> _solutions;

    internal SolutionsFactory(
        ISteamApi api, 
        IConfigurationService configurationService, 
        ITradingEventHandler tradingEventHandler, 
        OrdersRepository ordersRepository, 
        MarketRules rules)
    {
        _rules = rules;
        _solutions = new Dictionary<string, MarketSolution>
        {
            { nameof(BuyMarketSolution), new BuyMarketSolution(api, configurationService, tradingEventHandler, ordersRepository) },
            { nameof(SellMarketSolution), new SellMarketSolution(api, configurationService, tradingEventHandler, ordersRepository) },
            { nameof(CancelMarketSolution), new CancelMarketSolution(api, configurationService, tradingEventHandler, ordersRepository) }
        };
    }

    internal async Task<MarketSolution?> GetSolutionAsync(ItemPage itemPage)
    {
        if (await _rules.CanSellItemAsync(itemPage))
            return _solutions[nameof(SellMarketSolution)];

        if (await _rules.IsNeedCancelItemAsync(itemPage))
            return _solutions[nameof(CancelMarketSolution)];

        if (await _rules.CanBuyItemAsync(itemPage))
            return _solutions[nameof(BuyMarketSolution)];

        return null;
    }
}