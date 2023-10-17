using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.Domain.StateModel;

namespace SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;

public abstract class StateRepository : Repository
{
    protected StateRepository(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory) : base(tradeBotDataContextFactory) { }

    public abstract Task AddOrUpdateStateAsync(ServiceState state);

    public abstract Task<ServiceState?> GetStateAsync(string apiKey);

    public abstract Task<List<ServiceState>> GetStatesAsync();
}