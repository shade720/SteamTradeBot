using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.Domain;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.ItemModel;

namespace SteamTradeBot.Backend.Infrastructure;

internal sealed class SqlOrdersRepository : OrdersRepository
{
    public SqlOrdersRepository(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory) : base(tradeBotDataContextFactory) { }

    public override async Task AddOrUpdateOrderAsync(Order order)
    {
        await using var context = await TradeBotDataContextFactory.CreateDbContextAsync();
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

    public override async Task<List<Order>> GetOrdersAsync(string apiKey, OrderType type)
    {
        await using var context = await TradeBotDataContextFactory.CreateDbContextAsync();
        return await context.Orders.Where(order => order.ApiKey == apiKey && order.OrderType == type).ToListAsync();
    }

    public override async Task<Order?> GetOrderAsync(string engName, string apiKey, OrderType type)
    {
        await using var context = await TradeBotDataContextFactory.CreateDbContextAsync();
        return await context.Orders.FirstOrDefaultAsync(o => o.EngItemName == engName && o.ApiKey == apiKey && o.OrderType == type);
    }

    public override async Task RemoveOrderAsync(Order order)
    {
        await using var context = await TradeBotDataContextFactory.CreateDbContextAsync();
        var orderSetToRemove = await context.Orders.FirstOrDefaultAsync(o =>
            o.EngItemName == order.EngItemName && o.ApiKey == order.ApiKey && o.OrderType == order.OrderType);
        if (orderSetToRemove is not null)
            context.Orders.Remove(orderSetToRemove);
        await context.SaveChangesAsync();
    }
}

