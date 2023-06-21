using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.BusinessLogicLayer.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer.DataAccessLayer;

public class DbAccess
{
    private readonly IDbContextFactory<MarketDataContext> _marketDataContextFactory;
    private readonly IDbContextFactory<HistoryDataContext> _historyDataContextFactory;

    public DbAccess(IDbContextFactory<MarketDataContext> marketDataContextFactory, IDbContextFactory<HistoryDataContext> historyDataContextFactory)
    {
        _marketDataContextFactory = marketDataContextFactory;
        _historyDataContextFactory = historyDataContextFactory;
    }

    #region HistoryData

    public void AddNewStateInfo(StateInfo stateInfo)
    {
        using var context = _historyDataContextFactory.CreateDbContext();
        context.States.Add(stateInfo);
        context.SaveChanges();
    }

    public List<StateInfo> GetHistory()
    {
        using var context = _historyDataContextFactory.CreateDbContext();
        return context.States.ToList();
    }

    public void ClearHistory()
    {
        using var context = _historyDataContextFactory.CreateDbContext();
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

