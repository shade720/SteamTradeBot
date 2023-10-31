using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.StateAggregate;

namespace SteamTradeBot.Backend.Infrastructure;

internal sealed class SqlStateRepository : IStateRepository
{
    private readonly IDbContextFactory<TradeBotDataContext> _tradeBotDataContextFactory;

    public SqlStateRepository(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory)
    {
        _tradeBotDataContextFactory = tradeBotDataContextFactory;
    }

    public async Task AddOrUpdateStateAsync(ServiceState state)
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        if (await context.ServiceStates.ContainsAsync(state))
            context.ServiceStates.Update(state);
        else
            await context.ServiceStates.AddAsync(state);
        await context.SaveChangesAsync();
    }

    public async Task<ServiceState?> GetStateAsync(string apiKey)
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        return await context.ServiceStates.FirstOrDefaultAsync(x => x.ApiKey == apiKey);
    }

    public async Task<List<ServiceState>> GetStatesAsync()
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        return await context.ServiceStates.ToListAsync();
    }
}