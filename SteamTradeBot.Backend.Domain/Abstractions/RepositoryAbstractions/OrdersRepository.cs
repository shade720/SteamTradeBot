using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.Domain.ItemModel;

namespace SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;

public abstract class OrdersRepository : Repository
{
    protected OrdersRepository(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory) : base(tradeBotDataContextFactory) { }

    public abstract Task AddOrUpdateOrderAsync(Order order);

    public abstract Task<List<Order>> GetOrdersAsync(string apiKey, OrderType type);

    public abstract Task<Order?> GetOrderAsync(string engName, string apiKey, OrderType type);

    public abstract Task RemoveOrderAsync(Order order);
}