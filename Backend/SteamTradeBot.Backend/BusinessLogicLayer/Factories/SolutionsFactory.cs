using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.ItemModel;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules;
using SteamTradeBot.Backend.BusinessLogicLayer.Solutions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Factories;

public sealed class SolutionsFactory
{
    private readonly IEventService _eventService;
    private readonly MarketRules _rules;
    private readonly Dictionary<string, MarketSolution> _solutions;

    public SolutionsFactory(
        ISteamApi api, 
        IConfigurationService configurationService, 
        IEventService eventService, 
        OrdersRepository ordersRepository, 
        MarketRules rules)
    {
        _rules = rules;
        _eventService = eventService;
        _solutions = new Dictionary<string, MarketSolution>
        {
            { nameof(BuyMarketSolution), new BuyMarketSolution(api, configurationService, eventService, ordersRepository) },
            { nameof(SellMarketSolution), new SellMarketSolution(api, configurationService, eventService, ordersRepository) },
            { nameof(CancelMarketSolution), new CancelMarketSolution(api, configurationService, eventService, ordersRepository) }
        };
    }

    public async Task<MarketSolution?> GetSolutionAsync(ItemPage itemPage)
    {
        await _eventService.OnItemAnalyzingAsync(itemPage);

        if (await _rules.CanSellItemAsync(itemPage))
            return _solutions[nameof(SellMarketSolution)];

        if (await _rules.IsNeedCancelItemAsync(itemPage))
            return _solutions[nameof(CancelMarketSolution)];

        if (await _rules.CanBuyItemAsync(itemPage))
            return _solutions[nameof(BuyMarketSolution)];

        return null;
    }
}