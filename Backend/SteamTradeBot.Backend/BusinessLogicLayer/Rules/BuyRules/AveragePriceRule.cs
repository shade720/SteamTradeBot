using System.Linq;
using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;

public sealed class AveragePriceRule : IBuyRule
{
    private readonly IConfigurationManager _configurationManager;

    public AveragePriceRule(IConfigurationManager configurationManager)
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
            if (itemPage.BuyOrderBook.Count <= 0)
            {
                Log.Information("Average price is bad. Reason: can't check average price, buy order book is empty.");
                return false;
            }

            var avgPriceFromChart = AveragePriceFromChart(itemPage.SalesChart);
            if (itemPage.BuyOrderBook.Any(buyOrder => IsBuyPriceLowerThanAverage(buyOrder.Price, avgPriceFromChart, _configurationManager.AveragePriceRatio)))
            {
                Log.Information("Average price is ok.");
                return true;
            }
            Log.Information("Average price is bad. Reason: average price is higher than needed. Min buy price: {0} < Required average price: {1}",
                itemPage.BuyOrderBook.Min(buyOrder => buyOrder.Price), avgPriceFromChart * (1 + _configurationManager.AveragePriceRatio));
            return false;
        });
    }

    private static bool IsBuyPriceLowerThanAverage(double buyPrice, double averagePrice, double avgPriceRatio)
    {
        return buyPrice < averagePrice * (1 + avgPriceRatio);
    }

    private static double AveragePriceFromChart(Chart salesChart)
    {
        return salesChart.Sum(x => x.Price * x.Quantity) / salesChart.Sum(x => x.Quantity);
    }
}