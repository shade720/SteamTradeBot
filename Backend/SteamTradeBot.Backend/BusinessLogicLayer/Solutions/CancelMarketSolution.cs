using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.ItemModel;
using System;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Solutions;

public sealed class CancelMarketSolution : MarketSolution
{
    public CancelMarketSolution(
        ISteamApi api, 
        IConfigurationService configurationService, 
        IStateService stateService, 
        OrdersRepository ordersRepository) 
        : base(api, configurationService, stateService, ordersRepository) { }

    public override void Perform(ItemPage itemPage)
    {
        throw new NotImplementedException();
    }

    public override async Task PerformAsync(ItemPage itemPage)
    {
        Log.Information("Cancelling buy order ({0})...", itemPage.EngItemName);
        var order = await OrdersRepository.GetOrderAsync(itemPage.EngItemName, ConfigurationService.ApiKey, OrderType.BuyOrder);
        if (order is null)
            throw new Exception("Can't load stored buy order from database.");
        var successfullyCanceled = await SteamApi.CancelBuyOrderAsync(order.ItemUrl);
        if (successfullyCanceled)
        {
            await OrdersRepository.RemoveOrderAsync(order);
            await OrdersRepository.RemoveOrderAsync(order);
            await StateService.OnItemCancellingAsync(order);
            Log.Information("Buy order {0} (Price: {1}, Quantity: {2}) has been canceled.", order.EngItemName, order.BuyPrice, order.Quantity);
        }
        else
        {
            Log.Logger.Warning("Can't cancel item {0} (Price: {1}, Quantity: {2})",
                order.EngItemName, order.BuyPrice, order.Quantity);
        }
    }
}