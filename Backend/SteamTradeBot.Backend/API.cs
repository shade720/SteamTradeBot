using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SteamTradeBot.Backend.BusinessLogicLayer;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend;

public static class API
{
    private record Credentials(string Login, string Password, string Token, string Secret);
    public static void ConfigureApi(this WebApplication app)
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            var exception = exceptionHandlerApp.ServerFeatures.Get<IExceptionHandlerFeature>()?.Error;
            if (exception is null)
                return;
            var obj = exceptionHandlerApp.ApplicationServices.GetService(typeof(ServiceState));
            if (obj is not null)
            {
                ((ServiceState)obj).Errors++;
            }
            exceptionHandlerApp.Run(async context => await Results.Problem().ExecuteAsync(context));
        });
        app.MapPost("api/login", async (TradeBot tradeBot, Credentials credentials) => await LogIn(tradeBot, credentials));
        app.MapPost("api/logout", async (TradeBot tradeBot) => await LogOut(tradeBot));
        app.MapPost("api/activation", async (TradeBot tradeBot) => await StartTrading(tradeBot));
        app.MapPost("api/deactivation", async (TradeBot tradeBot) => await StopTrading(tradeBot));
        app.MapPost("api/configuration", async (TradeBot tradeBot, HttpContext context) =>
        {
            using var reader = new StreamReader(context.Request.Body);
            var configurationJson = await reader.ReadToEndAsync();
            tradeBot.SetConfiguration(configurationJson);
        });
        app.MapPost("api/orderscanceling", async (TradeBot tradeBot) => await ClearBuyOrders(tradeBot));
        app.MapGet("api/state", async (StateManager stateManager, [FromQuery] long fromTicks) => await GetServiceState(stateManager, fromTicks));
        app.MapGet("api/logs", async (TradeBot tradeBot) => await GetLogs(tradeBot));
    }

    private static async Task<IResult> LogIn(TradeBot tradeBot, Credentials credentials)
    {
        return await Task.Run(() => tradeBot.LogIn(credentials.Login, credentials.Password, credentials.Token, credentials.Secret))
            .ContinueWith(task => task.IsCompletedSuccessfully ? Results.Ok() : Results.Problem(task.Exception?.Message));
    }

    private static async Task<IResult> LogOut(TradeBot tradeBot)
    {
        return await Task.Run(tradeBot.LogOut)
            .ContinueWith(task => task.IsCompletedSuccessfully ? Results.Ok() : Results.Problem(task.Exception?.Message));
    }

    private static async Task<IResult> StartTrading(TradeBot tradeBot)
    {
        return await Task.Run(tradeBot.StartTrading)
            .ContinueWith(task => task.IsCompletedSuccessfully ? Results.Ok() : Results.Problem(task.Exception?.Message));
    }

    private static async Task<IResult> StopTrading(TradeBot tradeBot)
    {
        return await Task.Run(tradeBot.StopTrading)
            .ContinueWith(task => task.IsCompletedSuccessfully ? Results.Ok() : Results.Problem(task.Exception?.Message));
    }

    private static async Task<IResult> ClearBuyOrders(TradeBot tradeBot)
    {
        return await Task.Run(tradeBot.ClearBuyOrders)
            .ContinueWith(task => task.IsCompletedSuccessfully ? Results.Ok() : Results.Problem(task.Exception?.Message));
    }
    private static async Task<IResult> GetServiceState(StateManager stateManager, long fromTicks)
    {
        return await Task.Run(() => stateManager.GetServiceState(fromTicks))
            .ContinueWith(task => task.IsCompletedSuccessfully ? Results.Ok(task.Result) : Results.Problem(task.Exception?.Message));
    }
    private static async Task<IResult> GetLogs(TradeBot tradeBot)
    {
        return await Task.Run(tradeBot.GetLogs)
            .ContinueWith(task => task.IsCompletedSuccessfully ? Results.Ok(task.Result) : Results.Problem(task.Exception?.Message));
    }
}