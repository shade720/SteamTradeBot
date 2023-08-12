using SteamTradeBot.Backend.BusinessLogicLayer.Abstractions;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.ItemModel;
using System;
using System.Linq;
using System.Threading.Tasks;
using SteamTradeBot.Backend.Services;
using ConfigurationManager = SteamTradeBot.Backend.Services.ConfigurationManager;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Solutions;

public class BuyMarketSolution : MarketSolution
{
    public BuyMarketSolution(SteamAPI api, MarketDbAccess marketDb, ConfigurationManager configurationManager, StateManagerService stateManager) : base(api, marketDb, configurationManager, stateManager) { }

    public override void Perform(ItemPage itemPage)
    {
        throw new NotImplementedException();
    }

    public override async Task PerformAsync(ItemPage itemPage)
    {
        var price = itemPage.BuyOrderBook.FirstOrDefault(buyOrder => itemPage.SellOrderBook.Any(sellOrder => sellOrder.Price + ConfigurationManager.RequiredProfit > buyOrder.Price * (1 + ConfigurationManager.SteamCommission)));
        if (price is null)
            throw new Exception("Sell order not found");
        var buyOrder = new BuyOrder
        {
            EngItemName = itemPage.EngItemName,
            RusItemName = itemPage.RusItemName,
            ItemUrl = itemPage.ItemUrl,
            Price = price.Price,
            Quantity = ConfigurationManager.OrderQuantity
        };
        await SteamApi.PlaceBuyOrderAsync(buyOrder.ItemUrl, buyOrder.Price, buyOrder.Quantity);
        await MarketDb.AddOrUpdateBuyOrderAsync(buyOrder);
        StateManager.OnItemBuying(buyOrder);
    }
}