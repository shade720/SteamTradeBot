﻿using Serilog;
using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.OrderAggregate;

namespace SteamTradeBot.Backend.Application.Factories;

public sealed class ItemsNamesProvider
{
    private readonly IItemsTableApi _api;
    private readonly IConfigurationService _configurationService;
    private readonly IOrdersRepository _ordersRepository;

    internal ItemsNamesProvider(
        IItemsTableApi api, 
        IConfigurationService configurationService,
        IOrdersRepository ordersRepository)
    {
        _api = api;
        _configurationService = configurationService;
        _ordersRepository = ordersRepository;
    }

    internal async IAsyncEnumerable<string> GetNamesAsync()
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