using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models;
using SteamTradeBot.Backend.Services;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Solutions;

public abstract class MarketSolution
{
    protected readonly SteamAPI SteamApi;
    protected readonly MarketDbAccess MarketDb;
    protected readonly ConfigurationManager ConfigurationManager;

    protected MarketSolution(SteamAPI api, MarketDbAccess marketDb, ConfigurationManager configurationManager)
    {
        SteamApi = api;
        MarketDb = marketDb;
        ConfigurationManager = configurationManager;
    }

    public abstract void Perform(ItemPage itemPage);
}