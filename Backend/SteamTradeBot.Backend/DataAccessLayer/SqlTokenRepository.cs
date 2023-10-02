using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.BusinessLogicLayer.Models;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.RepositoryAbstractions;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.DataAccessLayer;

public sealed class SqlTokenRepository : TokenRepository
{
    public SqlTokenRepository(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory) : base(tradeBotDataContextFactory) { }

    public override async Task<bool> Contains(string token)
    {
        await using var context = await TradeBotDataContextFactory.CreateDbContextAsync();
        return await context.ApiKeys.AnyAsync(x => x.Value == token);
    }
}