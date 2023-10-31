using SteamTradeBot.Backend.Domain.OrderAggregate;

namespace SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;

public interface IOrdersRepository
{
    public Task AddOrUpdateOrderAsync(Order order);

    public Task<List<Order>> GetOrdersAsync(string apiKey, OrderType type);

    public Task<Order?> GetOrderAsync(string engName, string apiKey, OrderType type);

    public Task RemoveOrderAsync(Order order);
}