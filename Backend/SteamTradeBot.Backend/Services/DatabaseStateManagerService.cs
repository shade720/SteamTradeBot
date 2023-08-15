using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.ItemModel;
using SteamTradeBot.Backend.Models.StateModel;
using SteamTradeBot.Backend.Models.Abstractions;

namespace SteamTradeBot.Backend.Services;

public class DatabaseStateManagerService : IStateManager
{
    private readonly HistoryDbAccess _historyDb;
    private readonly ServiceState _serviceState;
    private readonly Stopwatch _stopwatch;

    public DatabaseStateManagerService(HistoryDbAccess historyDb)
    {
        _historyDb = historyDb;
        var previousMarketEvents = _historyDb.GetHistoryAsync().Result;
        _serviceState = new ServiceState
        {
            Warnings = previousMarketEvents.Count(x => x.Type == InfoType.Warning),
            Errors = previousMarketEvents.Count(x => x.Type == InfoType.Error),
            ItemsAnalyzed = previousMarketEvents.Count(x => x.Type == InfoType.ItemAnalyzed),
            ItemsBought = previousMarketEvents.Count(x => x.Type == InfoType.ItemBought),
            ItemsSold = previousMarketEvents.Count(x => x.Type == InfoType.ItemSold),
            ItemCanceled = previousMarketEvents.Count(x => x.Type == InfoType.ItemCanceled),
            Events = previousMarketEvents
                .Where(x => x.Type is InfoType.ItemBought or InfoType.ItemCanceled or InfoType.ItemSold)
                .Select(x =>
                {
                    var profit = x.Profit > 0 ? x.Profit.ToString() : string.Empty;
                    return $"{x.Time}#{x.Info}#{x.Type}#{x.BuyPrice}#{x.SellPrice}#{profit}";
                })
                .ToList()
        };
        _stopwatch = new Stopwatch();
    }

    public async Task<ServiceState> GetServiceStateAsync(long fromDate)
    {
        var previousMarketEvents = await _historyDb.GetHistoryAsync();
        var currentState = new ServiceState
        {
            Warnings = previousMarketEvents.Count(x => x.Type == InfoType.Warning),
            Errors = previousMarketEvents.Count(x => x.Type == InfoType.Error),
            ItemsAnalyzed = previousMarketEvents.Count(x => x.Type == InfoType.ItemAnalyzed),
            ItemsBought = previousMarketEvents.Count(x => x.Type == InfoType.ItemBought),
            ItemsSold = previousMarketEvents.Count(x => x.Type == InfoType.ItemSold),
            ItemCanceled = previousMarketEvents.Count(x => x.Type == InfoType.ItemCanceled),
            Events = previousMarketEvents
                .Where(x => x.Type is InfoType.ItemBought or InfoType.ItemCanceled or InfoType.ItemSold)
                .Select(x =>
                {
                    var profit = x.Profit > 0 ? x.Profit.ToString() : string.Empty;
                    return $"{x.Time}#{x.Info}#{x.Type}#{x.BuyPrice}#{x.SellPrice}#{profit}";
                })
                .ToList()
        };
        return currentState;
    }

    public void OnTradingStarted()
    {
        _serviceState.WorkingState = ServiceWorkingState.Up;
        _stopwatch.Start();
    }

    public void OnTradingStopped()
    {
        _serviceState.WorkingState = ServiceWorkingState.Down;
        _stopwatch.Stop();
        _stopwatch.Reset();
    }

    public void OnLogInPending()
    {
        _serviceState.IsLoggedIn = LogInState.Pending;
    }

    public void OnLoggedIn(string login)
    {
        _serviceState.IsLoggedIn = LogInState.LoggedIn;
        _serviceState.CurrentUser = login;
    }

    public void OnLoggedOut()
    {
        _serviceState.IsLoggedIn = LogInState.NotLoggedIn;
        _serviceState.CurrentUser = string.Empty;
    }

    public async Task OnErrorAsync(Exception exception)
    {
        await _historyDb.AddNewEventAsync(new TradingEvent
        {
            Type = InfoType.Error,
            Time = DateTime.UtcNow,
            Info = $"Message: {exception.Message}, StackTrace: {exception.StackTrace}"
        });
        _serviceState.Errors++;
    }

    public async Task OnItemAnalyzedAsync(ItemPage itemPage)
    {
        _serviceState.ItemsAnalyzed++;
        await _historyDb.AddNewEventAsync(new TradingEvent
        {
            Type = InfoType.ItemAnalyzed,
            CurrentBalance = itemPage.CurrentBalance,
            Time = DateTime.UtcNow,
            Info = itemPage.EngItemName
        });
    }

    public async Task OnItemSellingAsync(SellOrder order)
    {
        await _historyDb.AddNewEventAsync(new TradingEvent
        {
            Type = InfoType.ItemSold,
            Time = DateTime.UtcNow,
            Info = order.EngItemName,
            SellPrice = order.Price,
            Profit = order.Price - order.Price
        });
        _serviceState.ItemsSold++;
        _serviceState.Events.Add($"{DateTime.UtcNow}#{order.EngItemName}#Sold#{order.Price}");
    }

    public async Task OnItemBuyingAsync(BuyOrder order)
    {
        await _historyDb.AddNewEventAsync(new TradingEvent
        {
            Type = InfoType.ItemBought,
            Time = DateTime.UtcNow,
            Info = order.EngItemName,
            BuyPrice = order.Price
        });
        _serviceState.ItemsBought++;
        _serviceState.Events.Add($"{DateTime.UtcNow}#{order.EngItemName}#Bought#{order.Price}");
    }

    public async Task OnItemCancellingAsync(BuyOrder order)
    {
        await _historyDb.AddNewEventAsync(new TradingEvent
        {
            Type = InfoType.ItemCanceled,
            Time = DateTime.UtcNow,
            Info = order.EngItemName
        });
        _serviceState.ItemCanceled++;
        _serviceState.Events.Add($"{DateTime.UtcNow}#{order.EngItemName}#Canceled#{order.Price}");
    }

    public void OnError(Exception exception)
    {
        throw new NotImplementedException();
    }

    public void OnItemAnalyzed(ItemPage itemPage)
    {
        throw new NotImplementedException();
    }

    public void OnItemSelling(SellOrder order)
    {
        throw new NotImplementedException();
    }

    public void OnItemBuying(BuyOrder order)
    {
        throw new NotImplementedException();
    }

    public void OnItemCancelling(BuyOrder order)
    {
        throw new NotImplementedException();
    }
}