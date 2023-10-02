using Microsoft.AspNetCore.SignalR;
using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Factories;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.ItemModel;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.StateModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Services;

public sealed class StateService : Hub, IStateService
{
    private readonly IConfigurationService _configurationService;
    private readonly IHubContext<StateService> _context;
    private readonly StateRepository _stateRepository;
    private readonly HistoryRepository _historyRepository;
    private readonly UptimeProvider _uptimeProvider;

    #region PublicInterface

    public StateService(
        IConfigurationService configurationService,
        IHubContext<StateService> context,
        StateRepository stateRepository,
        HistoryRepository historyRepository)
    {
        _configurationService = configurationService;
        _context = context;
        _stateRepository = stateRepository;
        _historyRepository = historyRepository;
        _uptimeProvider = new UptimeProvider();
        _uptimeProvider.UptimeUpdate += OnUptimeUpdated;
    }

    #region State

    public async Task EnsureStateCreated()
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey);
        if (storedState is not null)
            return;
        await _stateRepository.AddOrUpdateStateAsync(new ServiceState { ApiKey = _configurationService.ApiKey });
    }

    public async Task<ServiceState> GetServiceStateAsync(string apiKey)
    {
        var currentState = await _stateRepository.GetStateAsync(apiKey);
        return currentState ?? new ServiceState();
    }

    public async Task ClearServiceStateAsync(string apiKey)
    {
        var stateToClear = await _stateRepository.GetStateAsync(apiKey);

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

        await _stateRepository.AddOrUpdateStateAsync(stateToClear);
        await PublishState(stateToClear);
    }

    public async Task OnTradingStartedAsync()
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.WorkingState = ServiceWorkingState.Up;
        await _stateRepository.AddOrUpdateStateAsync(storedState);
        _uptimeProvider.StartCountdown();
        await PublishState(storedState);
        Log.Information("Worker has started");
    }

    public async Task OnTradingStoppedAsync()
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.WorkingState = ServiceWorkingState.Down;
        storedState.Uptime = TimeSpan.Zero;
        await _stateRepository.AddOrUpdateStateAsync(storedState);
        _uptimeProvider.StopCountdown();
        await PublishState(storedState);
        Log.Information("Worker has stopped");
    }

    public async Task OnLogInPendingAsync()
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.IsLoggedIn = LogInState.Pending;
        await PublishState(storedState);
        await _stateRepository.AddOrUpdateStateAsync(storedState);
    }

    public async Task OnLoggedInAsync()
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.IsLoggedIn = LogInState.LoggedIn;
        storedState.ApiKey = _configurationService.ApiKey;
        await PublishState(storedState);
        await _stateRepository.AddOrUpdateStateAsync(storedState);
    }

    public async Task OnLoggedOutAsync()
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.IsLoggedIn = LogInState.NotLoggedIn;
        await PublishState(storedState);
        await _stateRepository.AddOrUpdateStateAsync(storedState);
    }

    #endregion

    #region History

    public async Task<List<TradingEvent>> GetServiceHistoryAsync(string apiKey)
    {
        return await _historyRepository.GetHistoryAsync(apiKey);
    }

    public async Task OnErrorAsync(Exception exception)
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.Errors++;
        await _stateRepository.AddOrUpdateStateAsync(storedState);
        await PublishState(storedState);
        var tradingEvent = new TradingEvent
        {
            ApiKey = _configurationService.ApiKey,
            Type = InfoType.Error,
            Time = DateTime.UtcNow,
            Info = $"Message: {exception.Message}, StackTrace: {exception.StackTrace}"
        };
        await _historyRepository.AddNewEventAsync(tradingEvent);
        await PublishEvent(tradingEvent);
    }

    public async Task OnItemAnalyzingAsync(ItemPage itemPage)
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.ItemsAnalyzed++;
        await _stateRepository.AddOrUpdateStateAsync(storedState);
        await PublishState(storedState);

        var tradingEvent = new TradingEvent
        {
            ApiKey = _configurationService.ApiKey,
            Type = InfoType.ItemAnalyzed,
            CurrentBalance = itemPage.CurrentBalance,
            Time = DateTime.UtcNow,
            Info = itemPage.EngItemName
        };
        await _historyRepository.AddNewEventAsync(tradingEvent);
        await PublishEvent(tradingEvent);
    }

    public async Task OnItemSellingAsync(Order order)
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.ItemsSold++;
        await _stateRepository.AddOrUpdateStateAsync(storedState);
        await PublishState(storedState);
        var tradingEvent = new TradingEvent
        {
            ApiKey = _configurationService.ApiKey,
            Type = InfoType.ItemSold,
            Time = DateTime.UtcNow,
            Info = order.EngItemName,
            BuyPrice = order.BuyPrice,
            SellPrice = order.SellPrice,
            Profit = (1 - _configurationService.SteamCommission) * order.SellPrice - order.BuyPrice
        };
        await _historyRepository.AddNewEventAsync(tradingEvent);
        await PublishEvent(tradingEvent);
    }

    public async Task OnItemBuyingAsync(Order order)
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.ItemsBought++;
        await _stateRepository.AddOrUpdateStateAsync(storedState);
        await PublishState(storedState);
        var tradingEvent = new TradingEvent
        {
            ApiKey = _configurationService.ApiKey,
            Type = InfoType.ItemBought,
            Time = DateTime.UtcNow,
            Info = order.EngItemName,
            BuyPrice = order.BuyPrice,
            SellPrice = order.SellPrice
        };
        await _historyRepository.AddNewEventAsync(tradingEvent);
        await PublishEvent(tradingEvent);
    }

    public async Task OnItemCancellingAsync(Order order)
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.ItemCanceled++;
        await _stateRepository.AddOrUpdateStateAsync(storedState);
        await PublishState(storedState);
        var tradingEvent = new TradingEvent
        {
            ApiKey = _configurationService.ApiKey,
            Type = InfoType.ItemCanceled,
            Time = DateTime.UtcNow,
            Info = order.EngItemName,
            BuyPrice = order.BuyPrice
        };
        await _historyRepository.AddNewEventAsync(tradingEvent);
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

    private async Task OnUptimeUpdated(TimeSpan uptime)
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.Uptime = uptime;
        await _stateRepository.AddOrUpdateStateAsync(storedState);
        await PublishState(storedState);
    }

    #endregion
}