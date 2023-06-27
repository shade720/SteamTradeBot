using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SteamTradeBot.Backend.BusinessLogicLayer;

namespace SteamTradeBot.Backend;

public static class API
{
    private record Credentials(string Login, string Password, string Token, string Secret);
    public static void ConfigureApi(this WebApplication app)
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            var obj = exceptionHandlerApp.ApplicationServices.GetService(typeof(ServiceState));
            if (obj is not null) ((ServiceState)obj).Errors++;
            exceptionHandlerApp.Run(async context => await Results.Problem().ExecuteAsync(context));
        });
        app.MapPost("api/login", LogIn);
        app.MapPost("api/logout", ([FromServices] TradeBot tradeBot) => tradeBot.LogOut());
        app.MapPost("api/activation", ([FromServices] TradeBot tradeBot) => tradeBot.StartTrading());
        app.MapPost("api/deactivation", ([FromServices] TradeBot tradeBot) => tradeBot.StopTrading());
        app.MapPost("api/configuration", async ([FromServices] TradeBot tradeBot, HttpContext context) =>
        {
            using var reader = new StreamReader(context.Request.Body);
            var configurationJson = await reader.ReadToEndAsync();
            tradeBot.SetConfiguration(configurationJson);
        });
        app.MapPost("api/itemslistrefreshing", ([FromServices] TradeBot tradeBot) => tradeBot.RefreshWorkingSet());
        app.MapPost("api/orderscanceling", ([FromServices] TradeBot tradeBot) => tradeBot.ClearBuyOrders());
        app.MapGet("api/state", ([FromServices] TradeBot tradeBot) => tradeBot.GetServiceState());
        app.MapGet("api/logs", ([FromServices] TradeBot tradeBot) => tradeBot.GetLogs());
    }

    private static async Task<IResult> LogIn(TradeBot tradeBot, Credentials credentials)
    {
        return await Task.Run(() => tradeBot.LogIn(credentials.Login, credentials.Password, credentials.Token, credentials.Secret))
            .ContinueWith(task => task.IsCompletedSuccessfully ? Results.Ok() : Results.Problem(task.Exception?.Message));
    }
}