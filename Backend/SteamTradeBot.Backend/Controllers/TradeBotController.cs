using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SteamTradeBot.Backend.Models;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.StateModel;
using SteamTradeBot.Backend.Services;
using System;
using System.Threading.Tasks;

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
        Credentials credentials)
    {
        if (stateManager is not DbBasedStateManagerService dbBasedStateManager)
            throw new ApplicationException("Application error: stateManager does not configured");
        await dbBasedStateManager.EnsureStateCreated();

        await stateManager.OnLogInPendingAsync();
        var isAuthenticated = await api.LogIn(credentials.Login, credentials.Password, credentials.Secret);
        if (!isAuthenticated)
        {
            await stateManager.OnLoggedOutAsync();
            return Results.BadRequest("Authorization failed.");
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
    public async Task<IResult> SetConfiguration(
        IConfigurationManager configurationManager,
        UserConfiguration userConfiguration,
        [FromQuery] string apiKey)
    {
        var isSuccessful = await configurationManager.RefreshConfigurationAsync(apiKey, userConfiguration);
        return !isSuccessful ? Results.BadRequest("Settings have not been updated. UserConfiguration was corrupted.") : Results.Ok();
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