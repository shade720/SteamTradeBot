using System;
using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.ItemModel;
using SteamTradeBot.Backend.Models.StateModel;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Factories;

namespace SteamTradeBot.Backend.Services;

public sealed class DbBasedStateManagerService : IStateManager
{
    private readonly IConfigurationManager _configurationManager;
    private readonly StateDbAccess _stateDb;
    private readonly HistoryDbAccess _historyDb;
    private readonly UptimeProvider _uptimeProvider;
    
    public DbBasedStateManagerService(
        IConfigurationManager configurationManager, 
        StateDbAccess stateDb, 
        HistoryDbAccess historyDb)
    {
        _configurationManager = configurationManager;
        _stateDb = stateDb;
        _historyDb = historyDb;
        _uptimeProvider = new UptimeProvider();
        _uptimeProvider.UptimeUpdate += OnUptimeUpdated;
    }

    public async Task EnsureStateCreated()
    {
        var storedState = await _stateDb.GetStateAsync(_configurationManager.ApiKey);
        if (storedState is not null)
            return;
        await _stateDb.AddOrUpdateStateAsync(new ServiceState { ApiKey = _configurationManager.ApiKey });
    }

    public async Task<ServiceState> GetServiceStateAsync(string apiKey)
    {
        var currentState =  await _stateDb.GetStateAsync(apiKey);
        return currentState ?? new ServiceState();
    }

    public async Task OnTradingStartedAsync()
    {
        var storedState = await _stateDb.GetStateAsync(_configurationManager.ApiKey) 
                          ?? throw new Exception("There is no state for this api key");
        storedState.WorkingState = ServiceWorkingState.Up;
        await _stateDb.AddOrUpdateStateAsync(storedState);
        _uptimeProvider.StartCountdown();
        Log.Information("Worker has started");
    }

    public async Task OnTradingStoppedAsync()
    {
        var storedState = await _stateDb.GetStateAsync(_configurationManager.ApiKey) 
                          ?? throw new Exception("There is no state for this api key");
        storedState.WorkingState = ServiceWorkingState.Down;
        storedState.Uptime = TimeSpan.Zero;
        await _stateDb.AddOrUpdateStateAsync(storedState);
        _uptimeProvider.StopCountdown();
        Log.Information("Worker has stopped");
    }

    public async Task OnLogInPendingAsync()
    {
        var storedState = await _stateDb.GetStateAsync(_configurationManager.ApiKey) 
                          ?? throw new Exception("There is no state for this api key");
        storedState.IsLoggedIn = LogInState.Pending;
        await _stateDb.AddOrUpdateStateAsync(storedState);
    }

    public async Task OnLoggedInAsync()
    {
        var storedState = await _stateDb.GetStateAsync(_configurationManager.ApiKey) 
                          ?? throw new Exception("There is no state for this api key");
        storedState.IsLoggedIn = LogInState.LoggedIn;
        storedState.ApiKey = _configurationManager.ApiKey;
        await _stateDb.AddOrUpdateStateAsync(storedState);
    }

    public async Task OnLoggedOutAsync()
    {
        var storedState = await _stateDb.GetStateAsync(_configurationManager.ApiKey) 
                          ?? throw new Exception("There is no state for this api key");
        storedState.IsLoggedIn = LogInState.NotLoggedIn;
        await _stateDb.AddOrUpdateStateAsync(storedState);
    }

    public async Task OnErrorAsync(Exception exception)
    {
        var storedState = await _stateDb.GetStateAsync(_configurationManager.ApiKey) 
                          ?? throw new Exception("There is no state for this api key");
        storedState.Errors++;
        await _stateDb.AddOrUpdateStateAsync(storedState);
        await _historyDb.AddNewEventAsync(new TradingEvent
        {
            ApiKey = _configurationManager.ApiKey,
            Type = InfoType.Error,
            Time = DateTime.UtcNow,
            Info = $"Message: {exception.Message}, StackTrace: {exception.StackTrace}"
        });
    }

    public async Task OnItemAnalyzingAsync(ItemPage itemPage)
    {
        var storedState = await _stateDb.GetStateAsync(_configurationManager.ApiKey) 
                          ?? throw new Exception("There is no state for this api key");
        storedState.ItemsAnalyzed++;
        await _stateDb.AddOrUpdateStateAsync(storedState);
        await _historyDb.AddNewEventAsync(new TradingEvent
        {
            ApiKey = _configurationManager.ApiKey,
            Type = InfoType.ItemAnalyzed,
            CurrentBalance = itemPage.CurrentBalance,
            Time = DateTime.UtcNow,
            Info = itemPage.EngItemName
        });
    }

    public async Task OnItemSellingAsync(Order order)
    {
        var storedState = await _stateDb.GetStateAsync(_configurationManager.ApiKey) 
                          ?? throw new Exception("There is no state for this api key");
        storedState.ItemsSold++;
        await _stateDb.AddOrUpdateStateAsync(storedState);
        await _historyDb.AddNewEventAsync(new TradingEvent
        {
            ApiKey = _configurationManager.ApiKey,
            Type = InfoType.ItemSold,
            Time = DateTime.UtcNow,
            Info = order.EngItemName,
            SellPrice = order.SellPrice,
            Profit = order.SellPrice - order.BuyPrice - _configurationManager.SteamCommission
        });
    }

    public async Task OnItemBuyingAsync(Order order)
    {
        var storedState = await _stateDb.GetStateAsync(_configurationManager.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.ItemsBought++;
        await _stateDb.AddOrUpdateStateAsync(storedState);
        await _historyDb.AddNewEventAsync(new TradingEvent
        {
            ApiKey = _configurationManager.ApiKey,
            Type = InfoType.ItemBought,
            Time = DateTime.UtcNow,
            Info = order.EngItemName,
            BuyPrice = order.BuyPrice,
            SellPrice = order.SellPrice
        });
    }

    public async Task OnItemCancellingAsync(Order order)
    {
        var storedState = await _stateDb.GetStateAsync(_configurationManager.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.ItemCanceled++;
        await _stateDb.AddOrUpdateStateAsync(storedState);
        await _historyDb.AddNewEventAsync(new TradingEvent
        {
            ApiKey = _configurationManager.ApiKey,
            Type = InfoType.ItemCanceled,
            Time = DateTime.UtcNow,
            Info = order.EngItemName,
            BuyPrice = order.BuyPrice
        });
    }

    private void OnUptimeUpdated(TimeSpan uptime)
    {
        var storedState = _stateDb.GetStateAsync(_configurationManager.ApiKey).Result
                          ?? throw new Exception("There is no state for this api key");
        storedState.Uptime = uptime;
        var _ = _stateDb.AddOrUpdateStateAsync(storedState);
    }
}