using SteamTradeBot.Backend.DataAccessLayer;
using System;
using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.Models.ItemModel;
using SteamTradeBot.Backend.Models.Abstractions;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Solutions;

public sealed class SellMarketSolution : MarketSolution
{
    public SellMarketSolution(
        ISteamApi api, 
        IConfigurationManager configurationManager, 
        IStateManager stateManager, 
        OrdersDbAccess ordersDb) 
        : base(api, configurationManager, stateManager, ordersDb) { }

    public override void Perform(ItemPage itemPage)
    {
        throw new NotImplementedException();
    }

    public override async Task PerformAsync(ItemPage itemPage)
    {
        Log.Logger.Information("Placing sell order ({0})...", itemPage.EngItemName);
        var buyOrder = await OrdersDb.GetOrderAsync(itemPage.EngItemName, ConfigurationManager.ApiKey, OrderType.BuyOrder);

        if (buyOrder is null)
            throw new Exception("Can't load stored buy order for sell order forming.");

        if (itemPage.MyBuyOrder is not null && itemPage.MyBuyOrder.Quantity == buyOrder.Quantity)
            throw new Exception($"Can't form sell order. Nothing was bought. Current quantity: {itemPage.MyBuyOrder.Quantity}; Stored quantity: {buyOrder.Quantity}");

        var itemsCountToSell = itemPage.MyBuyOrder is null
            ? buyOrder.Quantity
            : buyOrder.Quantity - itemPage.MyBuyOrder.Quantity;

        var sellOrder = new Order
        {
            ApiKey = ConfigurationManager.ApiKey,
            EngItemName = itemPage.EngItemName,
            RusItemName = itemPage.RusItemName,
            ItemUrl = itemPage.ItemUrl,
            OrderType = OrderType.SellOrder,
            BuyPrice = buyOrder.BuyPrice,
            SellPrice = buyOrder.SellPrice,
            Quantity = 1
        };

        for (var itemNum = 0; itemNum < itemsCountToSell; itemNum++)
        {
            var isPlacedSuccessfully = await SteamApi.PlaceSellOrderAsync(sellOrder.EngItemName, sellOrder.SellPrice,
                ConfigurationManager.SteamUserId);
            if (isPlacedSuccessfully)
            {
                buyOrder.Quantity -= 1;
                if (buyOrder.Quantity > 0)
                    await OrdersDb.AddOrUpdateOrderAsync(buyOrder);
                else
                    await OrdersDb.RemoveOrderAsync(buyOrder);

                await OrdersDb.AddOrUpdateOrderAsync(sellOrder);
                await StateManager.OnItemSellingAsync(sellOrder);
                Log.Information("Sell order {0} (Price: {1}) placed successfully.", sellOrder.EngItemName, sellOrder.SellPrice);
            }
            else
                Log.Error("Can't place sell order {0} (Price: {1}). Item not found in inventory!", sellOrder.EngItemName, sellOrder.SellPrice);
        }
    }
}