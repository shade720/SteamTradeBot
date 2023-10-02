using System.Threading.Tasks;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.ItemModel;
using SteamTradeBot.Backend.DataAccessLayer;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;

public abstract class MarketSolution
{
    protected readonly ISteamApi SteamApi;
    protected readonly IConfigurationService ConfigurationService;
    protected readonly IEventService EventService;
    protected readonly OrdersRepository OrdersRepository;

    protected MarketSolution(
        ISteamApi api,
        IConfigurationService configurationService,
        IEventService eventService,
        OrdersRepository ordersRepository)
    {
        SteamApi = api;
        ConfigurationService = configurationService;
        EventService = eventService;
        OrdersRepository = ordersRepository;
    }

    public abstract void Perform(ItemPage itemPage);

    public abstract Task PerformAsync(ItemPage itemPage);
}