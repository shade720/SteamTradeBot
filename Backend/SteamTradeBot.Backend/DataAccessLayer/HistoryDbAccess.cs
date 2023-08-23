using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.Models.StateModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.DataAccessLayer;

public class HistoryDbAccess
{
    private readonly IDbContextFactory<TradeBotDataContext> _tradeBotDataContextFactory;

    public HistoryDbAccess(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory)
    {
        _tradeBotDataContextFactory = tradeBotDataContextFactory;
    }

    #region Events

    public async Task AddNewEventAsync(TradingEvent tradingEvent)
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        await context.History.AddAsync(tradingEvent);
        await context.SaveChangesAsync();
    }

    public async Task<List<TradingEvent>> GetHistoryAsync()
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        return await context.History.ToListAsync();
    }

    public async Task ClearHistoryAsync()
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        await context.History.ExecuteDeleteAsync();
        await context.SaveChangesAsync();
    }

    #endregion

    #region State


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

    #endregion
}