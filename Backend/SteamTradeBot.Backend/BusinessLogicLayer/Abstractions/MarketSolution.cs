using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.ItemModel;
using SteamTradeBot.Backend.Services;
using ConfigurationManager = SteamTradeBot.Backend.Services.ConfigurationManager;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Abstractions;

public abstract class MarketSolution
{
    protected readonly SteamAPI SteamApi;
    protected readonly MarketDbAccess MarketDb;
    protected readonly ConfigurationManager ConfigurationManager;
    protected readonly StateManagerService StateManager;

    protected MarketSolution(SteamAPI api, MarketDbAccess marketDb, ConfigurationManager configurationManager, StateManagerService stateManager)
    {
        SteamApi = api;
        MarketDb = marketDb;
        ConfigurationManager = configurationManager;
        StateManager = stateManager;
    }

    public abstract void Perform(ItemPage itemPage);
}