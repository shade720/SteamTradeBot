using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SteamTradeBot.Backend.BusinessLogicLayer.Models;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.StateModel;
using SteamTradeBot.Backend.BusinessLogicLayer.Services;
using System;
using System.Collections.Generic;
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
        IStateService stateService,
        Credentials credentials)
    {
        if (stateService is not StateService dbBasedStateManager)
            throw new ApplicationException("Application error: stateService does not configured");
        await dbBasedStateManager.EnsureStateCreated();

        await stateService.OnLogInPendingAsync();
        var isAuthenticated = await api.LogIn(credentials.Login, credentials.Password, credentials.Secret);
        if (!isAuthenticated)
        {
            await stateService.OnLoggedOutAsync();
            return Results.BadRequest("Authorization failed.");
        }
        await stateService.OnLoggedInAsync();
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
        IConfigurationService configurationService,
        UserConfiguration userConfiguration,
        [FromQuery] string apiKey)
    {
        var isSuccessful = await configurationService.RefreshConfigurationAsync(apiKey, userConfiguration);
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
        IStateService stateService,
        [FromQuery] string apiKey)
    {
        return await stateService.GetServiceStateAsync(apiKey);
    }

    [HttpGet]
    [Route("history")]
    public async Task<List<TradingEvent>> GetHistory(
        IStateService stateService,
        [FromQuery] string apiKey)
    {
        return await stateService.GetServiceHistoryAsync(apiKey);
    }

    [HttpPost]
    [Route("clearState")]
    public async Task CancelOrders(
        IStateService cancellingService,
        [FromQuery] string apiKey)
    {
        await cancellingService.ClearServiceStateAsync(apiKey);
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