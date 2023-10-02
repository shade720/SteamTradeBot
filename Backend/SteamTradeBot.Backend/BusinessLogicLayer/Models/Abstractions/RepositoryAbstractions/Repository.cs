using Microsoft.EntityFrameworkCore;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.RepositoryAbstractions;

public abstract class Repository
{
    protected readonly IDbContextFactory<TradeBotDataContext> TradeBotDataContextFactory;

    protected Repository(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory)
    {
        TradeBotDataContextFactory = tradeBotDataContextFactory;
    }
}