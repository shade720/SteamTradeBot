using Microsoft.AspNetCore.SignalR;
using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Domain.ItemPageAggregate;
using SteamTradeBot.Backend.Domain.OrderAggregate;
using SteamTradeBot.Backend.Domain.StateAggregate;
using SteamTradeBot.Backend.Domain.TradingEventAggregate;

namespace SteamTradeBot.Backend.Application.EventHandlers;

internal class SignalTradingEventHandler : Hub, ITradingEventHandler
{
    private readonly ITradingEventHandler _innerTradingEventHandler;
    private readonly IHubContext<SignalTradingEventHandler> _context;

    public SignalTradingEventHandler(
        ITradingEventHandler innerTradingEventHandler, 
        IHubContext<SignalTradingEventHandler> context)
    {
        _innerTradingEventHandler = innerTradingEventHandler;
        _context = context;
    }

    #region Decorated

    public async Task OnErrorAsync(Exception exception)
    {
        var tradingEvent = new TradingEvent
        {
            Type = InfoType.Error,
            Time = DateTime.UtcNow,
            Info = $"Message: {exception.Message}, StackTrace: {exception.StackTrace}"
        };
        await PublishEvent(tradingEvent);
        await _innerTradingEventHandler.OnErrorAsync(exception);
    }

    public async Task OnItemAnalyzingAsync(ItemPage itemPage)
    {
        var tradingEvent = new TradingEvent
        {
            Type = InfoType.ItemAnalyzed,
            CurrentBalance = itemPage.CurrentBalance,
            Time = DateTime.UtcNow,
            Info = itemPage.EngItemName
        };
        await PublishEvent(tradingEvent);
        await _innerTradingEventHandler.OnItemAnalyzingAsync(itemPage);
    }

    public async Task OnItemSellingAsync(Order order)
    {
        var tradingEvent = new TradingEvent
        {
            Type = InfoType.ItemSold,
            Time = DateTime.UtcNow,
            Info = order.EngItemName,
            BuyPrice = order.BuyPrice,
            SellPrice = order.SellPrice
        };
        await PublishEvent(tradingEvent);
        await _innerTradingEventHandler.OnItemSellingAsync(order);
    }

    public async Task OnItemBuyingAsync(Order order)
    {
        var tradingEvent = new TradingEvent
        {
            Type = InfoType.ItemBought,
            Time = DateTime.UtcNow,
            Info = order.EngItemName,
            BuyPrice = order.BuyPrice,
            SellPrice = order.SellPrice
        };
        await PublishEvent(tradingEvent);
        await _innerTradingEventHandler.OnItemBuyingAsync(order);
    }

    public async Task OnItemCancellingAsync(Order order)
    {
        var tradingEvent = new TradingEvent
        {
            Type = InfoType.ItemCanceled,
            Time = DateTime.UtcNow,
            Info = order.EngItemName,
            BuyPrice = order.BuyPrice
        };
        await PublishEvent(tradingEvent);
        await _innerTradingEventHandler.OnItemCancellingAsync(order);
    }

    #endregion

    #region Forwarding

    public async Task OnTradingStartedAsync()
    {
        await _innerTradingEventHandler.OnTradingStartedAsync();
    }

    public async Task OnTradingStoppedAsync()
    {
        await _innerTradingEventHandler.OnTradingStoppedAsync();
    }

    public async Task OnLogInPendingAsync()
    {
        await _innerTradingEventHandler.OnLogInPendingAsync();
    }

    public async Task OnLoggedInAsync()
    {
        await _innerTradingEventHandler.OnLoggedInAsync();
    }

    public async Task OnLoggedOutAsync()
    {
        await _innerTradingEventHandler.OnLoggedOutAsync();
    }

    #endregion

    #region Private

    private async Task PublishState(ServiceState state)
    {
        await _context.Clients.All.SendAsync("getState", state);
    }

    private async Task PublishEvent(TradingEvent tradingEvent)
    {
        await _context.Clients.All.SendAsync("getEvents", tradingEvent);
    }

    #endregion
}