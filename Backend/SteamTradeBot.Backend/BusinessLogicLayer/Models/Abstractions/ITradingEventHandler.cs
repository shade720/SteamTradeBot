using SteamTradeBot.Backend.BusinessLogicLayer.Models.ItemModel;
using System;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;

public interface ITradingEventHandler
{
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