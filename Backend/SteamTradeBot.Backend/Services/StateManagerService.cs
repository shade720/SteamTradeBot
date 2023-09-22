using Microsoft.AspNetCore.SignalR;
using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Factories;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;
using SteamTradeBot.Backend.Models.StateModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.Services;

public sealed class StateManagerService : Hub, IStateManager
{
    private readonly IConfigurationManager _configurationManager;
    private readonly IHubContext<StateManagerService> _context;
    private readonly StateDbAccess _stateDb;
    private readonly HistoryDbAccess _historyDb;
    private readonly UptimeProvider _uptimeProvider;
    
    #region PublicInterface

    public StateManagerService(
        IConfigurationManager configurationManager,
        IHubContext<StateManagerService> context,
        StateDbAccess stateDb,
        HistoryDbAccess historyDb)
    {
        _configurationManager = configurationManager;
        _context = context;
        _stateDb = stateDb;
        _historyDb = historyDb;
        _uptimeProvider = new UptimeProvider();
        _uptimeProvider.UptimeUpdate += OnUptimeUpdated;
    }

    #region State

    public async Task EnsureStateCreated()
    {
        var storedState = await _stateDb.GetStateAsync(_configurationManager.ApiKey);
        if (storedState is not null)
            return;
        await _stateDb.AddOrUpdateStateAsync(new ServiceState { ApiKey = _configurationManager.ApiKey });
    }

    public async Task<ServiceState> GetServiceStateAsync(string apiKey)
    {
        var currentState = await _stateDb.GetStateAsync(apiKey);
        return currentState ?? new ServiceState();
    }

    public async Task ClearServiceStateAsync(string apiKey)
    {
        var stateToClear = await _stateDb.GetStateAsync(apiKey);

        if (stateToClear is null)
            return;

        stateToClear.Errors = 0;
        stateToClear.Warnings = 0;
        stateToClear.ItemCanceled = 0;
        stateToClear.ItemsAnalyzed = 0;
        stateToClear.ItemsBought = 0;
        stateToClear.ItemsSold = 0;
        stateToClear.Uptime = TimeSpan.Zero;
        stateToClear.IsLoggedIn = LogInState.NotLoggedIn;
        stateToClear.WorkingState = ServiceWorkingState.Down;

        await _stateDb.AddOrUpdateStateAsync(stateToClear);
        await PublishState(stateToClear);
    }

