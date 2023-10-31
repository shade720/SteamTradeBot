using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;

namespace SteamTradeBot.Backend.Infrastructure;

internal sealed class SqlTokenRepository : ITokenRepository
{
    private readonly IDbContextFactory<TradeBotDataContext> _tradeBotDataContextFactory;

    public SqlTokenRepository(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory)
    {
        _tradeBotDataContextFactory = tradeBotDataContextFactory;
    }

    public async Task<bool> Contains(string token)
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        return await context.ApiKeys.AnyAsync(x => x.Value == token);
    }
}