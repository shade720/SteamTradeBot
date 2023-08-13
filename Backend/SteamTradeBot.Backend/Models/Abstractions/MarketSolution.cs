using System.Threading.Tasks;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.Models.Abstractions;

public abstract class MarketSolution
{
    protected readonly ISteamApi SteamApi;
    protected readonly IConfigurationManager ConfigurationManager;
    protected readonly IStateManager StateManager;
    protected readonly MarketDbAccess MarketDb;

    protected MarketSolution(ISteamApi api, IConfigurationManager configurationManager, IStateManager stateManager, MarketDbAccess marketDb)
    {
        SteamApi = api;
        MarketDb = marketDb;
        ConfigurationManager = configurationManager;
        StateManager = stateManager;
    }

    public abstract void Perform(ItemPage itemPage);

    public abstract Task PerformAsync(ItemPage itemPage);
}