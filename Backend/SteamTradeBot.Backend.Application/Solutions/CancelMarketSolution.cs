using Serilog;
using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.ItemPageAggregate;
using SteamTradeBot.Backend.Domain.OrderAggregate;

namespace SteamTradeBot.Backend.Application.Solutions;

internal sealed class CancelMarketSolution : MarketSolution
{
    public CancelMarketSolution(
        ISteamApi api,
        IConfigurationService configurationService,
        ITradingEventHandler tradingEventHandler,
        IOrdersRepository ordersRepository)
        : base(api, configurationService, tradingEventHandler, ordersRepository) { }

    public override void Perform(ItemPage itemPage)
    {
        throw new NotImplementedException();
    }

    public override async Task PerformAsync(ItemPage itemPage)
    {
        Log.Information("Cancelling buy order ({0})...", itemPage.EngItemName);
        if (itemPage.MyBuyOrder is null)
            throw new Exception("Can't cancel buy order. MyBuyOrder is null.");

        var order = await OrdersRepository.GetOrderAsync(itemPage.EngItemName, ConfigurationService.ApiKey, OrderType.BuyOrder);
        if (order is null)
            throw new Exception("Can't load stored buy order from database.");

        var successfullyCanceled = await SteamApi.CancelBuyOrderAsync(itemPage.MyBuyOrder.ItemUrl);
        if (successfullyCanceled)
        {
            await OrdersRepository.RemoveOrderAsync(order);
            await TradingEventHandler.OnItemCancellingAsync(order);
        }
        else
        {
            Log.Logger.Warning("Can't cancel item {0} (Price: {1}, Quantity: {2})",
                order.EngItemName, order.BuyPrice, order.Quantity);
        }
    }
}