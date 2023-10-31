using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.ItemPageAggregate;

namespace SteamTradeBot.Backend.Domain.Abstractions;

public abstract class MarketSolution
{
    protected readonly ISteamApi SteamApi;
    protected readonly IConfigurationService ConfigurationService;
    protected readonly ITradingEventHandler TradingEventHandler;
    protected readonly IOrdersRepository OrdersRepository;

    protected MarketSolution(
        ISteamApi api,
        IConfigurationService configurationService,
        ITradingEventHandler tradingEventHandler,
        IOrdersRepository ordersRepository)
    {
        SteamApi = api;
        ConfigurationService = configurationService;
        TradingEventHandler = tradingEventHandler;
        OrdersRepository = ordersRepository;
    }

    public abstract void Perform(ItemPage itemPage);

    public abstract Task PerformAsync(ItemPage itemPage);
}