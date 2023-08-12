using System.Linq;
using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;
using ConfigurationManager = SteamTradeBot.Backend.Services.ConfigurationManager;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.BuyRules;

public class SalesCountRule : IBuyRule
{
    private readonly ConfigurationManager _configurationManager;

    public SalesCountRule(ConfigurationManager configurationManager)
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
            Log.Information("Checking sales per week...");

            var salesByDay = SalesByDay(itemPage.SalesChart);
            if (salesByDay > _configurationManager.SalesPerWeek)
                return true;
            Log.Information("Item is not profitable. Reason: sales volume is lower than needed. Current sales: {0} < Required sales: {1}", salesByDay, _configurationManager.SalesPerWeek);
            return false;
        });
    }

    private static double SalesByDay(Chart salesChart)
    {
        return salesChart
            .GroupBy(x => x.Date.Date)
            .Select(x => x.Sum(y => y.Quantity))
            .Average();
    }
}