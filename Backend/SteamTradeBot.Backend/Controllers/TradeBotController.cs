using System;
using Microsoft.AspNetCore.Mvc;
using SteamTradeBot.Backend.Models;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.StateModel;
using SteamTradeBot.Backend.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SteamTradeBot.Backend.Controllers;

[Route("api")]
[ApiController]
public class TradeBotController : ControllerBase
{
    public record Credentials(string Login, string Password, string Secret);

    [HttpPost]
    [Route("activate")]
    public async Task<IResult> StartBot(
        WorkerService worker,
        ISteamApi api,
        IStateManager stateManager,
        IConfigurationManager configurationManager,
        Credentials credentials,
        [FromQuery] string apiKey)
    {
        if (configurationManager is not JsonFileBasedConfigurationManagerService jsonFileBasedConfigurationManager)
            throw new ApplicationException("Application error: configurationManager does not configured");
        jsonFileBasedConfigurationManager.SetConfigurationContextForUser(apiKey);

        if (stateManager is not DbBasedStateManagerService dbBasedStateManager)
            throw new ApplicationException("Application error: stateManager does not configured");
        await dbBasedStateManager.EnsureStateCreated();

        await stateManager.OnLogInPendingAsync();
        var isAuthenticated = await api.LogIn(credentials.Login, credentials.Password, credentials.Secret);
        if (!isAuthenticated)
        {
            await stateManager.OnLoggedOutAsync();
            return Results.BadRequest("Authorization failed. User data are incorrect.");
        }
        await stateManager.OnLoggedInAsync();
        await worker.StartAsync();

        return Results.Ok();
    }

    [HttpPost]
    [Route("deactivate")]
    public async Task StopBot(
        WorkerService worker)
    {
        await worker.StopAsync();
    }

    [HttpPost]
    [Route("refreshConfiguration")]
    public async Task SetConfiguration(
        IConfigurationManager configurationManager,
        UserConfiguration userConfiguration,
        [FromQuery] string apiKey)
    {
        if (configurationManager is not JsonFileBasedConfigurationManagerService jsonFileBasedConfigurationManager)
            throw new ApplicationException("Application error: configurationManager not configured");
        jsonFileBasedConfigurationManager.SetConfigurationContextForUser(apiKey);
        await configurationManager.RefreshConfigurationAsync(apiKey, userConfiguration);
    }

    [HttpPost]
    [Route("cancelOrders")]
    public async Task CancelOrders(
        OrderCancellingService cancellingService,
        [FromQuery] string apiKey)
    {
        await cancellingService.ClearBuyOrdersAsync(apiKey);
    }

    [HttpGet]
    [Route("state")]
    public async Task<ServiceState> GetState(
        IStateManager stateManager,
        [FromQuery] string apiKey)
    {
        return await stateManager.GetServiceStateAsync(apiKey);
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