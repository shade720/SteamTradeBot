using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.ItemModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.EventHandlers;

public class LogEventHandler : ITradingEventHandler
{
    private readonly ITradingEventHandler _innerEventHandler;

    public LogEventHandler(ITradingEventHandler innerEventHandler)
    {
        _innerEventHandler = innerEventHandler;
    }

    public async Task OnTradingStartedAsync()
    {
        await _innerEventHandler.OnTradingStartedAsync();
        Log.Information("Worker has started");
    }

    public async Task OnTradingStoppedAsync()
    {
        await _innerEventHandler.OnTradingStoppedAsync();
        Log.Information("Worker has stopped");
    }

    public async Task OnLogInPendingAsync()
    {
        Log.Information("Log in pending...");
        await _innerEventHandler.OnLogInPendingAsync();
    }

    public async Task OnLoggedInAsync()
    {
        Log.Information("Successfully logged in!");
        await _innerEventHandler.OnLoggedInAsync();
    }

    public async Task OnLoggedOutAsync()
    {
        Log.Information("Successfully logged out!");
        await _innerEventHandler.OnLoggedOutAsync();
    }

    public async Task OnErrorAsync(Exception exception)
    {
        Log.Logger.Error("Item skipped due to error -> \r\nMessage: {0}, StackTrace: {1}",
            exception.Message, exception.StackTrace);
        await _innerEventHandler.OnErrorAsync(exception);
    }

    public async Task OnItemAnalyzingAsync(ItemPage itemPage)
    {
        await _innerEventHandler.OnItemAnalyzingAsync(itemPage);
        Log.Logger.Information("Item {0} provided:\r\nUrl: {1}\r\nRusName: {2}\r\nExisting order: (Price: {3}; Quantity: {4})\r\nBuy order book: {5}...\r\nSell order book: {6}...",
            itemPage.ToString(), 
            itemPage.ItemUrl, 
            itemPage.RusItemName,
            itemPage.MyBuyOrder is null ? "null" : itemPage.MyBuyOrder.BuyPrice,
            itemPage.MyBuyOrder is null ? "null" : itemPage.MyBuyOrder.Quantity,
            string.Join("; ", itemPage.BuyOrderBook.Select(x => Math.Round(x.Price, 2, MidpointRounding.AwayFromZero)).Take(3)),
            string.Join("; ", itemPage.SellOrderBook.Select(x => Math.Round(x.Price, 2, MidpointRounding.AwayFromZero)).Take(3)));
    }

    public async Task OnItemSellingAsync(Order order)
    {
        await _innerEventHandler.OnItemSellingAsync(order);
        Log.Information("Sell order {0} (Price: {1}) placed successfully.",
            order.EngItemName, order.SellPrice);
    }

    public async Task OnItemBuyingAsync(Order order)
    {
        await _innerEventHandler.OnItemBuyingAsync(order);
        Log.Logger.Information("Buy order {0} has been placed:\r\nItem name: {1}\r\nUrl: {2}\r\nBuy price: {3}\r\nEstimated sell price: {4}\r\nQuantity: {5}",
            order.EngItemName, order.ItemUrl, order.ItemUrl, order.BuyPrice, order.SellPrice, order.Quantity);
    }

    public async Task OnItemCancellingAsync(Order order)
    {
        await _innerEventHandler.OnItemCancellingAsync(order);
        Log.Information("Buy order {0} (Price: {1}, Quantity: {2}) has been canceled.",
            order.EngItemName, order.BuyPrice, order.Quantity);
    }
}