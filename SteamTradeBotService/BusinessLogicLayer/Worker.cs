using Microsoft.Extensions.Configuration;
using Serilog;
using SteamTradeBotService.BusinessLogicLayer.Database;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SteamTradeBotService.BusinessLogicLayer;

public class Worker
{
    private readonly DatabaseClient _database;
    private readonly SteamAPI _steamApi;
    private readonly IConfiguration _configuration;

    private PriorityQueue<Item, Priority> _itemsPipeline;
    private List<(Item, Priority)> _processedItems;

    private CancellationTokenSource _cancellationTokenSource;

    public Worker
    (
        SteamAPI steamApi,
        DatabaseClient database,
        IConfiguration configuration)
    {
        _steamApi = steamApi;
        _database = database;
        _configuration = configuration;
    }

    public void StartWork()
    {
        Log.Information("Starting worker...");
        if (_cancellationTokenSource is not null && !_cancellationTokenSource.IsCancellationRequested)
        {
            Log.Warning("Worker already started!");
            return;
        }

        if (!CheckConfigurationIntegrity())
            return;

        _itemsPipeline = InitializePipeline();

        if (_itemsPipeline.Count == 0)
        {
            Log.Error("Worker is not started. Pipeline is empty!");
            return;
        }

        _cancellationTokenSource = new CancellationTokenSource();
        _processedItems = new List<(Item, Priority)>();

        Task.Run(ProcessPipeline, _cancellationTokenSource.Token);
        Log.Information("Worker started!");
    }

    private void ProcessPipeline()
    {
        Log.Information("Pipeline processing has started");
        while (!_cancellationTokenSource.Token.IsCancellationRequested)
        {
            var item = _itemsPipeline.Dequeue();

            item.CollectItemData();

            if (item.IsProfitable())
                item.Buy();

            if (item.IsBuyOrderObsolete())
                item.CancelBuyOrder();

            if (item.IsBuyOrderSatisfied())
                item.Sell();

            _processedItems.Add((item, item.ItemPriority));
            _database.UpdateItem(item);

            if (_itemsPipeline.Count == 0)
                RestartPipeline();
        }
        Log.Information("Pipeline processing has ended");
    }

    public void StopWork()
    {
        _cancellationTokenSource.Cancel();
    }

    private void RestartPipeline()
    {
        _itemsPipeline = new PriorityQueue<Item, Priority>(_processedItems);
        _processedItems.Clear();
    }

    private PriorityQueue<Item, Priority> InitializePipeline()
    {
        Log.Information("Initializing pipeline...");
        var pipeline = new PriorityQueue<Item, Priority>();
        var savedItems = _database.GetItems();
        if (savedItems.Any())
        {
            Log.Information("Local pipeline loaded");
            foreach (var savedItem in savedItems)
            {
                pipeline.Enqueue(savedItem.SetExecutionProperties(_configuration, _steamApi, _database), savedItem.IsBeingPurchased ? Priority.BuyOrder : Priority.ForReview);
            }
        }
        else
        {
            Log.Information("Local pipeline not found");
            foreach (var newItem in GetItemNames().Select(itemName => new Item { EngItemName = itemName }.SetExecutionProperties(_configuration, _steamApi, _database)))
            {
                _database.AddItem(newItem);
                pipeline.Enqueue(newItem, Priority.ForReview);
            }
        }
        Log.Information("Pipeline initialized");
        return pipeline;
    }

    private IEnumerable<string> GetItemNames()
    {
        Log.Information("Load pipeline from skins-table.xyz...");
        return _steamApi.GetItemNamesList(double.Parse(_configuration["StartPrice"]!, NumberStyles.Any, CultureInfo.InvariantCulture), double.Parse(_configuration["EndPrice"]!, NumberStyles.Any, CultureInfo.InvariantCulture), int.Parse(_configuration["Sales"]!) * 7, int.Parse(_configuration["PipelineSize"]!));
    }

    private bool CheckConfigurationIntegrity()
    {
        Log.Information("Check configuration integrity...");
        try
        {
            if (!int.TryParse(_configuration["BuyQuantity"], out _)) return false;
            if (!int.TryParse(_configuration["Sales"], out _)) return false;
            if (_configuration["SteamUserId"] is null) return false;
            if (!int.TryParse(_configuration["ListingFindRange"], out _)) return false;
            if (!int.TryParse(_configuration["AnalysisPeriod"], out _)) return false;
            if (!double.TryParse(_configuration["PriceRangeToCancel"], NumberStyles.Any, CultureInfo.InvariantCulture, out _)) return false;
            if (!double.TryParse(_configuration["AvgPrice"], NumberStyles.Any, CultureInfo.InvariantCulture, out _)) return false;
            if (!double.TryParse(_configuration["Trend"], NumberStyles.Any, CultureInfo.InvariantCulture, out _)) return false;
        }
        catch (Exception e)
        {
            Log.Fatal($"Configuration error: {e.Message}");
            return false;
        }
        Log.Information("Check configuration integrity -> OK");
        return true;
    }

    public void ClearLots()
    {
        foreach (var item in _itemsPipeline.UnorderedItems.Where(x => x.Priority == Priority.BuyOrder))
        {
            item.Element.CancelBuyOrder();
        }
    }
}

