using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Serilog;
using Serilog.Events;
using SteamTradeBot.Backend.BusinessLogicLayer.Decorators;
using SteamTradeBot.Backend.BusinessLogicLayer.EventHandlers;
using SteamTradeBot.Backend.BusinessLogicLayer.Factories;
using SteamTradeBot.Backend.BusinessLogicLayer.Middlewares;
using SteamTradeBot.Backend.BusinessLogicLayer.Models;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.Rules;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules.CancelRules;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules.SellRules;
using SteamTradeBot.Backend.BusinessLogicLayer.Services;
using SteamTradeBot.Backend.BusinessLogicLayer.SteamConnectors.Selenium;
using SteamTradeBot.Backend.DataAccessLayer;
using System;
using System.IO;

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

builder.Services.AddTransient<OrdersRepository, SqlOrdersRepository>();
builder.Services.AddTransient<HistoryRepository, SqlHistoryRepository>();
builder.Services.AddTransient<StateRepository, SqlStateRepository>();
builder.Services.AddTransient<TokenRepository, SqlTokenRepository>();
builder.Services.AddDbContextFactory<TradeBotDataContext>(options =>
{
    var postgresConnectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");
    if (postgresConnectionString is not null)
    {
        options.UseNpgsql(postgresConnectionString);
    }
    else
    {
        var sqlServerConnectionString = builder.Configuration["MsSqlServerConnectionString"];
        options.UseSqlServer(sqlServerConnectionString);
    }
});

builder.Services.AddScoped<WorkerService>();

builder.Services.AddSingleton<ItemsNamesProvider>();
builder.Services.AddSingleton<ItemPageFactory>();
builder.Services.AddSingleton<SolutionsFactory>();

builder.Services.AddSingleton<MarketRules>();
builder.Services.AddSingleton<IBuyRule, AvailableBalanceRule>();
builder.Services.AddSingleton<IBuyRule, AveragePriceRule>();
builder.Services.AddSingleton<IBuyRule, SalesCountRule>();
builder.Services.AddSingleton<IBuyRule, OrderAlreadyExistRule>();
builder.Services.AddSingleton<IBuyRule, RequiredProfitRule>();
builder.Services.AddSingleton<IBuyRule, TrendRule>();
builder.Services.AddSingleton<ISellRule, CurrentQuantityCheckRule>();
builder.Services.AddSingleton<ICancelRule, FitPriceRule>();

builder.Services.AddSingleton<IEventHistoryAgent, DbEventHistoryAgent>();
builder.Services.AddSingleton<ITradingEventHandler, DbBasedTradingEventHandler>();
builder.Services.Decorate<ITradingEventHandler, TimerUpdateTradingEventHandler>();
builder.Services.Decorate<ITradingEventHandler, LogEventHandler>();
builder.Services.Decorate<ITradingEventHandler, SignalTradingEventHandler>();

builder.Services.AddSignalR();

builder.Services.AddSingleton<IConfigurationService, JsonFileBasedConfigurationService>();

var webDriverHostFromEnvironment = Environment.GetEnvironmentVariable("SELENIUM_HOST");
builder.Services.AddSingleton<ISteamApi, SeleniumSteamApi>();
builder.Services.AddSingleton<IItemsTableApi, SkinsTableApi>();
builder.Services.AddSingleton(_ => new SeleniumWebDriver(() =>
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
    if (webDriverHostFromEnvironment is not null)
        return new RemoteWebDriver(new Uri(webDriverHostFromEnvironment), chromeOptions.ToCapabilities());
    var driverService = ChromeDriverService.CreateDefaultService();
    driverService.EnableVerboseLogging = false;
    driverService.SuppressInitialDiagnosticInformation = true;
    return new ChromeDriver(driverService, chromeOptions);
}));

builder.Services.AddScoped<LogsProviderService>();
builder.Services.AddScoped<OrderCancellingService>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapHub<SignalTradingEventHandler>("/tradingEventHandler");
app.MapControllers();
app.UseMiddleware<TokenAuthenticationMiddleware>();
app.UseMiddleware<ExclusiveAccessMiddleware>();
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