using SteamTradeBot.Backend.Domain.StateAggregate;

namespace SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;

public interface IStateRepository
{
    public Task AddOrUpdateStateAsync(ServiceState state);

    public Task<ServiceState?> GetStateAsync(string apiKey);

    public Task<List<ServiceState>> GetStatesAsync();
}