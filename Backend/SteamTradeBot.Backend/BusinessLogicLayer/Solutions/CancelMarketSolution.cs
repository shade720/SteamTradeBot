using System.Threading.Tasks;
using Serilog;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Solutions;

public class CancelMarketSolution : MarketSolution
{
    public CancelMarketSolution(ISteamApi api, IConfigurationManager configurationManager, IStateManager stateManager, MarketDbAccess marketDb) : 
        base(api, configurationManager, stateManager, marketDb) { }

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
        await StateManager.OnItemCancellingAsync(order);
    }
}