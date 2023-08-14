using System.Linq;
using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;

public class RequiredProfitRule : IBuyRule
{
    private readonly IConfigurationManager _configurationManager;

    public RequiredProfitRule(IConfigurationManager configurationManager)
    {
        _configurationManager = configurationManager;
    }

    public bool IsFollowed(ItemPage itemPage)
    {
        throw new System.NotImplementedException();
    }

    public async Task<bool> IsFollowedAsync(ItemPage itemPage)
    {
        return await Task.Run(() =>
        {
            Log.Information("Finding profitable order...");

            var buyPrice = itemPage.BuyOrderBook.FirstOrDefault(buyOrder => itemPage.SellOrderBook.Any(sellOrder => IsBuyPriceProfitable(buyOrder.Price, sellOrder.Price, _configurationManager.SteamCommission, _configurationManager.RequiredProfit)));
            if (buyPrice is null)
            {
                Log.Information("Item is not profitable. Reason: profitable buy price is not found. Available price: {1} - Required price: {0}",
                    itemPage.BuyOrderBook.Min(order => order.Price), itemPage.SellOrderBook.Max(sellOrder => sellOrder.Price) * (1 - _configurationManager.SteamCommission) - _configurationManager.RequiredProfit);
                return false;
            }

            var sellPrice = itemPage.SellOrderBook.FirstOrDefault(sellOrder => IsBuyPriceProfitable(buyPrice.Price, sellOrder.Price, _configurationManager.SteamCommission, _configurationManager.RequiredProfit));
            if (sellPrice is null)
                return false;

            itemPage.EstimatedBuyPrice = buyPrice.Price;
            itemPage.EstimatedSellPrice = sellPrice.Price;

            return true;
        });
    }

    private static bool IsBuyPriceProfitable(double buyPrice, double sellPrice, double commission, double requiredProfit)
    {
        return buyPrice < sellPrice * (1 - commission) - requiredProfit;
    }
}