using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        api.LogIn(configurationManager.Login, configurationManager.Password, configurationManager.Secret);
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
        configurationManager.RefreshConfiguration(userConfiguration);
    }

    [HttpPost]
    [Route("orderscanceling")]
    public async Task CancelOrders(
        OrderCancellingService cancellingService)
    {
        cancellingService.ClearBuyOrders();
    }

    [HttpGet]
    [Route("state")]
    public async Task<ServiceState> GetState(
        StateManagerService stateManager, [FromQuery] long fromTicks)
    {
        return stateManager.GetServiceState(fromTicks);
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