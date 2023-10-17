using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.Domain;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.StateModel;

namespace SteamTradeBot.Backend.Infrastructure;

internal sealed class SqlHistoryRepository : HistoryRepository
{
    public SqlHistoryRepository(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory) : base(tradeBotDataContextFactory) { }

    public override async Task AddNewEventAsync(TradingEvent tradingEvent)
    {
        await using var context = await TradeBotDataContextFactory.CreateDbContextAsync();
        await context.History.AddAsync(tradingEvent);
        await context.SaveChangesAsync();
    }

    public override async Task<List<TradingEvent>> GetHistoryAsync(string apiKey)
    {
        await using var context = await TradeBotDataContextFactory.CreateDbContextAsync();
        return await context.History.Where(x => x.ApiKey == apiKey).ToListAsync();
    }

    public override async Task ClearHistoryAsync(string apiKey)
    {
        await using var context = await TradeBotDataContextFactory.CreateDbContextAsync();
        var eventsSetToRemove = await context.History.Where(t => t.ApiKey == apiKey).ToListAsync();
        context.History.RemoveRange(eventsSetToRemove);
        await context.SaveChangesAsync();
    }
}