using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.DataAccessLayer;

public class MarketDbAccess
{
    private readonly IDbContextFactory<TradeBotDataContext> _tradeBotDataContextFactory;

    public MarketDbAccess(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory)
    {
        _tradeBotDataContextFactory = tradeBotDataContextFactory;
    }

    #region BuyOrders

    public void AddOrUpdateBuyOrder(BuyOrder order)
    {
        using var context = _tradeBotDataContextFactory.CreateDbContext();
        if (context.BuyOrders.Contains(order))
            context.BuyOrders.Update(order);
        else
            context.BuyOrders.Add(order);
        context.SaveChanges();
    }

    public void RemoveBuyOrder(BuyOrder order)
    {
        using var context = _tradeBotDataContextFactory.CreateDbContext();
        var itemSetToRemove = context.BuyOrders.FirstOrDefault(i => i.EngItemName == order.EngItemName && i.RusItemName == order.RusItemName);
        if (itemSetToRemove is not null)
            context.BuyOrders.Remove(itemSetToRemove);
        context.SaveChanges();
    }

    public List<BuyOrder> GetBuyOrders()
    {
        using var context = _tradeBotDataContextFactory.CreateDbContext();
        return context.BuyOrders.ToList();
    }

    #endregion

    #region SellOrders

    public void AddOrUpdateSellOrder(SellOrder order)
    {
        using var context = _tradeBotDataContextFactory.CreateDbContext();
        if (context.SellOrders.Contains(order))
            context.SellOrders.Update(order);
        else
            context.SellOrders.Add(order);
        context.SaveChanges();
    }

    public void RemoveSellOrder(SellOrder order)
    {
        using var context = _tradeBotDataContextFactory.CreateDbContext();
        var itemSetToRemove = context.SellOrders.FirstOrDefault(i => i.EngItemName == order.EngItemName && i.RusItemName == order.RusItemName);
        if (itemSetToRemove is not null)
            context.SellOrders.Remove(itemSetToRemove);
        context.SaveChanges();
    }

    public List<SellOrder> GetSellOrders()
    {
        using var context = _tradeBotDataContextFactory.CreateDbContext();
        return context.SellOrders.ToList();
    }

    #endregion
}

