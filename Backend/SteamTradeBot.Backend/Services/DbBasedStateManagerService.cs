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

    private Timer? _timer;
    
    public DbBasedStateManagerService(IConfigurationManager configurationManager, HistoryDbAccess historyDb)
    {
        _configurationManager = configurationManager;
        _historyDb = historyDb;
        _stopwatch = new Stopwatch();
    }

    public async Task EnsureStateCreated()
    {
        var storedState = await _historyDb.GetStateAsync(_configurationManager.ApiKey);
        if (storedState is not null)
            return;
        await _historyDb.AddOrUpdateStateAsync(new ServiceState { ApiKey = _configurationManager.ApiKey });
    }

    public async Task<ServiceState> GetServiceStateAsync(string apiKey)
    {
        var currentState =  await _historyDb.GetStateAsync(apiKey);
        return currentState ?? new ServiceState();
    }

    public async Task OnTradingStartedAsync()
    {
        var storedState = await _historyDb.GetStateAsync(_configurationManager.ApiKey) 
                          ?? throw new Exception("There is no state for this api key");
        storedState.WorkingState = ServiceWorkingState.Up;
        await _historyDb.AddOrUpdateStateAsync(storedState);
        _stopwatch.Start();
        _timer = new Timer(UpdateTimer, null, StartDelay, UptimeUpdateDelayMs);
        Log.Information("Worker has started");
    }

    public async Task OnTradingStoppedAsync()
    {
        var storedState = await _historyDb.GetStateAsync(_configurationManager.ApiKey) 
                          ?? throw new Exception("There is no state for this api key");
        storedState.WorkingState = ServiceWorkingState.Down;
        storedState.Uptime = TimeSpan.Zero;
        await _historyDb.AddOrUpdateStateAsync(storedState);
        _stopwatch.Stop();
        _stopwatch.Reset();
        if (_timer is not null)
            await _timer.DisposeAsync();
        Log.Information("Worker has stopped");
    }

    public async Task OnLogInPendingAsync()
    {
        var storedState = await _historyDb.GetStateAsync(_configurationManager.ApiKey) 
                          ?? throw new Exception("There is no state for this api key");
        storedState.IsLoggedIn = LogInState.Pending;
        await _historyDb.AddOrUpdateStateAsync(storedState);
    }

    public async Task OnLoggedInAsync()
    {
        var storedState = await _historyDb.GetStateAsync(_configurationManager.ApiKey) 
                          ?? throw new Exception("There is no state for this api key");
        storedState.IsLoggedIn = LogInState.LoggedIn;
        storedState.ApiKey = _configurationManager.ApiKey;
        await _historyDb.AddOrUpdateStateAsync(storedState);
    }

    public async Task OnLoggedOutAsync()
    {
        var storedState = await _historyDb.GetStateAsync(_configurationManager.ApiKey) 
                          ?? throw new Exception("There is no state for this api key");
        storedState.IsLoggedIn = LogInState.NotLoggedIn;
        await _historyDb.AddOrUpdateStateAsync(storedState);
    }

    public async Task OnErrorAsync(Exception exception)
    {
        var storedState = await _historyDb.GetStateAsync(_configurationManager.ApiKey) 
                          ?? throw new Exception("There is no state for this api key");
        storedState.Errors++;
        await _historyDb.AddOrUpdateStateAsync(storedState);
        await _historyDb.AddNewEventAsync(new TradingEvent
        {
            ApiKey = _configurationManager.ApiKey,
            Type = InfoType.Error,
            Time = DateTime.UtcNow,
            Info = $"Message: {exception.Message}, StackTrace: {exception.StackTrace}"
        });
    }

    public async Task OnItemAnalyzedAsync(ItemPage itemPage)
    {
        var storedState = await _historyDb.GetStateAsync(_configurationManager.ApiKey) 
                          ?? throw new Exception("There is no state for this api key");
        storedState.ItemsAnalyzed++;
        await _historyDb.AddOrUpdateStateAsync(storedState);
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
        var storedState = await _historyDb.GetStateAsync(_configurationManager.ApiKey) 
                          ?? throw new Exception("There is no state for this api key");
        storedState.ItemsSold++;
        await _historyDb.AddOrUpdateStateAsync(storedState);
        await _historyDb.AddNewEventAsync(new TradingEvent
        {
            ApiKey = _configurationManager.ApiKey,
            Type = InfoType.ItemSold,
            Time = DateTime.UtcNow,
            Info = order.EngItemName,
            SellPrice = order.Price,
            Profit = order.Price - order.Price
        });
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
        var storedState = await _historyDb.GetStateAsync(_configurationManager.ApiKey) 
                          ?? throw new Exception("There is no state for this api key");
        storedState.ItemsBought++;
        await _historyDb.AddOrUpdateStateAsync(storedState);
    }

    public async Task OnItemCancellingAsync(BuyOrder order)
    {
        var storedState = await _historyDb.GetStateAsync(_configurationManager.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.ItemCanceled++;
        await _historyDb.AddOrUpdateStateAsync(storedState);
        await _historyDb.AddNewEventAsync(new TradingEvent
        {
            ApiKey = _configurationManager.ApiKey,
            Type = InfoType.ItemCanceled,
            Time = DateTime.UtcNow,
            Info = order.EngItemName
        });
    }

    private void UpdateTimer(object? source)
    {
        var storedState = _historyDb.GetStateAsync(_configurationManager.ApiKey).Result 
                          ?? throw new Exception("There is no state for this api key");
        storedState.Uptime = _stopwatch.Elapsed;
        var _ = _historyDb.AddOrUpdateStateAsync(storedState);
    }
}