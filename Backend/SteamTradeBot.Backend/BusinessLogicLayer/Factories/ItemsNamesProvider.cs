using Serilog;
using SteamTradeBot.Backend.DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using SteamTradeBot.Backend.Models.Abstractions;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Factories;

public class ItemsNamesProvider
{
    private readonly IConfigurationManager _configurationManager;
    private readonly ISteamApi _api;
    private readonly MarketDbAccess _marketDb;

    public ItemsNamesProvider(
        ISteamApi api, 
        IConfigurationManager configurationManager, 
        MarketDbAccess marketDb)
    {
        _api = api;
        _configurationManager = configurationManager;
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
                    _configurationManager.SalesPerDay * 7,
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