using Microsoft.AspNetCore.Mvc;
using SteamTradeBot.Backend.Models;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.StateModel;
using SteamTradeBot.Backend.Services;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend;

[Route("api")]
[ApiController]
public class TradeBotController : ControllerBase
{
    public record Credentials(string Login, string Password, string Secret);

    [HttpPost]
    [Route("activation")]
    public async Task StartBot(
        WorkerService worker,
        ISteamApi api,
        IStateManager stateManager,
        Credentials credentials)
    {
        await (stateManager as DbBasedStateManagerService).EnsureCreated();
        await stateManager.OnLogInPendingAsync();
        await api.LogIn(credentials.Login, credentials.Password, credentials.Secret);
        await stateManager.OnLoggedInAsync();
        await worker.StartAsync();
    }

    [HttpPost]
    [Route("deactivation")]
    public async Task StopBot(
        WorkerService worker,
        ISteamApi api,
        IStateManager stateManager)
    {
        await worker.StopAsync();
        api.LogOut();
        await stateManager.OnLoggedOutAsync();
    }

    [HttpPost]
    [Route("refreshConfiguration")]
    public async Task SetConfiguration(
        IConfigurationManager configurationManager,
        UserConfiguration userConfiguration,
        [FromQuery] string apiKey)
    {
        (configurationManager as JsonFileBasedConfigurationManagerService).SetConfigurationContextForUser(apiKey);
        await configurationManager.RefreshConfigurationAsync(apiKey, userConfiguration);
    }

    [HttpPost]
    [Route("cancelOrders")]
    public async Task CancelOrders(
        OrderCancellingService cancellingService,
        [FromQuery] string apiKey)
    {
        await cancellingService.ClearBuyOrdersAsync();
    }

    [HttpGet]
    [Route("state")]
    public async Task<ServiceState> GetState(
        IStateManager stateManager,
        [FromQuery] long fromTicks,
        [FromQuery] string apiKey)
    {
        return await stateManager.GetServiceStateAsync(apiKey, fromTicks);
    }

    [HttpGet]
    [Route("logs")]
    public async Task<string> GetLogs(
        LogsProviderService logsProvider,
        [FromQuery] string apiKey)
    {
        return await logsProvider.GetLogs(apiKey);
    }
}