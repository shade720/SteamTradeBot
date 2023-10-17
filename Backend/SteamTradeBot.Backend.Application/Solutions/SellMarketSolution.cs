using Serilog;
using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.ItemModel;

namespace SteamTradeBot.Backend.Application.Solutions;

internal sealed class SellMarketSolution : MarketSolution
{
    public SellMarketSolution(
        ISteamApi api, 
        IConfigurationService configurationService, 
        ITradingEventHandler tradingEventHandler, 
        OrdersRepository ordersRepository) 
        : base(api, configurationService, tradingEventHandler, ordersRepository) { }

    public override void Perform(ItemPage itemPage)
    {
        throw new NotImplementedException();
    }

    public override async Task PerformAsync(ItemPage itemPage)
    {
        Log.Logger.Information("Placing sell order ({0})...", itemPage.EngItemName);
        var buyOrder = await OrdersRepository.GetOrderAsync(itemPage.EngItemName, ConfigurationService.ApiKey, OrderType.BuyOrder);

        if (buyOrder is null)
            throw new Exception("Can't load stored buy order for sell order forming.");

        if (itemPage.MyBuyOrder is not null && itemPage.MyBuyOrder.Quantity == buyOrder.Quantity)
            throw new Exception($"Can't form sell order. Nothing was bought. Current quantity: {itemPage.MyBuyOrder.Quantity}; Stored quantity: {buyOrder.Quantity}");

        var sellOrder = new Order
        {
            ApiKey = ConfigurationService.ApiKey,
            EngItemName = itemPage.EngItemName,
            RusItemName = itemPage.RusItemName,
            ItemUrl = itemPage.ItemUrl,
            OrderType = OrderType.SellOrder,
            BuyPrice = buyOrder.BuyPrice,
            SellPrice = buyOrder.SellPrice,
            Quantity = 1
        };

        var itemsCountToSell = itemPage.MyBuyOrder is null
            ? buyOrder.Quantity
            : buyOrder.Quantity - itemPage.MyBuyOrder.Quantity;

        for (var itemNum = 0; itemNum < itemsCountToSell; itemNum++)
        {
            var isPlacedSuccessfully = await SteamApi.PlaceSellOrderAsync(sellOrder.EngItemName, sellOrder.SellPrice,
                ConfigurationService.SteamUserId);
            if (isPlacedSuccessfully)
            {
                buyOrder.Quantity -= 1;
                if (buyOrder.Quantity > 0)
                    await OrdersRepository.AddOrUpdateOrderAsync(buyOrder);
                else
                    await OrdersRepository.RemoveOrderAsync(buyOrder);

                await OrdersRepository.AddOrUpdateOrderAsync(sellOrder);
                await TradingEventHandler.OnItemSellingAsync(sellOrder);
            }
            else
                throw new ApplicationException($"Can't place sell order {sellOrder.EngItemName} (Price: {sellOrder.SellPrice}). Item not found in inventory!");
        }
    }
}