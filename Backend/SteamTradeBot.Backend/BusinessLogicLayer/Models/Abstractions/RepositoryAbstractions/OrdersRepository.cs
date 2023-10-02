using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.ItemModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.RepositoryAbstractions;

public abstract class OrdersRepository : Repository
{
    protected OrdersRepository(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory) : base(tradeBotDataContextFactory) { }

    public abstract Task AddOrUpdateOrderAsync(Order order);

    public abstract Task<List<Order>> GetOrdersAsync(string apiKey, OrderType type);

    public abstract Task<Order?> GetOrderAsync(string engName, string apiKey, OrderType type);

    public abstract Task RemoveOrderAsync(Order order);
}