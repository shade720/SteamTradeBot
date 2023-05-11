using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using SteamTradeBot.Backend.BusinessLogicLayer;
using SteamTradeBot.Backend.BusinessLogicLayer.DataAccessLayer;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.RollingFile(
        @".\Logs\log{Date}.txt",
        LogEventLevel.Information,
        outputTemplate: "`~{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message:lj}{NewLine}{Exception}",
        retainedFileCountLimit: 3)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddSerilog(Log.Logger);
});

builder.Configuration.SetBasePath(Environment.CurrentDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.UserDomainName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddDbContextFactory<MarketDataContext>(options => options.UseNpgsql(builder.Configuration["ConnectionString"]));

builder.Services.AddSingleton<TradeBot>();

var app = builder.Build();

#region MinimalAPI

app.MapPost("api/login", ([FromServices] TradeBot tradeBot, [FromBody] Credentials credentials) => tradeBot.LogIn(credentials.Login, credentials.Password, credentials.Token));
app.MapPost("api/logout", ([FromServices] TradeBot tradeBot) => tradeBot.LogOut());
app.MapPost("api/activation", ([FromServices] TradeBot tradeBot) => tradeBot.StartTrading());
app.MapPost("api/deactivation", ([FromServices] TradeBot tradeBot) => tradeBot.StopTrading());
app.MapPost("api/configuration", ([FromServices] TradeBot tradeBot, [FromBody] string configurationJson) => tradeBot.SetConfiguration(configurationJson));
app.MapPost("api/itemlist", ([FromServices] TradeBot tradeBot) => tradeBot.RefreshWorkingSet());

#endregion

app.Run();

internal record Credentials(string Login, string Password, string Token);