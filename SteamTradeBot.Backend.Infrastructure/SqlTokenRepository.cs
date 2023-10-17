using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.Domain;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;

namespace SteamTradeBot.Backend.Infrastructure;

internal sealed class SqlTokenRepository : TokenRepository
{
    public SqlTokenRepository(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory) : base(tradeBotDataContextFactory) { }

    public override async Task<bool> Contains(string token)
    {
        await using var context = await TradeBotDataContextFactory.CreateDbContextAsync();
        return await context.ApiKeys.AnyAsync(x => x.Value == token);
    }
}