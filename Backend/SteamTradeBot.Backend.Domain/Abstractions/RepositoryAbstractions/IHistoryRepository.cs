using SteamTradeBot.Backend.Domain.TradingEventAggregate;

namespace SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;

public interface IHistoryRepository
{
    public Task AddNewEventAsync(TradingEvent tradingEvent);

    public Task<List<TradingEvent>> GetHistoryAsync(string apiKey);

    public Task ClearHistoryAsync(string apiKey);
}