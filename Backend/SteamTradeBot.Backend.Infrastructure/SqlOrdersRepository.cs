using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.OrderAggregate;

namespace SteamTradeBot.Backend.Infrastructure;

internal sealed class SqlOrdersRepository : IOrdersRepository
{
    private readonly IDbContextFactory<TradeBotDataContext> _tradeBotDataContextFactory;

    public SqlOrdersRepository(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory)
    {
        _tradeBotDataContextFactory = tradeBotDataContextFactory;
    }

    public async Task AddOrUpdateOrderAsync(Order order)
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        var orderSetToUpdate = await context.Orders.FirstOrDefaultAsync(o =>
            o.EngItemName == order.EngItemName && o.ApiKey == order.ApiKey && o.OrderType == order.OrderType);
        if (orderSetToUpdate is not null)
        {
            orderSetToUpdate.EngItemName = order.EngItemName;
            orderSetToUpdate.RusItemName = order.RusItemName;
            orderSetToUpdate.BuyPrice = order.BuyPrice;
            orderSetToUpdate.SellPrice = order.SellPrice;
            orderSetToUpdate.Quantity = order.Quantity;
            orderSetToUpdate.ItemUrl = order.ItemUrl;
            context.Orders.Update(orderSetToUpdate);
        }
        else
            await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();
    }

    public async Task<List<Order>> GetOrdersAsync(string apiKey, OrderType type)
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        return await context.Orders.Where(order => order.ApiKey == apiKey && order.OrderType == type).ToListAsync();
    }

    public async Task<Order?> GetOrderAsync(string engName, string apiKey, OrderType type)
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        return await context.Orders.FirstOrDefaultAsync(o => o.EngItemName == engName && o.ApiKey == apiKey && o.OrderType == type);
    }

    public async Task RemoveOrderAsync(Order order)
    {
        await using var context = await _tradeBotDataContextFactory.CreateDbContextAsync();
        var orderSetToRemove = await context.Orders.FirstOrDefaultAsync(o =>
            o.EngItemName == order.EngItemName && o.ApiKey == order.ApiKey && o.OrderType == order.OrderType);
        if (orderSetToRemove is not null)
            context.Orders.Remove(orderSetToRemove);
        await context.SaveChangesAsync();
    }
}

