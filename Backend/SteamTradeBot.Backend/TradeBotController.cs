using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SteamTradeBot.Backend.BusinessLogicLayer;
using SteamTradeBot.Backend.Models;
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
        ConfigurationManager configurationManager, 
        SteamAPI api, 
        StateManagerService stateManager, 
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
        SteamAPI api, 
        StateManagerService stateManager)
    {
        api.LogOut();
        stateManager.OnLoggedOut();
        await worker.Stop();
    }

    [HttpPost]
    [Route("userConfiguration")]
    public async Task SetConfiguration(
        ConfigurationManager configurationManager, 
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
        StateManagerService stateManager, [FromQuery] long fromTicks)
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