    public async Task OnTradingStartedAsync()
    {
        var storedState = await _stateDb.GetStateAsync(_configurationManager.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.WorkingState = ServiceWorkingState.Up;
        await _stateDb.AddOrUpdateStateAsync(storedState);
        _uptimeProvider.StartCountdown();
        await PublishState(storedState);
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
        await PublishState(storedState);
        Log.Information("Worker has stopped");
    }

    public async Task OnLogInPendingAsync()
    {
        var storedState = await _stateDb.GetStateAsync(_configurationManager.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.IsLoggedIn = LogInState.Pending;
        await PublishState(storedState);
        await _stateDb.AddOrUpdateStateAsync(storedState);
    }

    public async Task OnLoggedInAsync()
    {
        var storedState = await _stateDb.GetStateAsync(_configurationManager.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.IsLoggedIn = LogInState.LoggedIn;
        storedState.ApiKey = _configurationManager.ApiKey;
        await PublishState(storedState);
        await _stateDb.AddOrUpdateStateAsync(storedState);
    }

    public async Task OnLoggedOutAsync()
    {
        var storedState = await _stateDb.GetStateAsync(_configurationManager.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.IsLoggedIn = LogInState.NotLoggedIn;
        await PublishState(storedState);
        await _stateDb.AddOrUpdateStateAsync(storedState);
    }

    #endregion

    #region History

    public async Task<List<TradingEvent>> GetServiceHistoryAsync(string apiKey)
    {
        return await _historyDb.GetHistoryAsync(apiKey);
    }

    public async Task OnErrorAsync(Exception exception)
    {
        var storedState = await _stateDb.GetStateAsync(_configurationManager.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.Errors++;
        await _stateDb.AddOrUpdateStateAsync(storedState);
        await PublishState(storedState);
        var tradingEvent = new TradingEvent
        {
            ApiKey = _configurationManager.ApiKey,
            Type = InfoType.Error,
            Time = DateTime.Now,
            Info = $"Message: {exception.Message}, StackTrace: {exception.StackTrace}"
        };
        await _historyDb.AddNewEventAsync(tradingEvent);
        await PublishEvent(tradingEvent);
    }

    public async Task OnItemAnalyzingAsync(ItemPage itemPage)
    {
        var storedState = await _stateDb.GetStateAsync(_configurationManager.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.ItemsAnalyzed++;
        await _stateDb.AddOrUpdateStateAsync(storedState);
        await PublishState(storedState);

        var tradingEvent = new TradingEvent
        {
            ApiKey = _configurationManager.ApiKey,
            Type = InfoType.ItemAnalyzed,
            CurrentBalance = itemPage.CurrentBalance,
            Time = DateTime.Now,
            Info = itemPage.EngItemName
        };
        await _historyDb.AddNewEventAsync(tradingEvent);
        await PublishEvent(tradingEvent);
    }

    public async Task OnItemSellingAsync(Order order)
    {
        var storedState = await _stateDb.GetStateAsync(_configurationManager.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.ItemsSold++;
        await _stateDb.AddOrUpdateStateAsync(storedState);
        await PublishState(storedState);
        var tradingEvent = new TradingEvent
        {
            ApiKey = _configurationManager.ApiKey,
            Type = InfoType.ItemSold,
            Time = DateTime.Now,
            Info = order.EngItemName,
            BuyPrice = order.BuyPrice,
            SellPrice = order.SellPrice,
            Profit = (1 - _configurationManager.SteamCommission) * order.SellPrice - order.BuyPrice
        };
        await _historyDb.AddNewEventAsync(tradingEvent);
        await PublishEvent(tradingEvent);
    }

    public async Task OnItemBuyingAsync(Order order)
    {
        var storedState = await _stateDb.GetStateAsync(_configurationManager.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.ItemsBought++;
        await _stateDb.AddOrUpdateStateAsync(storedState);
        await PublishState(storedState);
        var tradingEvent = new TradingEvent
        {
            ApiKey = _configurationManager.ApiKey,
            Type = InfoType.ItemBought,
            Time = DateTime.Now,
            Info = order.EngItemName,
            BuyPrice = order.BuyPrice,
            SellPrice = order.SellPrice
        };
        await _historyDb.AddNewEventAsync(tradingEvent);
        await PublishEvent(tradingEvent);
    }

    public async Task OnItemCancellingAsync(Order order)
    {
        var storedState = await _stateDb.GetStateAsync(_configurationManager.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.ItemCanceled++;
        await _stateDb.AddOrUpdateStateAsync(storedState);
        await PublishState(storedState);
        var tradingEvent = new TradingEvent
        {
            ApiKey = _configurationManager.ApiKey,
            Type = InfoType.ItemCanceled,
            Time = DateTime.Now,
            Info = order.EngItemName,
            BuyPrice = order.BuyPrice
        };
        await _historyDb.AddNewEventAsync(tradingEvent);
        await PublishEvent(tradingEvent);
    }

    #endregion

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

    private void OnUptimeUpdated(TimeSpan uptime)
    {
        var storedState = _stateDb.GetStateAsync(_configurationManager.ApiKey).Result
                          ?? throw new Exception("There is no state for this api key");
        storedState.Uptime = uptime;
        var _ = _stateDb.AddOrUpdateStateAsync(storedState);
        var __ = PublishState(storedState);
    }

    #endregion
}