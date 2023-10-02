using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.BusinessLogicLayer.Models;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.StateModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.DataAccessLayer;

public sealed class SqlHistoryRepository : HistoryRepository
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