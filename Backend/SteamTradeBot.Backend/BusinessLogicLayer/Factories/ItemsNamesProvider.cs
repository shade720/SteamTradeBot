using Serilog;
using SteamTradeBot.Backend.DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using SteamTradeBot.Backend.Models.Abstractions;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Factories;

public sealed class ItemsNamesProvider
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
            Log.Information("Load items names for analysis...");

            var loadedItemNamesList = await _api.GetItemNamesListAsync(
                    _configurationManager.MinPrice,
                    _configurationManager.MaxPrice,
                    _configurationManager.SalesPerDay * 7,
                    _configurationManager.ItemListSize);
            var existingOrders = await _marketDb.GetBuyOrdersAsync(_configurationManager.ApiKey);
            foreach (var existingOrderName in existingOrders.Select(x => x.EngItemName))
            {
                loadedItemNamesList.Insert(0, existingOrderName);
                Log.Logger.Information("Add {0} as existing order", existingOrderName);
            }
            Log.Information("Items names list loaded.");

            foreach (var name in loadedItemNamesList)
            {
                yield return name;
            }
        }
    }
}