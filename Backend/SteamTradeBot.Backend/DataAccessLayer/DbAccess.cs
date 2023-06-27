using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.BusinessLogicLayer;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.DataAccessLayer;

public class DbAccess
{
    private readonly IDbContextFactory<MarketDataContext> _marketDataContextFactory;

    public DbAccess(IDbContextFactory<MarketDataContext> marketDataContextFactory)
    {
        _marketDataContextFactory = marketDataContextFactory;
    }

    #region HistoryData

    public void AddNewStateInfo(StateChangingEvent stateChangingEvent)
    {
        using var context = _marketDataContextFactory.CreateDbContext();
        context.States.Add(stateChangingEvent);
        context.SaveChanges();
    }

    public List<StateChangingEvent> GetHistory()
    {
        using var context = _marketDataContextFactory.CreateDbContext();
        return context.States.ToList();
    }

    public void ClearHistory()
    {
        using var context = _marketDataContextFactory.CreateDbContext();
        context.States.ExecuteDelete();
        context.SaveChanges();
    }

    #endregion

    #region MarketData

    public void AddItem(Item item)
    {
        using var context = _marketDataContextFactory.CreateDbContext();
        context.Items.Add(item);
        context.SaveChanges();
    }

    public void UpdateItem(Item item)
    {
        using var context = _marketDataContextFactory.CreateDbContext();
        if (context.Items.Contains(item))
            context.Items.Update(item);
        context.SaveChanges();
    }

    public List<Item> GetItems()
    {
        using var context = _marketDataContextFactory.CreateDbContext();
        return context.Items.ToList();
    }

    public void ClearItems()
    {
        using var context = _marketDataContextFactory.CreateDbContext();
        context.Items.ExecuteDelete();
        context.SaveChanges();
    }

    #endregion
}

