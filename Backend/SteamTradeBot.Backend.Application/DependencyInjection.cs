using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SteamTradeBot.Backend.Application.EventHandlers;
using SteamTradeBot.Backend.Application.Factories;
using SteamTradeBot.Backend.Application.Rules;
using SteamTradeBot.Backend.Application.Rules.BuyRules;
using SteamTradeBot.Backend.Application.Rules.CancelRules;
using SteamTradeBot.Backend.Application.Rules.SellRules;
using SteamTradeBot.Backend.Application.Services;
using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Domain.Abstractions.Rules;

namespace SteamTradeBot.Backend.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddTradeBotApplication(this IServiceCollection services, IConfiguration configuration, string configurationPath)
    {
        services.AddScoped<WorkerService>();

        services.AddSingleton<ItemsNamesProvider>();
        services.AddSingleton<ItemPageFactory>();
        services.AddSingleton<SolutionsFactory>();

        services.AddSingleton<MarketRules>();
        services.AddSingleton<IBuyRule, AvailableBalanceRule>();
        services.AddSingleton<IBuyRule, AveragePriceRule>();
        services.AddSingleton<IBuyRule, SalesCountRule>();
        services.AddSingleton<IBuyRule, OrderAlreadyExistRule>();
        services.AddSingleton<IBuyRule, RequiredProfitRule>();
        services.AddSingleton<IBuyRule, TrendRule>();
        services.AddSingleton<ISellRule, CurrentQuantityCheckRule>();
        services.AddSingleton<ICancelRule, FitPriceRule>();

        
        services.AddSingleton<ITradingEventHandler, DbBasedTradingEventHandler>();
        services.Decorate<ITradingEventHandler, TimerUpdateTradingEventHandler>();
        services.Decorate<ITradingEventHandler, LogEventHandler>();
        services.Decorate<ITradingEventHandler, SignalTradingEventHandler>();

        services.AddSignalR();

        services.AddSingleton<IConfigurationService>(_ => new JsonFileBasedConfigurationService(configuration, configurationPath));
        services.AddScoped<IEventHistoryAgent, DbEventHistoryAgent>();
        services.AddScoped<LogsProviderService>();
        services.AddScoped<OrderCancellingService>();
        return services;
    }

    public static IApplicationBuilder ConfigureTradeBotApplication(this IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<SignalTradingEventHandler>("/tradingEventHandler");
        });
        return app;
    }
}