using Serilog;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Factories;

public class ItemsNamesProvider
{
    private readonly ConfigurationManager _configurationManager;
    private readonly SteamAPI _api;
    private readonly MarketDbAccess _marketDb;

    public ItemsNamesProvider(ConfigurationManager configurationManager, SteamAPI api, MarketDbAccess marketDb)
    {
        _configurationManager = configurationManager;
        _api = api;
        _marketDb = marketDb;
    }

    public async IAsyncEnumerable<string> GetNamesAsync()
    {
        while (true)
        {
            Log.Information("Load items for analysis....");
            var loadedItemNamesList = await _api.GetItemNamesListAsync(
                    _configurationManager.MinPrice,
                    _configurationManager.MaxPrice,
                    _configurationManager.SalesPerWeek * 7,
                    _configurationManager.ItemListSize);
            var existingOrdersItemNames = await _marketDb.GetBuyOrdersAsync();
            foreach (var itemName in existingOrdersItemNames.Select(x => x.EngItemName))
            {
                loadedItemNamesList.Insert(0, itemName);
                Log.Logger.Information("Add {0} as existing order", itemName);
            }
            Log.Information("Pipeline initialized");

            foreach (var name in loadedItemNamesList)
            {
                yield return name;
            }
        }
    }
}