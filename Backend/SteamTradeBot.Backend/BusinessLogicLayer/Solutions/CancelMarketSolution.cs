using System.Linq;
using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Abstractions;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.ItemModel;
using SteamTradeBot.Backend.Services;
using ConfigurationManager = SteamTradeBot.Backend.Services.ConfigurationManager;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Solutions;

public class CancelMarketSolution : MarketSolution
{
    public CancelMarketSolution(SteamAPI api, MarketDbAccess marketDb, ConfigurationManager configurationManager) : base(api, marketDb, configurationManager) { }

    public override void Perform(ItemPage itemPage)
    {
        var order = MarketDb.GetBuyOrders().FirstOrDefault(x => x.EngItemName == itemPage.EngItemName);
        SteamApi.CancelBuyOrder(order.ItemUrl);
        Log.Information("Cancel buy order {0} (Price: {1}, Quantity: {2})", order.EngItemName, order.Price, order.Quantity);
        MarketDb.RemoveBuyOrder(order);
    }
}