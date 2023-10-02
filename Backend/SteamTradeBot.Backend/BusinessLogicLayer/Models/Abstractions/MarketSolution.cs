using System.Threading.Tasks;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.ItemModel;
using SteamTradeBot.Backend.DataAccessLayer;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;

public abstract class MarketSolution
{
    protected readonly ISteamApi SteamApi;
    protected readonly IConfigurationService ConfigurationService;
    protected readonly IStateService StateService;
    protected readonly OrdersRepository OrdersRepository;

    protected MarketSolution(
        ISteamApi api,
        IConfigurationService configurationService,
        IStateService stateService,
        OrdersRepository ordersRepository)
    {
        SteamApi = api;
        ConfigurationService = configurationService;
        StateService = stateService;
        OrdersRepository = ordersRepository;
    }

    public abstract void Perform(ItemPage itemPage);

    public abstract Task PerformAsync(ItemPage itemPage);
}