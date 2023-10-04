using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.ItemModel;
using System.Collections.Generic;
using System.Linq;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Factories;

public sealed class ItemsNamesProvider
{
    private readonly IItemsTableApi _api;
    private readonly IConfigurationService _configurationService;
    private readonly OrdersRepository _ordersRepository;

    public ItemsNamesProvider(
        IItemsTableApi api, 
        IConfigurationService configurationService,
        OrdersRepository ordersRepository)
    {
        _api = api;
        _configurationService = configurationService;
        _ordersRepository = ordersRepository;
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
                    _configurationService.MinPrice,
                    _configurationService.MaxPrice,
                    _configurationService.SalesPerDay * 7,
                    _configurationService.ItemListSize);
            }
            catch
            {
                Log.Logger.Error("Can't get items names list. ");
                loadedItemNamesList = new List<string>();
            }
            
            var existingOrders = await _ordersRepository.GetOrdersAsync(_configurationService.ApiKey, OrderType.BuyOrder);
            foreach (var existingOrderName in existingOrders.Select(x => x.EngItemName))
            {
                if (loadedItemNamesList.Contains(existingOrderName))
                    loadedItemNamesList.Remove(existingOrderName);
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