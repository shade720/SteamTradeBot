using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SteamTradeBot.Backend;
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

var postgresConnectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING") ?? builder.Configuration["ConnectionString"];

builder.Services.AddDbContextFactory<MarketDataContext>(options => options.UseNpgsql(postgresConnectionString));
builder.Services.AddDbContextFactory<HistoryDataContext>(options => options.UseNpgsql(postgresConnectionString));

builder.Services.AddSingleton<TradeBot>();
builder.Services.AddSingleton<ServiceState>();

var app = builder.Build();

app.ConfigureApi();

app.Run();