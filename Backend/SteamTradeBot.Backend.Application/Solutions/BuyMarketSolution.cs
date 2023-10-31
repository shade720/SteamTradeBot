using Serilog;
using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.ItemPageAggregate;
using SteamTradeBot.Backend.Domain.OrderAggregate;

namespace SteamTradeBot.Backend.Application.Solutions;

internal sealed class BuyMarketSolution : MarketSolution
{
    public BuyMarketSolution(
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
        Log.Logger.Information("Placing buy order ({0})...", itemPage.EngItemName);

        if (itemPage.EstimatedBuyPrice is null || itemPage.EstimatedSellPrice is null)
            throw new Exception("Can't place buy order. Estimated prices are empty.");
        
        var buyOrder = new Order
        {
            ApiKey = ConfigurationService.ApiKey,
            EngItemName = itemPage.EngItemName,
            RusItemName = itemPage.RusItemName,
            ItemUrl = itemPage.ItemUrl,
            OrderType = OrderType.BuyOrder,
            BuyPrice = itemPage.EstimatedBuyPrice.Value,
            SellPrice = itemPage.EstimatedSellPrice.Value,
            Quantity = ConfigurationService.OrderQuantity
        };

        await SteamApi.PlaceBuyOrderAsync(buyOrder.ItemUrl, buyOrder.BuyPrice, buyOrder.Quantity);
        await OrdersRepository.AddOrUpdateOrderAsync(buyOrder);
        await TradingEventHandler.OnItemBuyingAsync(buyOrder);
    }
}