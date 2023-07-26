using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace SteamTradeBot.Backend.DataAccessLayer;

public class HistoryDbAccess
{
    private readonly IDbContextFactory<TradeBotDataContext> _tradeBotDataContextFactory;

    public HistoryDbAccess(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory)
    {
        _tradeBotDataContextFactory = tradeBotDataContextFactory;
    }

    public void AddNewEvent(TradingEvent tradingEvent)
    {
        using var context = _tradeBotDataContextFactory.CreateDbContext();
        context.History.Add(tradingEvent);
        context.SaveChanges();
    }

    public List<TradingEvent> GetHistory()
    {
        using var context = _tradeBotDataContextFactory.CreateDbContext();
        return context.History.ToList();
    }

    public void ClearHistory()
    {
        using var context = _tradeBotDataContextFactory.CreateDbContext();
        context.History.ExecuteDelete();
        context.SaveChanges();
    }
}