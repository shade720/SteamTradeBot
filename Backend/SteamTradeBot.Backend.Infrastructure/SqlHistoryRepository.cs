using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.TradingEventAggregate;

namespace SteamTradeBot.Backend.Infrastructure;

internal sealed class SqlHistoryRepository : IHistoryRepository
{
    private readonly IDbContextFactory<TradeBotDataContext> _tradeBotDataContextFactory;

    public SqlHistoryRepository(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory)
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

    public async Task ClearHistoryAsync(string apiKey)
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        var eventsSetToRemove = await context.History.Where(t => t.ApiKey == apiKey).ToListAsync();
        context.History.RemoveRange(eventsSetToRemove);
        await context.SaveChangesAsync();
    }
}