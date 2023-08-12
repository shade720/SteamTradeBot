using System.Linq;
using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;
using ConfigurationManager = SteamTradeBot.Backend.Services.ConfigurationManager;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;

public class AveragePriceRule : IBuyRule
{
    private readonly ConfigurationManager _configurationManager;

    public AveragePriceRule(ConfigurationManager configurationManager)
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
            Log.Information("Checking average price...");

            var avgPriceFromChart = AveragePrice(itemPage.SalesChart);

            if (itemPage.SellOrderBook.Any(sellOrder => sellOrder.Price > avgPriceFromChart + _configurationManager.AveragePrice))
                return true;
            Log.Information("Item is not profitable. Reason: average price is higher than needed. Max sell price: {0} < Required average price: {1}",
                itemPage.SellOrderBook.Max(sellOrder => sellOrder.Price), avgPriceFromChart + _configurationManager.AveragePrice);
            return false;
        });
    }

    private static double AveragePrice(Chart salesChart)
    {
        return salesChart.Sum(x => x.Price * x.Quantity) / salesChart.Sum(x => x.Quantity);
    }
}