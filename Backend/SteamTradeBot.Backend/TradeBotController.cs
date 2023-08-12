using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SteamTradeBot.Backend.BusinessLogicLayer;
using SteamTradeBot.Backend.Models;
using SteamTradeBot.Backend.Models.Configuration;
using SteamTradeBot.Backend.Services;

namespace SteamTradeBot.Backend;

[Route("api")]
[ApiController]
public class TradeBotController : ControllerBase
{
    [HttpPost]
    [Route("activation")]
    public async Task StartBot(WorkerService worker, ConfigurationManager configurationManager, SteamAPI api, Configuration configuration)
    {
        configurationManager.SetForUser(configuration.Login, configuration);
        api.LogIn(configuration.Login, configuration.Password, configuration.Token, configuration.Secret);
        await worker.Start();
    }

    [HttpPost]
    [Route("deactivation")]
    public async Task StopBot(WorkerService worker, SteamAPI api)
    {
        api.LogOut();
        await worker.Stop();
    }

    [HttpPost]
    [Route("configuration")]
    public async Task SetConfiguration(ConfigurationManager configurationManager, [FromBody] Configuration configuration)
    {
        configurationManager.RefreshConfiguration(configuration);
    }

    [HttpPost]
    [Route("orderscanceling")]
    public async Task CancelOrders(OrderCancellingService cancellingService)
    {
        cancellingService.ClearBuyOrders();
    }

    [HttpGet]
    [Route("state")]
    public async Task<ServiceState> GetState(StateManagerService stateManager, [FromQuery] long fromTicks)
    {
        return stateManager.GetServiceState(fromTicks);
    }

    [HttpGet]
    [Route("logs")]
    public async Task<string> GetLogs(LogsProviderService logsProvider, Guid userGuid)
    {
        return await logsProvider.GetLogs(userGuid.ToString());
    }
}