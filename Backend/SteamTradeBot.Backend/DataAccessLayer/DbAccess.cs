using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

    #region BuyOrders

    public void AddOrUpdateBuyOrder(BuyOrder order)
    {
        using var context = _marketDataContextFactory.CreateDbContext();
        if (context.BuyOrders.Contains(order))
            context.BuyOrders.Update(order);
        else
            context.BuyOrders.Add(order);
        context.SaveChanges();
    }

    public void RemoveBuyOrder(BuyOrder order)
    {
        using var context = _marketDataContextFactory.CreateDbContext();
        var itemSetToRemove = context.BuyOrders.FirstOrDefault(i => i.EngItemName == order.EngItemName && i.RusItemName == order.RusItemName);
        if (itemSetToRemove is not null)
            context.BuyOrders.Remove(itemSetToRemove);
        context.SaveChanges();
    }

    public List<BuyOrder> GetBuyOrders()
    {
        using var context = _marketDataContextFactory.CreateDbContext();
        return context.BuyOrders.ToList();
    }

    #endregion

    #region SellOrders

    public void AddOrUpdateSellOrder(SellOrder order)
    {
        using var context = _marketDataContextFactory.CreateDbContext();
        if (context.SellOrders.Contains(order))
            context.SellOrders.Update(order);
        else
            context.SellOrders.Add(order);
        context.SaveChanges();
    }

    public void RemoveSellOrder(SellOrder order)
    {
        using var context = _marketDataContextFactory.CreateDbContext();
        var itemSetToRemove = context.SellOrders.FirstOrDefault(i => i.EngItemName == order.EngItemName && i.RusItemName == order.RusItemName);
        if (itemSetToRemove is not null)
            context.SellOrders.Remove(itemSetToRemove);
        context.SaveChanges();
    }

    public List<SellOrder> GetSellOrders()
    {
        using var context = _marketDataContextFactory.CreateDbContext();
        return context.SellOrders.ToList();
    }

    #endregion

    #endregion
}

