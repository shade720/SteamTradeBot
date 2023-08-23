using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.ItemModel;
using SteamTradeBot.Backend.Models.StateModel;
using SteamTradeBot.Backend.Models.Abstractions;
using Timer = System.Threading.Timer;

namespace SteamTradeBot.Backend.Services;

public class DbBasedStateManagerService : IStateManager
{
    private readonly HistoryDbAccess _historyDb;
    private readonly IConfigurationManager _configurationManager;
    private readonly Stopwatch _stopwatch;
    private const int UptimeUpdateDelayMs = 1000;
    private const int StartDelay = 0;

    private Timer _timer;
    
    public DbBasedStateManagerService(IConfigurationManager configurationManager, HistoryDbAccess historyDb)
    {
        _configurationManager = configurationManager;
        _historyDb = historyDb;
        _stopwatch = new Stopwatch();
    }

    public async Task EnsureCreated()
    {
        var storedState = await _historyDb.GetStateAsync(_configurationManager.ApiKey);
        if (storedState is null)
            await _historyDb.AddOrUpdateStateAsync(new ServiceState { ApiKey = _configurationManager.ApiKey });
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
        _timer = new Timer(UpdateTimer, null, StartDelay, UptimeUpdateDelayMs);
        Log.Information("Worker has started");
    }

    public async Task OnTradingStoppedAsync()
    {
        var storedState = await _historyDb.GetStateAsync(_configurationManager.ApiKey);
        if (storedState is not null)
        {
            storedState.WorkingState = ServiceWorkingState.Down;
            storedState.Uptime = TimeSpan.Zero;
            await _historyDb.AddOrUpdateStateAsync(storedState);
        }
        _stopwatch.Stop();
        _stopwatch.Reset();
        await _timer.DisposeAsync();
        Log.Information("Worker has stopped");
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
        var storedState = await _historyDb.GetStateAsync(_configurationManager.ApiKey);
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
            await _historyDb.AddOrUpdateStateAsync(storedState);
        }
    }

    private void UpdateTimer(object? source)
    {
        var storedState = _historyDb.GetStateAsync(_configurationManager.ApiKey).Result;
        if (storedState is not null)
        {
            storedState.Uptime = _stopwatch.Elapsed;
            var a = _historyDb.AddOrUpdateStateAsync(storedState);
        }
    }
}