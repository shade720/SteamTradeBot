using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SteamTradeBot.Backend.Models;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.StateModel;
using SteamTradeBot.Backend.Services;

namespace SteamTradeBot.Backend;

[Route("api")]
[ApiController]
public class TradeBotController : ControllerBase
{
    [HttpPost]
    [Route("activation")]
    public async Task StartBot(
        WorkerService worker,
        [FromServices] JsonFileConfigurationManager configurationManager, 
        ISteamApi api,
        IStateManager stateManager, 
        UserConfiguration userConfiguration)
    {
        configurationManager.SetConfigurationContextForUser(userConfiguration.Login, userConfiguration);
        stateManager.OnLogInPending();
        await api.LogIn(configurationManager.Login, configurationManager.Password, configurationManager.Secret);
        stateManager.OnLoggedIn(configurationManager.Login);
        await worker.Start();
    }

    [HttpPost]
    [Route("deactivation")]
    public async Task StopBot(
        WorkerService worker,
        ISteamApi api,
        IStateManager stateManager)
    {
        api.LogOut();
        stateManager.OnLoggedOut();
        await worker.Stop();
    }

    [HttpPost]
    [Route("userConfiguration")]
    public async Task SetConfiguration(
        IConfigurationManager configurationManager, 
        UserConfiguration userConfiguration)
    {
        await configurationManager.RefreshConfigurationAsync(userConfiguration);
    }

    [HttpPost]
    [Route("orderscanceling")]
    public async Task CancelOrders(
        OrderCancellingService cancellingService)
    {
        await cancellingService.ClearBuyOrdersAsync();
    }

    [HttpGet]
    [Route("state")]
    public async Task<ServiceState> GetState(
        IStateManager stateManager, [FromQuery] long fromTicks)
    {
        return await stateManager.GetServiceStateAsync(fromTicks);
    }

    [HttpGet]
    [Route("logs")]
    public async Task<string> GetLogs(
        LogsProviderService logsProvider, 
        Guid userGuid)
    {
        return await logsProvider.GetLogs(userGuid.ToString());
    }
}