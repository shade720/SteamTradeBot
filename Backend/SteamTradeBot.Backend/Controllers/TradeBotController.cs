using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SteamTradeBot.Backend.Application.Services;
using SteamTradeBot.Backend.Domain;
using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Domain.StateAggregate;
using SteamTradeBot.Backend.Domain.TradingEventAggregate;

namespace SteamTradeBot.Backend.UI.Controllers;

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
        IEventHistoryAgent eventHistoryAgent,
        ITradingEventHandler tradingEventHandler,
        Credentials credentials)
    {
        await (eventHistoryAgent as DbEventHistoryAgent).EnsureStateCreated();

        await tradingEventHandler.OnLogInPendingAsync();
        var isAuthenticated = await api.LogIn(credentials.Login, credentials.Password, credentials.Secret);
        if (!isAuthenticated)
        {
            await tradingEventHandler.OnLoggedOutAsync();
            return Results.BadRequest("Authorization failed.");
        }
        await tradingEventHandler.OnLoggedInAsync();
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
        IEventHistoryAgent eventHistoryAgent,
        [FromQuery] string apiKey)
    {
        return await eventHistoryAgent.GetHistorySummaryAsync(apiKey);
    }

    [HttpGet]
    [Route("history")]
    public async Task<List<TradingEvent>> GetHistory(
        IEventHistoryAgent eventHistoryAgent,
        [FromQuery] string apiKey)
    {
        return await eventHistoryAgent.GetHistoryAsync(apiKey);
    }

    [HttpPost]
    [Route("clearState")]
    public async Task CancelOrders(
        IEventHistoryAgent eventHistoryAgent,
        [FromQuery] string apiKey)
    {
        await eventHistoryAgent.ClearHistorySummaryAsync(apiKey);
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