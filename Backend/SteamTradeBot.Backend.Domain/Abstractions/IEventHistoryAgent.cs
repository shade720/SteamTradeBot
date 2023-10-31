using SteamTradeBot.Backend.Domain.StateAggregate;
using SteamTradeBot.Backend.Domain.TradingEventAggregate;

namespace SteamTradeBot.Backend.Domain.Abstractions;

public interface IEventHistoryAgent
{
    public Task<ServiceState> GetHistorySummaryAsync(string apiKey);
    public Task ClearHistorySummaryAsync(string apiKey);
    public Task<List<TradingEvent>> GetHistoryAsync(string apiKey);
    public Task ClearHistoryAsync(string apiKey);
}