using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Serilog;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Rules.CancelRules;

public class FitPriceRule : ICancelRule
{
    private readonly SteamAPI _api;
    private readonly IConfiguration _configuration;

    public FitPriceRule(SteamAPI api, IConfiguration configuration)
    {
        _api = api;
        _configuration = configuration;
    }
    public bool IsFollowed(ItemPage itemPage)
    {
        if (itemPage.MyBuyOrder is null) return false;
        Log.Information("Checking if order obsolete...");
        const int findRange = 1;
        var sellOrderBook = _api.GetSellOrdersBook(itemPage.ItemUrl, findRange);
        var fitPriceRange = double.Parse(_configuration["FitPriceRange"]!, NumberStyles.Any, CultureInfo.InvariantCulture);
        return sellOrderBook.Any(x => x.Price - itemPage.MyBuyOrder.Price > fitPriceRange);
    }
}