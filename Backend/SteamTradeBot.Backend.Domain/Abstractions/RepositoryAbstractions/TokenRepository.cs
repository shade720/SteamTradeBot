using Microsoft.EntityFrameworkCore;

namespace SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;

public abstract class TokenRepository : Repository
{
    protected TokenRepository(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory) : base(tradeBotDataContextFactory) { }

    public abstract Task<bool> Contains(string token);
}