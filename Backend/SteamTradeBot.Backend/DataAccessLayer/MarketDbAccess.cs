using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.DataAccessLayer;

public sealed class MarketDbAccess
{
    private readonly IDbContextFactory<TradeBotDataContext> _tradeBotDataContextFactory;

    public MarketDbAccess(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory)
    {
        _tradeBotDataContextFactory = tradeBotDataContextFactory;
    }

    #region BuyOrders

    public async Task AddOrUpdateBuyOrderAsync(BuyOrder order)
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        if (await context.BuyOrders.ContainsAsync(order))
            context.BuyOrders.Update(order);
        else
            await context.BuyOrders.AddAsync(order);
        await context.SaveChangesAsync();
    }

    public async Task RemoveBuyOrderAsync(BuyOrder order)
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        var itemSetToRemove = await context.BuyOrders.FirstOrDefaultAsync(i => i.EngItemName == order.EngItemName && i.RusItemName == order.RusItemName && i.ApiKey == order.ApiKey);
        if (itemSetToRemove is not null)
            context.BuyOrders.Remove(itemSetToRemove);
        await context.SaveChangesAsync();
    }

    public async Task<List<BuyOrder>> GetBuyOrdersAsync(string apiKey)
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        return await context.BuyOrders.Where(order => order.ApiKey == apiKey).ToListAsync();
    }

    public async Task<BuyOrder?> GetBuyOrderAsync(string engName, string apiKey)
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        return await context.BuyOrders.FirstOrDefaultAsync(i => i.EngItemName == engName && i.ApiKey == apiKey);
    }

    #endregion

    #region SellOrders

    public async Task AddOrUpdateSellOrderAsync(SellOrder order)
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        if (await context.SellOrders.ContainsAsync(order))
            context.SellOrders.Update(order);
        else
            await context.SellOrders.AddAsync(order);
        await context.SaveChangesAsync();
    }

    public async Task RemoveSellOrderAsync(SellOrder order)
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        var itemSetToRemove = await context.SellOrders.FirstOrDefaultAsync(i => i.EngItemName == order.EngItemName && i.RusItemName == order.RusItemName && i.ApiKey == order.ApiKey);
        if (itemSetToRemove is not null)
            context.SellOrders.Remove(itemSetToRemove);
        await context.SaveChangesAsync();
    }

    public async Task<List<SellOrder>> GetSellOrdersAsync()
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        return await context.SellOrders.ToListAsync();
    }

    #endregion
}

