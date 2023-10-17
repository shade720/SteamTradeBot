using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using SteamTradeBot.Backend.UI.Middlewares;
using System;
using System.IO;
using SteamTradeBot.Backend.Application;
using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Infrastructure;

var logFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

if (!Directory.Exists(logFolderPath))
    Directory.CreateDirectory(logFolderPath);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.RollingFile(
        logFolderPath + @"/log-{Date}.txt",
        LogEventLevel.Information,
        "`~{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message:lj}{NewLine}{Exception}",
        retainedFileCountLimit: 3)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);

builder.Configuration
    .SetBasePath(Environment.CurrentDirectory)
    .AddJsonFile("appsettings.json", true, true);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddTradeBotApplication(builder.Configuration, Path.Combine(Environment.CurrentDirectory, "appsettings.json"));

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.UseMiddleware<TokenAuthenticationMiddleware>();
app.UseMiddleware<ExclusiveAccessMiddleware>();

app.ConfigureTradeBotApplication();

app.UseExceptionHandler(async exceptionHandlerApp =>  
{
    var exception = exceptionHandlerApp.ServerFeatures.Get<IExceptionHandlerFeature>()?.Error;
    if (exception is null)
        return;
    var stateManager = exceptionHandlerApp.ApplicationServices.GetService<ITradingEventHandler>();
    if (stateManager is null)
    {
        Log.Error("Application error: tradingEventHandler does not configured (ExceptionHandler)");
        return;
    }
    await stateManager.OnErrorAsync(exception);
    exceptionHandlerApp.Run(async context => await Results.Problem().ExecuteAsync(context));
});

app.Run();