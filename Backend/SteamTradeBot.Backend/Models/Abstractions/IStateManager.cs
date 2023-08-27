using System;
using System.Threading.Tasks;
using SteamTradeBot.Backend.Models.ItemModel;
using SteamTradeBot.Backend.Models.StateModel;

namespace SteamTradeBot.Backend.Models.Abstractions;

public interface IStateManager
{
    public Task<ServiceState> GetServiceStateAsync(string apiKey);
    public Task OnTradingStartedAsync();
    public Task OnTradingStoppedAsync();
    public Task OnLogInPendingAsync();
    public Task OnLoggedInAsync();
    public Task OnLoggedOutAsync();
    public Task OnErrorAsync(Exception exception);
    public Task OnItemAnalyzingAsync(ItemPage itemPage);
    public Task OnItemSellingAsync(SellOrder order);
    public Task OnItemBuyingAsync(BuyOrder order);
    public Task OnItemCancellingAsync(BuyOrder order);
}