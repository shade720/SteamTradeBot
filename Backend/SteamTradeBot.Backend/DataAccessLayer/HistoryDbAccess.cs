using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.Models.StateModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.DataAccessLayer;

public sealed class HistoryDbAccess
{
    private readonly IDbContextFactory<TradeBotDataContext> _tradeBotDataContextFactory;

    public HistoryDbAccess(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory)
    {
        _tradeBotDataContextFactory = tradeBotDataContextFactory;
    }

    public async Task AddNewEventAsync(TradingEvent tradingEvent)
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        await context.History.AddAsync(tradingEvent);
        await context.SaveChangesAsync();
    }

    public async Task<List<TradingEvent>> GetHistoryAsync(string apiKey)
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        return await context.History.Where(x => x.ApiKey == apiKey).ToListAsync();
    }

    public async Task ClearHistoryAsync()
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        await context.History.ExecuteDeleteAsync();
        await context.SaveChangesAsync();
    }
}