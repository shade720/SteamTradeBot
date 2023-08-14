using SteamTradeBot.Backend.DataAccessLayer;
using System;
using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.Models.ItemModel;
using SteamTradeBot.Backend.Models.Abstractions;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Solutions;

public class SellMarketSolution : MarketSolution
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
        var buyOrder = await MarketDb.GetBuyOrderAsync(itemPage.EngItemName, itemPage.UserName);
        if (buyOrder is null)
            throw new Exception("Can't find local buy order for sell order forming");
        var sellOrder = new SellOrder
        {
            EngItemName = itemPage.EngItemName,
            RusItemName = itemPage.RusItemName,
            ItemUrl = itemPage.ItemUrl,
            Price = buyOrder.Price * (1 + ConfigurationManager.SteamCommission) + ConfigurationManager.RequiredProfit,
            Quantity = itemPage.MyBuyOrder is null ? buyOrder.Quantity : buyOrder.Quantity - itemPage.MyBuyOrder.Quantity
        };

        for (var i = 0; i < sellOrder.Quantity; i++)
        {
            if (await SteamApi.PlaceSellOrderAsync(sellOrder.EngItemName, sellOrder.Price, ConfigurationManager.SteamUserId))
                Log.Information("Place sell order {0} (Price: {1})", sellOrder.EngItemName, sellOrder.Price);
            else
                Log.Error("Can't place sell order {0} (Price: {1}). Item not found in inventory!", sellOrder.EngItemName, sellOrder.Price);
        }

        if (itemPage.MyBuyOrder is null)
        {
            await MarketDb.RemoveBuyOrderAsync(buyOrder);
        }
        if (itemPage.MyBuyOrder is not null && itemPage.MyBuyOrder.Quantity > 0)
        {
            buyOrder.Quantity = itemPage.MyBuyOrder.Quantity;
            await MarketDb.AddOrUpdateBuyOrderAsync(buyOrder);
        }
        await MarketDb.AddOrUpdateSellOrderAsync(sellOrder);
        await StateManager.OnItemSellingAsync(sellOrder);
    }
}