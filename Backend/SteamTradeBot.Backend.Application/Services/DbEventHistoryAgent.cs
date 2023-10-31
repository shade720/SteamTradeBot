using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.StateAggregate;
using SteamTradeBot.Backend.Domain.TradingEventAggregate;

namespace SteamTradeBot.Backend.Application.Services;

public class DbEventHistoryAgent : IEventHistoryAgent
{
    private readonly IStateRepository _stateRepository;
    private readonly IHistoryRepository _historyRepository;
    private readonly IConfigurationService _configurationService;

    public DbEventHistoryAgent(
        IStateRepository stateRepository,
        IHistoryRepository historyRepository, 
        IConfigurationService configurationService)
    {
        _stateRepository = stateRepository;
        _historyRepository = historyRepository;
        _configurationService = configurationService;
    }

    public async Task EnsureStateCreated()
    {
        var storedState = await _stateRepository.GetStateAsync(_configurationService.ApiKey);
        if (storedState is not null)
            return;
        await _stateRepository.AddOrUpdateStateAsync(new ServiceState { ApiKey = _configurationService.ApiKey });
    }

    public async Task<ServiceState> GetHistorySummaryAsync(string apiKey)
    {
        var currentState = await _stateRepository.GetStateAsync(apiKey);
        return currentState ?? new ServiceState();
    }

    public async Task ClearHistorySummaryAsync(string apiKey)
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
    }

    public async Task<List<TradingEvent>> GetHistoryAsync(string apiKey)
    {
        return await _historyRepository.GetHistoryAsync(apiKey);
    }

    public async Task ClearHistoryAsync(string apiKey)
    {
        await _historyRepository.ClearHistoryAsync(apiKey);
    }

}