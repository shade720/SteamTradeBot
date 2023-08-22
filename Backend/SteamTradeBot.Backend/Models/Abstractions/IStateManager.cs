using System;
using System.Threading.Tasks;
using SteamTradeBot.Backend.Models.ItemModel;
using SteamTradeBot.Backend.Models.StateModel;

namespace SteamTradeBot.Backend.Models.Abstractions;

public interface IStateManager
{
    #region Async

    public Task<ServiceState> GetServiceStateAsync(string apiKey, long fromDate);
    public Task OnTradingStartedAsync();
    public Task OnTradingStoppedAsync();
    public Task OnLogInPendingAsync();
    public Task OnLoggedInAsync();
    public Task OnLoggedOutAsync();
    public Task OnErrorAsync(Exception exception);
    public Task OnItemAnalyzedAsync(ItemPage itemPage);
    public Task OnItemSellingAsync(SellOrder order);
    public Task OnItemBuyingAsync(BuyOrder order);
    public Task OnItemCancellingAsync(BuyOrder order);

    #endregion

    #region Sync

    public void OnError(Exception exception);
    public void OnItemAnalyzed(ItemPage itemPage);
    public void OnItemSelling(SellOrder order);
    public void OnItemBuying(BuyOrder order);
    public void OnItemCancelling(BuyOrder order);
    public void OnTradingStarted(string apiKey);
    public void OnTradingStopped(string apiKey);
    public void OnLogInPending(string apiKey);
    public void OnLoggedIn(string apiKey);
    public void OnLoggedOut(string apiKey);

    #endregion
}