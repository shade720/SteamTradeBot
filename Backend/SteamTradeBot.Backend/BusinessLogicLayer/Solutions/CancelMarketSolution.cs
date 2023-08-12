using System.Linq;
using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Abstractions;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.ItemModel;
using SteamTradeBot.Backend.Services;
using ConfigurationManager = SteamTradeBot.Backend.Services.ConfigurationManager;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Solutions;

public class CancelMarketSolution : MarketSolution
{
    public CancelMarketSolution(SteamAPI api, MarketDbAccess marketDb, ConfigurationManager configurationManager, StateManagerService stateManager) : base(api, marketDb, configurationManager, stateManager) { }

    public override void Perform(ItemPage itemPage)
    {
        throw new System.NotImplementedException();
    }

    public override async Task PerformAsync(ItemPage itemPage)
    {
        var order = await MarketDb.GetBuyOrderAsync(itemPage.EngItemName, itemPage.UserName);
        await SteamApi.CancelBuyOrderAsync(order.ItemUrl);
        Log.Information("Cancel buy order {0} (Price: {1}, Quantity: {2})", order.EngItemName, order.Price, order.Quantity);
        await MarketDb.RemoveBuyOrderAsync(order);
        StateManager.OnItemCancelling(order);
    }
}