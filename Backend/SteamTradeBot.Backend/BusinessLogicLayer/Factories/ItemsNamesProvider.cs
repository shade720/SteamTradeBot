using Serilog;
using SteamTradeBot.Backend.DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Factories;

public sealed class ItemsNamesProvider
{
    private readonly IItemsTableApi _api;
    private readonly IConfigurationManager _configurationManager;
    private readonly OrdersDbAccess _ordersDb;

    public ItemsNamesProvider(
        IItemsTableApi api, 
        IConfigurationManager configurationManager, 
        OrdersDbAccess ordersDb)
    {
        _api = api;
        _configurationManager = configurationManager;
        _ordersDb = ordersDb;
    }

    public async IAsyncEnumerable<string> GetNamesAsync()
    {
        while (true)
        {
            Log.Information("Load items names for analysis...");
            List<string> loadedItemNamesList;
            try
            {
                loadedItemNamesList = await _api.GetItemNamesListAsync(
                    _configurationManager.MinPrice,
                    _configurationManager.MaxPrice,
                    _configurationManager.SalesPerDay * 7,
                    _configurationManager.ItemListSize);
            }
            catch
            {
                Log.Logger.Error("Can't get items names list. ");
                loadedItemNamesList = new List<string>();
            }
            
            var existingOrders = await _ordersDb.GetOrdersAsync(_configurationManager.ApiKey, OrderType.BuyOrder);
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