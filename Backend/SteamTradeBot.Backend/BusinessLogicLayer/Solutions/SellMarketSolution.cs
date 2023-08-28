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
        MarketDbAccess marketDb) 
        : base(api, configurationManager, stateManager, marketDb) { }

    public override void Perform(ItemPage itemPage)
    {
        throw new NotImplementedException();
    }

    public override async Task PerformAsync(ItemPage itemPage)
    {
        Log.Logger.Information("Placing sell order ({0})...", itemPage.EngItemName);
        var buyOrder = await MarketDb.GetBuyOrderAsync(itemPage.EngItemName, ConfigurationManager.ApiKey);

        if (buyOrder is null)
            throw new Exception("Can't load stored buy order for sell order forming.");
        if (itemPage.MyBuyOrder is not null && itemPage.MyBuyOrder.Quantity == buyOrder.Quantity)
            throw new Exception($"Can't form sell order. Nothing was bought. Current quantity: {itemPage.MyBuyOrder.Quantity}; Stored quantity: {buyOrder.Quantity}");

        var itemsCountToSell = itemPage.MyBuyOrder is null
            ? buyOrder.Quantity
            : buyOrder.Quantity - itemPage.MyBuyOrder.Quantity;

        var sellOrder = new SellOrder
        {
            ApiKey = ConfigurationManager.ApiKey,
            EngItemName = itemPage.EngItemName,
            RusItemName = itemPage.RusItemName,
            ItemUrl = itemPage.ItemUrl,
            PurchasedPrice = buyOrder.BuyPrice,
            SellPrice = buyOrder.EstimatedSellPrice,
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
                    await MarketDb.AddOrUpdateBuyOrderAsync(buyOrder);
                else
                    await MarketDb.RemoveBuyOrderAsync(buyOrder);

                await MarketDb.AddSellOrderAsync(sellOrder);
                await StateManager.OnItemSellingAsync(sellOrder);
                Log.Information("Sell order {0} (Price: {1}) placed successfully.", sellOrder.EngItemName, sellOrder.SellPrice);
            }
            else
                Log.Error("Can't place sell order {0} (Price: {1}). Item not found in inventory!", sellOrder.EngItemName, sellOrder.SellPrice);
        }
    }
}