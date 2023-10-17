using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.ItemModel;
using SteamTradeBot.Backend.Domain.StateModel;

namespace SteamTradeBot.Backend.Application.EventHandlers;

internal sealed class DbBasedTradingEventHandler : ITradingEventHandler
{
    private readonly IConfigurationService _configurationService;
    private readonly StateRepository _stateRepository;
    private readonly HistoryRepository _historyRepository;

    public DbBasedTradingEventHandler(
        IConfigurationService configurationService,
        StateRepository stateRepository,
        HistoryRepository historyRepository)
    {
        _configurationService = configurationService;
        _stateRepository = stateRepository;
        _historyRepository = historyRepository;
    }

    public async Task OnTradingStartedAsync()
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.WorkingState = ServiceWorkingState.Up;
        await _stateRepository.AddOrUpdateStateAsync(storedState);
    }

    public async Task OnTradingStoppedAsync()
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.WorkingState = ServiceWorkingState.Down;
        storedState.Uptime = TimeSpan.Zero;
        await _stateRepository.AddOrUpdateStateAsync(storedState);
    }

    public async Task OnLogInPendingAsync()
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.IsLoggedIn = LogInState.Pending;
        await _stateRepository.AddOrUpdateStateAsync(storedState);
    }

    public async Task OnLoggedInAsync()
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.IsLoggedIn = LogInState.LoggedIn;
        storedState.ApiKey = _configurationService.ApiKey;
        await _stateRepository.AddOrUpdateStateAsync(storedState);
    }

    public async Task OnLoggedOutAsync()
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.IsLoggedIn = LogInState.NotLoggedIn;
        await _stateRepository.AddOrUpdateStateAsync(storedState);
    }

    public async Task OnErrorAsync(Exception exception)
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.Errors++;
        await _stateRepository.AddOrUpdateStateAsync(storedState);
        var tradingEvent = new TradingEvent
        {
            ApiKey = _configurationService.ApiKey,
            Type = InfoType.Error,
            Time = DateTime.UtcNow,
            Info = $"Message: {exception.Message}, StackTrace: {exception.StackTrace}"
        };
        await _historyRepository.AddNewEventAsync(tradingEvent);
    }

    public async Task OnItemAnalyzingAsync(ItemPage itemPage)
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.ItemsAnalyzed++;
        await _stateRepository.AddOrUpdateStateAsync(storedState);
        var tradingEvent = new TradingEvent
        {
            ApiKey = _configurationService.ApiKey,
            Type = InfoType.ItemAnalyzed,
            CurrentBalance = itemPage.CurrentBalance,
            Time = DateTime.UtcNow,
            Info = itemPage.EngItemName
        };
        await _historyRepository.AddNewEventAsync(tradingEvent);
    }

    public async Task OnItemSellingAsync(Order order)
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.ItemsSold++;
        await _stateRepository.AddOrUpdateStateAsync(storedState);
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
    }

    public async Task OnItemBuyingAsync(Order order)
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.ItemsBought++;
        await _stateRepository.AddOrUpdateStateAsync(storedState);
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
    }

    public async Task OnItemCancellingAsync(Order order)
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey)
                          ?? throw new Exception("There is no state for this api key");
        storedState.ItemCanceled++;
        await _stateRepository.AddOrUpdateStateAsync(storedState);
        var tradingEvent = new TradingEvent
        {
            ApiKey = _configurationService.ApiKey,
            Type = InfoType.ItemCanceled,
            Time = DateTime.UtcNow,
            Info = order.EngItemName,
            BuyPrice = order.BuyPrice
        };
        await _historyRepository.AddNewEventAsync(tradingEvent);
    }
}