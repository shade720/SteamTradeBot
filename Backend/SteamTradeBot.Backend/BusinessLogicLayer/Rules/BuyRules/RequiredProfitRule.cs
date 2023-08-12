using System.Linq;
using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;
using SteamTradeBot.Backend.Services;
using ConfigurationManager = SteamTradeBot.Backend.Services.ConfigurationManager;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;

public class RequiredProfitRule : IBuyRule
{
    private readonly ConfigurationManager _configurationManager;

    public RequiredProfitRule(ConfigurationManager configurationManager)
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

            var buyPrice = itemPage.BuyOrderBook.FirstOrDefault(buyOrder => itemPage.SellOrderBook.Any(sellOrder =>
                sellOrder.Price + _configurationManager.RequiredProfit > buyOrder.Price * (1 + _configurationManager.SteamCommission)));
            if (buyPrice is null)
            {
                Log.Information("Item is not profitable. Reason: profitable buy price is not found. Required price: {0}, Available price: {1}",
                    itemPage.SellOrderBook.Max(sellOrder => sellOrder.Price) + _configurationManager.RequiredProfit, itemPage.BuyOrderBook.Min(order => order.Price) + _configurationManager.SteamCommission);
                return false;
            }

            var sellPrice = itemPage.SellOrderBook.FirstOrDefault(sellOrder =>
                sellOrder.Price + _configurationManager.RequiredProfit > buyPrice.Price * (1 + _configurationManager.SteamCommission));
            if (sellPrice is null)
                return false;

            itemPage.EstimatedBuyPrice = buyPrice.Price;
            itemPage.EstimatedSellPrice = sellPrice.Price;

            return false;
        });
    }
}