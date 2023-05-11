using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Models;

public class Worker
{
    #region Public

    public delegate void UpdateItem(Item item);
    public event UpdateItem OnItemUpdate;

    public Worker(List<Item> workingSet)
    {
        if (workingSet.Count == 0)
        {
            throw new ArgumentException("Items list was empty!");
        }
        _itemsPipeline = new PriorityQueue<Item, Priority>();
        foreach (var item in workingSet)
        {
            _itemsPipeline.Enqueue(item, item.ItemPriority);
        }
    }

    public void StartWork()
    {
        Log.Information("Starting worker...");
        if (IsWorking)
        {
            Log.Warning("Worker already started!");
            return;
        }

        _cancellationTokenSource = new CancellationTokenSource();
        _processedItems = new List<(Item, Priority)>();

        Task.Run(ProcessPipelineLoop, _cancellationTokenSource.Token);
        Log.Information("Worker started!");
    }

    public void StopWork()
    {
        if (!IsWorking)
        {
            Log.Warning("Worker already stopped!");
            return;
        }
        _cancellationTokenSource.Cancel();
    }

    #endregion

    #region Private
    private bool IsWorking => _cancellationTokenSource is not null && !_cancellationTokenSource.IsCancellationRequested;

    private PriorityQueue<Item, Priority> _itemsPipeline;
    private List<(Item, Priority)> _processedItems;

    private CancellationTokenSource _cancellationTokenSource;

    private void ProcessPipelineLoop()
    {
        Log.Information("Pipeline processing has started");
        while (!_cancellationTokenSource.Token.IsCancellationRequested)
        {
            var item = _itemsPipeline.Dequeue();
            var analyzedItem = AnalyzeItem(item);

            if (analyzedItem is null)
                continue;
            _processedItems.Add((analyzedItem, analyzedItem.ItemPriority));
            OnItemUpdate?.Invoke(analyzedItem);

            if (_itemsPipeline.Count == 0)
                RestartPipeline();
        }
        Log.Information("Pipeline processing has ended");
    }

    private static Item AnalyzeItem(Item item)
    {
        try
        {
            item.CollectItemData();

            if (item.IsProfitable())
                item.Buy();

            if (item.IsBuyOrderObsolete())
                item.CancelBuyOrder();

            if (item.IsBuyOrderSatisfied())
                item.Sell();
        }
        catch (Exception e)
        {
            Log.Error($"The item was skipped due to an error ->\r\nMessage: {e.Message}\r\nStack trace: {e.StackTrace}");
            return null;
        }
        return item;
    }

    private void RestartPipeline()
    {
        _itemsPipeline = new PriorityQueue<Item, Priority>(_processedItems);
        _processedItems.Clear();
    }

    #endregion
}

