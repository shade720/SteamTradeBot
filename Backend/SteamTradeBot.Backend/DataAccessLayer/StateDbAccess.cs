using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.Models.StateModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.DataAccessLayer;

public sealed class StateDbAccess
{
    private readonly IDbContextFactory<TradeBotDataContext> _tradeBotDataContextFactory;

    public StateDbAccess(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory)
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