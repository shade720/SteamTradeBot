using System;
using System.Threading.Tasks;
using SteamTradeBot.Backend.Models.ItemModel;
using SteamTradeBot.Backend.Models.StateModel;

namespace SteamTradeBot.Backend.Models.Abstractions;

public interface IStateManager
{
    public Task<ServiceState> GetServiceStateAsync(long fromDate);
    public void OnTradingStarted();
    public void OnTradingStopped();
    public void OnLogInPending();
    public void OnLoggedIn(string login);
    public void OnLoggedOut();
    public Task OnErrorAsync(Exception exception);
    public Task OnItemAnalyzedAsync(ItemPage itemPage);
    public Task OnItemSellingAsync(SellOrder order);
    public Task OnItemBuyingAsync(BuyOrder order);
    public Task OnItemCancellingAsync(BuyOrder order);
    public void OnError(Exception exception);
    public void OnItemAnalyzed(ItemPage itemPage);
    public void OnItemSelling(SellOrder order);
    public void OnItemBuying(BuyOrder order);
    public void OnItemCancelling(BuyOrder order);
}