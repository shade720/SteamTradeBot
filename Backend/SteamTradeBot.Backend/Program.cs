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
using SteamTradeBot.Backend.BusinessLogicLayer.Factories;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules.CancelRules;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules.SellRules;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Middlewares;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Services;
using System;
using System.IO;
using SteamTradeBot.Backend.BusinessLogicLayer;

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

builder.Services.AddDbContextFactory<TradeBotDataContext>(options =>
{
    var postgresConnectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");
    if (postgresConnectionString is not null)
        options.UseNpgsql(postgresConnectionString);
    else
    {
        var sqlServerConnectionString = builder.Configuration["MsSqlServerConnectionString"];
        options.UseSqlServer(sqlServerConnectionString);
    }
});

var webDriverHostFromEnvironment = Environment.GetEnvironmentVariable("SELENIUM_HOST");

builder.Services.AddSingleton<WorkerService>();
builder.Services.AddSingleton<IConfigurationManager, JsonFileBasedConfigurationManagerService>();
builder.Services.AddSingleton<IStateManager, DbBasedStateManagerService>();

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
    //chromeOptions.AddArgument("--headless");
    chromeOptions.AddArgument("--disable-logging");
    chromeOptions.AddArgument("--log-level=3");
    if (webDriverHostFromEnvironment is not null)
        return new RemoteWebDriver(new Uri(webDriverHostFromEnvironment), chromeOptions.ToCapabilities());
    var driverService = ChromeDriverService.CreateDefaultService();
    driverService.EnableVerboseLogging = false;
    driverService.SuppressInitialDiagnosticInformation = true;
    return new ChromeDriver(driverService, chromeOptions);
}));

builder.Services.AddTransient<OrdersDbAccess>();
builder.Services.AddTransient<HistoryDbAccess>();
builder.Services.AddTransient<StateDbAccess>();
builder.Services.AddTransient<TokenDbAccess>();

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
builder.Services.AddTransient<ItemsNamesProvider>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.UseMiddleware<TokenAuthenticationMiddleware>();
app.UseMiddleware<ExclusiveAccessMiddleware>();
app.UseExceptionHandler(async exceptionHandlerApp =>  
{
    var exception = exceptionHandlerApp.ServerFeatures.Get<IExceptionHandlerFeature>()?.Error;
    if (exception is null)
        return;
    var stateManager = exceptionHandlerApp.ApplicationServices.GetService<IStateManager>();
    if (stateManager is null)
    {
        Log.Error("Application error: stateManager does not configured (ExceptionHandler)");
        return;
    }
    await stateManager.OnErrorAsync(exception);
    exceptionHandlerApp.Run(async context => await Results.Problem().ExecuteAsync(context));
});

app.Run();