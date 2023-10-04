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
        IEventService eventService,
        Credentials credentials)
    {
        if (eventService is not EventService dbBasedStateManager)
            throw new ApplicationException("Application error: eventService does not configured");
        await dbBasedStateManager.EnsureStateCreated();

        await eventService.OnLogInPendingAsync();
        var isAuthenticated = await api.LogIn(credentials.Login, credentials.Password, credentials.Secret);
        if (!isAuthenticated)
        {
            await eventService.OnLoggedOutAsync();
            return Results.BadRequest("Authorization failed.");
        }
        await eventService.OnLoggedInAsync();
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
        IEventService eventService,
        [FromQuery] string apiKey)
    {
        return await eventService.GetHistorySummaryAsync(apiKey);
    }

    [HttpGet]
    [Route("history")]
    public async Task<List<TradingEvent>> GetHistory(
        IEventService eventService,
        [FromQuery] string apiKey)
    {
        return await eventService.GetHistoryAsync(apiKey);
    }

    [HttpPost]
    [Route("clearState")]
    public async Task CancelOrders(
        IEventService eventService,
        [FromQuery] string apiKey)
    {
        await eventService.ClearHistorySummaryAsync(apiKey);
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