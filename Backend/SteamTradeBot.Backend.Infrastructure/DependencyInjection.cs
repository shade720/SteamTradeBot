using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain;
using SteamTradeBot.Backend.Application.SteamConnectors.Selenium;
using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Infrastructure.SteamConnectors.Selenium;

namespace SteamTradeBot.Backend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<OrdersRepository, SqlOrdersRepository>();
        services.AddTransient<HistoryRepository, SqlHistoryRepository>();
        services.AddTransient<StateRepository, SqlStateRepository>();
        services.AddTransient<TokenRepository, SqlTokenRepository>();
        services.AddDbContextFactory<TradeBotDataContext>(options =>
        {
            var postgresConnectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");
            if (postgresConnectionString is not null)
            {
                options.UseNpgsql(postgresConnectionString);
            }
            else
            {
                var sqlServerConnectionString = configuration["MsSqlServerConnectionString"];
                options.UseSqlServer(sqlServerConnectionString);
            }
        });

        var webDriverHostFromEnvironment = Environment.GetEnvironmentVariable("SELENIUM_HOST");
        services.AddSingleton<ISteamApi, SeleniumSteamApi>();
        services.AddSingleton<IItemsTableApi, SkinsTableApi>();
        services.AddSingleton(_ => new SeleniumWebDriver(() =>
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
        return services;
    }
}