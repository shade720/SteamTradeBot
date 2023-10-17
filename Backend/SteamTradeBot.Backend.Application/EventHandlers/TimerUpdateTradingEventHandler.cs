using SteamTradeBot.Backend.Application.Factories;
using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.ItemModel;

namespace SteamTradeBot.Backend.Application.EventHandlers;

internal class TimerUpdateTradingEventHandler : ITradingEventHandler
{
    private readonly CurrentUptimeProvider _currentUptimeProvider;
    private readonly IConfigurationService _configurationService;
    private readonly StateRepository _stateRepository;
    private readonly ITradingEventHandler _innerEventHandler;

    public TimerUpdateTradingEventHandler(
        IConfigurationService configurationService,
        StateRepository stateRepository, 
        ITradingEventHandler innerEventHandler)
    {
        _configurationService = configurationService;
        _stateRepository = stateRepository;
        _innerEventHandler = innerEventHandler;
        _currentUptimeProvider = new CurrentUptimeProvider();
        _currentUptimeProvider.UptimeUpdate += OnCurrentUptimeUpdated;
    }

    #region Decorated

    public async Task OnTradingStartedAsync()
    {
        _currentUptimeProvider.StartCountdown();
        await _innerEventHandler.OnTradingStartedAsync();
    }

    public async Task OnTradingStoppedAsync()
    {
        _currentUptimeProvider.StopCountdown();
        await _innerEventHandler.OnTradingStoppedAsync();
    }

    #endregion

    #region Forwarded

    public async Task OnLogInPendingAsync()
    {
        await _innerEventHandler.OnLogInPendingAsync();
    }

    public async Task OnLoggedInAsync()
    {
        await _innerEventHandler.OnLoggedInAsync();
    }

    public async Task OnLoggedOutAsync()
    {
        await _innerEventHandler.OnLoggedOutAsync();
    }

    public async Task OnErrorAsync(Exception exception)
    {
        await _innerEventHandler.OnErrorAsync(exception);
    }

    public async Task OnItemAnalyzingAsync(ItemPage itemPage)
    {
        await _innerEventHandler.OnItemAnalyzingAsync(itemPage);
    }

    public async Task OnItemSellingAsync(Order order)
    {
        await _innerEventHandler.OnItemSellingAsync(order);
    }

    public async Task OnItemBuyingAsync(Order order)
    {
        await _innerEventHandler.OnItemBuyingAsync(order);
    }

    public async Task OnItemCancellingAsync(Order order)
    {
        await _innerEventHandler.OnItemCancellingAsync(order);
    }

    #endregion

    private async Task OnCurrentUptimeUpdated(TimeSpan uptime)
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.Uptime = uptime;
        await _stateRepository.AddOrUpdateStateAsync(storedState);
    }
}