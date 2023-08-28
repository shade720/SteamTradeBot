using System;
using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Solutions;

public sealed class CancelMarketSolution : MarketSolution
{
    public CancelMarketSolution(
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
        Log.Information("Cancelling buy order ({0})...", itemPage.EngItemName);
        var order = await OrdersDb.GetOrderAsync(itemPage.EngItemName, ConfigurationManager.ApiKey, OrderType.BuyOrder);
        if (order is null)
            throw new Exception("Can't load stored buy order from database.");
        await SteamApi.CancelBuyOrderAsync(order.ItemUrl);
        await OrdersDb.RemoveOrderAsync(order);
        await StateManager.OnItemCancellingAsync(order);
        Log.Information("Buy order {0} (Price: {1}, Quantity: {2}) has been canceled.", order.EngItemName, order.BuyPrice, order.Quantity);
    }
}