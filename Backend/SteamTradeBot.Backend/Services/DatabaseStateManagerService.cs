using System;
using System.Diagnostics;
using System.Threading.Tasks;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.ItemModel;
using SteamTradeBot.Backend.Models.StateModel;
using SteamTradeBot.Backend.Models.Abstractions;

namespace SteamTradeBot.Backend.Services;

public class DatabaseStateManagerService : IStateManager
{
    private readonly HistoryDbAccess _historyDb;
    private readonly IConfigurationManager _configurationManager;
    private readonly Stopwatch _stopwatch;

    public DatabaseStateManagerService(IConfigurationManager configurationManager, HistoryDbAccess historyDb)
    {
        _configurationManager = configurationManager;
        _historyDb = historyDb;
        
        //var previousMarketEvents = _historyDb.GetHistoryAsync().Result;
        //_serviceState = new ServiceState
        //{
        //    Warnings = previousMarketEvents.Count(x => x.Type == InfoType.Warning),
        //    Errors = previousMarketEvents.Count(x => x.Type == InfoType.Error),
        //    ItemsAnalyzed = previousMarketEvents.Count(x => x.Type == InfoType.ItemAnalyzed),
        //    ItemsBought = previousMarketEvents.Count(x => x.Type == InfoType.ItemBought),
        //    ItemsSold = previousMarketEvents.Count(x => x.Type == InfoType.ItemSold),
        //    ItemCanceled = previousMarketEvents.Count(x => x.Type == InfoType.ItemCanceled),
        //    Events = previousMarketEvents
        //        .Where(x => x.Type is InfoType.ItemBought or InfoType.ItemCanceled or InfoType.ItemSold)
        //        .Select(x =>
        //        {
        //            var profit = x.Profit > 0 ? x.Profit.ToString() : string.Empty;
        //            return $"{x.Time}#{x.Info}#{x.Type}#{x.BuyPrice}#{x.SellPrice}#{profit}";
        //        })
        //        .ToList()
        //};
        _stopwatch = new Stopwatch();
    }

    public async Task<ServiceState> GetServiceStateAsync(string apiKey, long fromDate)
    {
        var currentState =  await _historyDb.GetStateAsync(apiKey);
        return currentState ?? new ServiceState();
    }

    public async Task OnTradingStartedAsync()
    {
        var storedState = await _historyDb.GetStateAsync(_configurationManager.ApiKey);
        if (storedState is not null)
        {
            storedState.WorkingState = ServiceWorkingState.Up;
            await _historyDb.AddOrUpdateStateAsync(storedState);
        }
        _stopwatch.Start();
    }

    public async Task OnTradingStoppedAsync()
    {
        var storedState = await _historyDb.GetStateAsync(_configurationManager.ApiKey);
        if (storedState is not null)
        {
            storedState.WorkingState = ServiceWorkingState.Down;
            await _historyDb.AddOrUpdateStateAsync(storedState);
        }
        _stopwatch.Stop();
        _stopwatch.Reset();
    }

    public async Task OnLogInPendingAsync()
    {
        var storedState = await _historyDb.GetStateAsync(_configurationManager.ApiKey);
        if (storedState is not null)
        {
            storedState.IsLoggedIn = LogInState.Pending;
            await _historyDb.AddOrUpdateStateAsync(storedState);
        }
    }

    public async Task OnLoggedInAsync()
    {
        var storedState = await _historyDb.GetStateAsync(_configurationManager.ApiKey);
        if (storedState is not null)
        {
            storedState.IsLoggedIn = LogInState.LoggedIn;
            storedState.ApiKey = _configurationManager.ApiKey;
            await _historyDb.AddOrUpdateStateAsync(storedState);
        }
    }

    public async Task OnLoggedOutAsync()
    {
        var storedState = await _historyDb.GetStateAsync(_configurationManager.ApiKey);
        if (storedState is not null)
        {
            storedState.IsLoggedIn = LogInState.NotLoggedIn;
            storedState.ApiKey = string.Empty;
            await _historyDb.AddOrUpdateStateAsync(storedState);
        }
    }

    public async Task OnErrorAsync(Exception exception)
    {
        await _historyDb.AddNewEventAsync(new TradingEvent
        {
            ApiKey = _configurationManager.ApiKey,
            Type = InfoType.Error,
            Time = DateTime.UtcNow,
            Info = $"Message: {exception.Message}, StackTrace: {exception.StackTrace}"
        });
        var storedState = await _historyDb.GetStateAsync(_configurationManager.ApiKey);
        if (storedState is not null)
        {
            storedState.Errors++;
            await _historyDb.AddOrUpdateStateAsync(storedState);
        }
    }

    public async Task OnItemAnalyzedAsync(ItemPage itemPage)
    {
        var storedState = await _historyDb.GetStateAsync(itemPage.ApiKey);
        if (storedState is not null)
        {
            storedState.ItemsAnalyzed++;
            await _historyDb.AddOrUpdateStateAsync(storedState);
        }
        await _historyDb.AddNewEventAsync(new TradingEvent
        {
            ApiKey = _configurationManager.ApiKey,
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
            ApiKey = _configurationManager.ApiKey,
            Type = InfoType.ItemSold,
            Time = DateTime.UtcNow,
            Info = order.EngItemName,
            SellPrice = order.Price,
            Profit = order.Price - order.Price
        });
        var storedState = await _historyDb.GetStateAsync(order.ApiKey);
        if (storedState is not null)
        {
            storedState.ItemsSold++;
            storedState.Events.Add($"{DateTime.UtcNow}#{order.EngItemName}#Sold#{order.Price}");
            await _historyDb.AddOrUpdateStateAsync(storedState);
        }
    }

    public async Task OnItemBuyingAsync(BuyOrder order)
    {
        await _historyDb.AddNewEventAsync(new TradingEvent
        {
            ApiKey = _configurationManager.ApiKey,
            Type = InfoType.ItemBought,
            Time = DateTime.UtcNow,
            Info = order.EngItemName,
            BuyPrice = order.Price
        });
        var storedState = await _historyDb.GetStateAsync(_configurationManager.ApiKey);
        if (storedState is not null)
        {
            storedState.ItemsBought++;
            storedState.Events.Add($"{DateTime.UtcNow}#{order.EngItemName}#Bought#{order.Price}");
            await _historyDb.AddOrUpdateStateAsync(storedState);
        }
    }

    public async Task OnItemCancellingAsync(BuyOrder order)
    {
        await _historyDb.AddNewEventAsync(new TradingEvent
        {
            ApiKey = _configurationManager.ApiKey,
            Type = InfoType.ItemCanceled,
            Time = DateTime.UtcNow,
            Info = order.EngItemName
        });
        var storedState = await _historyDb.GetStateAsync(_configurationManager.ApiKey);
        if (storedState is not null)
        {
            storedState.ItemCanceled++;
            storedState.Events.Add($"{DateTime.UtcNow}#{order.EngItemName}#Canceled#{order.Price}");
            await _historyDb.AddOrUpdateStateAsync(storedState);
        }
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

    public void OnTradingStarted(string apiKey)
    {
        throw new NotImplementedException();
    }

    public void OnTradingStopped(string apiKey)
    {
        throw new NotImplementedException();
    }

    public void OnLogInPending(string apiKey)
    {
        throw new NotImplementedException();
    }

    public void OnLoggedIn(string apiKey)
    {
        throw new NotImplementedException();
    }

    public void OnLoggedOut(string apiKey)
    {
        throw new NotImplementedException();
    }
}