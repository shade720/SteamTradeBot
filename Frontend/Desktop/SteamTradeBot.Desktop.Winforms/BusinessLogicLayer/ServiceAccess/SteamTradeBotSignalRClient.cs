using Microsoft.AspNetCore.SignalR.Client;
using SteamTradeBot.Desktop.Winforms.Models;

namespace SteamTradeBot.Desktop.Winforms.BusinessLogicLayer.ServiceAccess;

public class SteamTradeBotSignalRClient
{
    private const string DefaultHost = "http://localhost:5050";
    private const string SignalREndpoint = "stateManager";
    private const int DelayBetweenAttemptsMs = 1000;

    private readonly HubConnection _hub;

    public delegate void OnStateRefresh(StateInfo stateInfo);
    public event OnStateRefresh OnStateRefreshEvent;

    public delegate void OnHistoryRefresh(TradingEvent tradingEvent);
    public event OnHistoryRefresh OnHistoryRefreshEvent;

    public SteamTradeBotSignalRClient(ApiKeyProvider apiKeyProvider)
    {
        var configuration = Program.LoadConfiguration();
        var host = configuration is null 
            ? DefaultHost 
            : configuration.ServerAddress;
        var hostWithEndpoint = string.Join('/', host, SignalREndpoint);

        var hostWithToken = $"{hostWithEndpoint}?apiKey={apiKeyProvider.GetApiKey()}";

        _hub = new HubConnectionBuilder()
            .WithUrl(hostWithToken)
            .WithAutomaticReconnect()
            .Build();
        _hub.Closed += HubOnClosed;
        _hub.On<StateInfo>("getState", state => OnStateRefreshEvent?.Invoke(state));
        _hub.On<TradingEvent>("getEvents", tradingEvent =>OnHistoryRefreshEvent?.Invoke(tradingEvent));
    }

    private async Task HubOnClosed(Exception? arg)
    {
        await _hub.StartAsync();
    }

    public async Task Connect()
    {
        while (true)
        {
            try
            {
                await _hub.StartAsync();
                return;
            }
            catch
            {
                OnStateRefreshEvent?.Invoke(new StateInfo());
                Thread.Sleep(DelayBetweenAttemptsMs);
            }
        }
    }

    public async Task Disconnect()
    {
        await _hub.StopAsync();
    }
}