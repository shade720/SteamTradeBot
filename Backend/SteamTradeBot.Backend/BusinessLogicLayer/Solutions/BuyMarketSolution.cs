using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.ItemModel;
using System;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Solutions;

public sealed class BuyMarketSolution : MarketSolution
{
    public BuyMarketSolution(
        ISteamApi api, 
        IConfigurationService configurationService, 
        IStateService stateService, 
        OrdersRepository ordersRepository) : 
        base(api, configurationService, stateService, ordersRepository) { }

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
        await StateService.OnItemBuyingAsync(buyOrder);

        Log.Logger.Information("Buy order {0} has been placed:\r\nItem name: {1}\r\nUrl: {2}\r\nBuy price: {3}\r\nEstimated sell price: {4}\r\nQuantity: {5}", 
            buyOrder.EngItemName, buyOrder.ItemUrl, buyOrder.ItemUrl, buyOrder.BuyPrice, buyOrder.SellPrice, buyOrder.Quantity);
    }
}