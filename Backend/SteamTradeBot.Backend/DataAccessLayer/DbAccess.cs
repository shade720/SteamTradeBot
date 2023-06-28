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

    public void AddNewEvent(TradingEvent tradingEvent)
    {
        using var context = _marketDataContextFactory.CreateDbContext();
        context.History.Add(tradingEvent);
        context.SaveChanges();
    }

    public List<TradingEvent> GetHistory()
    {
        using var context = _marketDataContextFactory.CreateDbContext();
        return context.History.ToList();
    }

    public void ClearHistory()
    {
        using var context = _marketDataContextFactory.CreateDbContext();
        context.History.ExecuteDelete();
        context.SaveChanges();
    }

    #endregion

    #region MarketData

    public void AddOrUpdateOrder(Item item)
    {
        using var context = _marketDataContextFactory.CreateDbContext();
        if (context.Orders.Contains(item))
            context.Orders.Update(item);
        else
            context.Orders.Add(item);
        context.SaveChanges();
    }

    public void RemoveOrder(Item item)
    {
        using var context = _marketDataContextFactory.CreateDbContext();
        var itemSetToRemove = context.Orders.FirstOrDefault(i => i.EngItemName == item.EngItemName && i.RusItemName == item.RusItemName);
        if (itemSetToRemove is not null)
            context.Orders.Remove(itemSetToRemove);
        context.SaveChanges();
    }

    public List<Item> GetOrders()
    {
        using var context = _marketDataContextFactory.CreateDbContext();
        return context.Orders.ToList();
    }

    #endregion
}

