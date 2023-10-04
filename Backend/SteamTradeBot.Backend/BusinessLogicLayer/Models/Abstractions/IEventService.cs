using SteamTradeBot.Backend.BusinessLogicLayer.Models.ItemModel;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.StateModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;

public interface IEventService
{
    public Task<ServiceState> GetHistorySummaryAsync(string apiKey);
    public Task ClearHistorySummaryAsync(string apiKey);
    public Task<List<TradingEvent>> GetHistoryAsync(string apiKey);
    public Task ClearHistoryAsync(string apiKey);
    public Task OnTradingStartedAsync();
    public Task OnTradingStoppedAsync();
    public Task OnLogInPendingAsync();
    public Task OnLoggedInAsync();
    public Task OnLoggedOutAsync();
    public Task OnErrorAsync(Exception exception);
    public Task OnItemAnalyzingAsync(ItemPage itemPage);
    public Task OnItemSellingAsync(Order order);
    public Task OnItemBuyingAsync(Order order);
    public Task OnItemCancellingAsync(Order order);
}