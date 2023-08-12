using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SteamTradeBot.Backend;
using SteamTradeBot.Backend.BusinessLogicLayer;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules.CancelRules;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules.SellRules;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.BusinessLogicLayer.Factories;
using SteamTradeBot.Backend.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using SteamTradeBot.Backend.BusinessLogicLayer.Extensions;
using ConfigurationManager = SteamTradeBot.Backend.Services.ConfigurationManager;
using SteamTradeBot.Backend.Models.StateModel;
using SteamTradeBot.Backend.BusinessLogicLayer.Abstractions;

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

builder.Configuration.AddUsersConfigurations();

var postgresConnectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING") ?? builder.Configuration["PostgresConnectionString"];
var sqlServerConnectionString = builder.Configuration["SqlServerConnectionString"];

builder.Services.AddDbContextFactory<TradeBotDataContext>(options => options.UseSqlServer(sqlServerConnectionString));
//builder.Services.AddDbContextFactory<TradeBotDataContext>(options => options.UseNpgsql(postgresConnectionString));

var remoteWebDriverHost = Environment.GetEnvironmentVariable("SELENIUM_HOST") ?? "http://localhost:5051";
builder.Services.AddScoped(_ => new SteamAPI(() =>
{
    var chromeOptions = new ChromeOptions();
    chromeOptions.AddArgument("--disable-gpu");
    chromeOptions.AddArgument("--no-sandbox");
    chromeOptions.AddArgument("--disable-setuid-sandbox");
    chromeOptions.AddArgument("--disable-dev-shm-usage");
    chromeOptions.AddArgument("--start-maximized");
    chromeOptions.AddArgument("--window-size=1920,1080");
    chromeOptions.AddArgument("--headless");
    chromeOptions.AddArgument("--disable-logging");
    chromeOptions.AddArgument("--log-level=3");
    //return new RemoteWebDriver(new Uri(remoteWebDriverHost), chromeOptions.ToCapabilities());
    var driverService = ChromeDriverService.CreateDefaultService();
    driverService.EnableVerboseLogging = false;
    driverService.SuppressInitialDiagnosticInformation = true;
    return new ChromeDriver(driverService, chromeOptions);
}));

builder.Services.AddScoped<MarketDbAccess>();
builder.Services.AddScoped<HistoryDbAccess>();

builder.Services.AddScoped<ConfigurationManager>();
builder.Services.AddScoped<StateManagerService>();
builder.Services.AddTransient<LogsProviderService>();
builder.Services.AddTransient<OrderCancellingService>();

builder.Services.AddTransient<IBuyRule, AvailableBalanceRule>();
builder.Services.AddTransient<IBuyRule, AveragePriceRule>();
builder.Services.AddTransient<IBuyRule, SalesCountRule>();
builder.Services.AddTransient<IBuyRule, OrderAlreadyExistRule>();
builder.Services.AddTransient<IBuyRule, RequiredProfitRule>();
builder.Services.AddTransient<IBuyRule, TrendRule>();
builder.Services.AddTransient<ISellRule, CurrentQuantityCheckRule>();
builder.Services.AddTransient<ICancelRule, FitPriceRule>();

builder.Services.AddTransient<MarketRules>();
builder.Services.AddTransient<SolutionsFactory>();
builder.Services.AddTransient<ItemPageFactory>();
builder.Services.AddScoped<ItemsNamesProvider>();

builder.Services.AddScoped<WorkerService>();

builder.Services.AddControllers();

var app = builder.Build();

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

app.MapControllers();

app.Run();