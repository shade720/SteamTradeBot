using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SteamTradeBot.Backend.DataAccessLayer;

public sealed class TokenDbAccess
{
    private readonly IDbContextFactory<TradeBotDataContext> _tradeBotDataContextFactory;

    public TokenDbAccess(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory)
    {
        _tradeBotDataContextFactory = tradeBotDataContextFactory;
    }

    public async Task<bool> Contains(string token)
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        return await context.ApiKeys.AnyAsync(x => x.Value == token);
    }
}