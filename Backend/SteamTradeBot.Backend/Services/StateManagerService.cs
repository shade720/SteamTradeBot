using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Linq;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.ItemModel;
using SteamTradeBot.Backend.Models.StateModel;

namespace SteamTradeBot.Backend.Services;

public class StateManagerService
{
    private readonly HistoryDbAccess _historyDb;
    private readonly ServiceState _serviceState;
    private readonly Stopwatch _stopwatch;

    public StateManagerService(HistoryDbAccess historyDb)
    {
        _historyDb = historyDb;
        var previousMarketEvents = _historyDb.GetHistory();
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

    public ServiceState GetServiceState(long fromDate)
    {
        var serviceStateCopy = new ServiceState
        {
            WorkingState = _serviceState.WorkingState,
            ItemsAnalyzed = _serviceState.ItemsAnalyzed,
            ItemsBought = _serviceState.ItemsBought,
            ItemsSold = _serviceState.ItemsSold,
            ItemCanceled = _serviceState.ItemCanceled,
            Errors = _serviceState.Errors,
            Warnings = _serviceState.Warnings,
            Events = new List<string>(_serviceState.Events.Where(x => DateTime.Parse(x.Split('#')[0]).Ticks > fromDate)),
            Uptime = _stopwatch.Elapsed,
            CurrentUser = _serviceState.CurrentUser,
            IsLoggedIn = _serviceState.IsLoggedIn,
        };
        return serviceStateCopy;
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

    public void OnError(Exception exception)
    {
        _historyDb.AddNewEvent(new TradingEvent
        {
            Type = InfoType.Error,
            Time = DateTime.UtcNow,
            Info = $"Message: {exception.Message}, StackTrace: {exception.StackTrace}"
        });
        _serviceState.Errors++;
    }

    public void OnItemAnalyzed(ItemPage itemPage)
    {
        _serviceState.ItemsAnalyzed++;
        _historyDb.AddNewEvent(new TradingEvent
        {
            Type = InfoType.ItemAnalyzed,
            CurrentBalance = itemPage.CurrentBalance,
            Time = DateTime.UtcNow,
            Info = itemPage.EngItemName
        });
    }

    public void OnItemSelling(SellOrder order)
    {
        _historyDb.AddNewEvent(new TradingEvent
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

    public void OnItemBuying(BuyOrder order)
    {
        _historyDb.AddNewEvent(new TradingEvent
        {
            Type = InfoType.ItemBought,
            Time = DateTime.UtcNow,
            Info = order.EngItemName,
            BuyPrice = order.Price
        });
        _serviceState.ItemsBought++;
        _serviceState.Events.Add($"{DateTime.UtcNow}#{order.EngItemName}#Bought#{order.Price}");
    }

    public void OnItemCancelling(BuyOrder order)
    {
        _historyDb.AddNewEvent(new TradingEvent
        {
            Type = InfoType.ItemCanceled,
            Time = DateTime.UtcNow,
            Info = order.EngItemName
        });
        _serviceState.ItemCanceled++;
        _serviceState.Events.Add($"{DateTime.UtcNow}#{order.EngItemName}#Canceled#{order.Price}");
    }
}