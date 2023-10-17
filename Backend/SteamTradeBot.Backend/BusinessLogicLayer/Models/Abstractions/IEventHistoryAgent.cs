using SteamTradeBot.Backend.BusinessLogicLayer.Models.StateModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;

public interface IEventHistoryAgent
{
    public Task<ServiceState> GetHistorySummaryAsync(string apiKey);
    public Task ClearHistorySummaryAsync(string apiKey);
    public Task<List<TradingEvent>> GetHistoryAsync(string apiKey);
    public Task ClearHistoryAsync(string apiKey);
}