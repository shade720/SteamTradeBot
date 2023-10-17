using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.ItemModel;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;

public abstract class MarketSolution
{
    protected readonly ISteamApi SteamApi;
    protected readonly IConfigurationService ConfigurationService;
    protected readonly ITradingEventHandler TradingEventHandler;
    protected readonly OrdersRepository OrdersRepository;

    protected MarketSolution(
        ISteamApi api,
        IConfigurationService configurationService,
        ITradingEventHandler tradingEventHandler,
        OrdersRepository ordersRepository)
    {
        SteamApi = api;
        ConfigurationService = configurationService;
        TradingEventHandler = tradingEventHandler;
        OrdersRepository = ordersRepository;
    }

    public abstract void Perform(ItemPage itemPage);

    public abstract Task PerformAsync(ItemPage itemPage);
}