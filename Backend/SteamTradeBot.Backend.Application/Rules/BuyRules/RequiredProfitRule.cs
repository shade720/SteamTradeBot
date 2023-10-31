using Serilog;
using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Domain.Abstractions.Rules;
using SteamTradeBot.Backend.Domain.ItemPageAggregate;

namespace SteamTradeBot.Backend.Application.Rules.BuyRules;

internal sealed class RequiredProfitRule : IBuyRule
{
    private const double SellPriceReduceValue = 0.02;
    private readonly IConfigurationService _configurationService;

    public RequiredProfitRule(IConfigurationService configurationService)
    {
        _configurationService = configurationService;
    }

    public bool IsFollowed(ItemPage itemPage)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsFollowedAsync(ItemPage itemPage)
    {
        return await Task.Run(() =>
        {
            if (itemPage.BuyOrderBook.Count <= 0)
            {
                Log.Information("Required profit is bad. Can't find profitable order, buy order book is empty.");
                return false;
            }
            if ( itemPage.SellOrderBook.Count <= 0)
            {
                Log.Information("Required profit is bad. Can't find profitable order, sell order book is empty.");
                return false;
            }
            
            var isPricesFound = GetBestPricesFromListings(
                itemPage.BuyOrderBook,
                itemPage.SellOrderBook,
                _configurationService.SteamCommission,
                _configurationService.RequiredProfit,
                out var buyPrice, out var sellPrice);

            if (!isPricesFound)
            {
                Log.Information("Required profit is bad. Can't find profitable order, prices not found.");
                return false;
            }

            itemPage.EstimatedBuyPrice = buyPrice;
            itemPage.EstimatedSellPrice = sellPrice;

            Log.Information("Required profit is ok.\r\nEstimated buy price: {0}\r\nEstimated sell price: {1}", 
                itemPage.EstimatedBuyPrice, itemPage.EstimatedSellPrice);
            return true;
        });
    }

    private static bool GetBestPricesFromListings(IEnumerable<OrderBookItem> buyListing, IReadOnlyCollection<OrderBookItem> sellListing, double commission, double requiredProfit, out double buyPrice, out double sellPrice)
    {
        buyPrice = 0;
        sellPrice = 0;
        foreach (var buyOrder in buyListing)
        {
            var profitSellOrder = sellListing.FirstOrDefault(sellOrder =>
                IsBuyPriceProfitable(buyOrder.Price, sellOrder.Price, commission, requiredProfit));
            if (profitSellOrder is null)
                continue;
            buyPrice = buyOrder.Price;
            sellPrice = profitSellOrder.Price - SellPriceReduceValue;
            return true;
        }
        return false;
    }

    private static bool IsBuyPriceProfitable(double buyPrice, double sellPrice, double commission, double requiredProfit)
    {
        return buyPrice < sellPrice * (1 - commission) - requiredProfit;
    }
}