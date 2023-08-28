using System.Threading.Tasks;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.Models.Abstractions;

public abstract class MarketSolution
{
    protected readonly ISteamApi SteamApi;
    protected readonly IConfigurationManager ConfigurationManager;
    protected readonly IStateManager StateManager;
    protected readonly OrdersDbAccess OrdersDb;

    protected MarketSolution(
        ISteamApi api, 
        IConfigurationManager configurationManager, 
        IStateManager stateManager, 
        OrdersDbAccess ordersDb)
    {
        SteamApi = api;
        ConfigurationManager = configurationManager;
        StateManager = stateManager;
        OrdersDb = ordersDb;
    }

    public abstract void Perform(ItemPage itemPage);

    public abstract Task PerformAsync(ItemPage itemPage);
}