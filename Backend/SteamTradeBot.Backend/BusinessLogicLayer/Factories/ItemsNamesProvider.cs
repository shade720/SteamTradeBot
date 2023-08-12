using System.Collections.Generic;
using System.Linq;
using Serilog;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Services;

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

    public IEnumerable<string> Names
    {
        get
        {
            while (true)
            {
                var currentConfiguration = _configurationManager.GetConfiguration();
                Log.Information("Load items for analysis....");
                var loadedItemNamesList = _api.GetItemNamesList(
                        currentConfiguration.MinPrice,
                        currentConfiguration.MaxPrice,
                        currentConfiguration.SalesPerWeek * 7,
                        currentConfiguration.ItemListSize)
                    .ToList();
                var existingOrdersItemNames = _marketDb.GetBuyOrders().Select(x => x.EngItemName);
                foreach (var itemName in existingOrdersItemNames)
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

    
}