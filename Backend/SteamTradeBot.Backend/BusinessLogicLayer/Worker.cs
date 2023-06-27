using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace SteamTradeBot.Backend.BusinessLogicLayer;

public class Worker
{
    #region Public

    public delegate void OnItemAnalyzed(Item item, double balance);
    public event OnItemAnalyzed? OnItemAnalyzedEvent;

    public delegate void OnItemBought(Item item);
    public event OnItemBought? OnItemBoughtEvent;

    public delegate void OnItemSold(Item item);
    public event OnItemSold? OnItemSoldEvent;

    public delegate void OnItemCanceled(Item item);
    public event OnItemCanceled? OnItemCanceledEvent;

    public delegate void OnError(Exception exception);
    public event OnError? OnErrorEvent;

    public delegate void OnWarning(Exception exception);
    public event OnWarning? OnWarningEvent;

    public Worker(List<Item> workingSet)
    {
        if (workingSet.Count == 0)
        {
            throw new ArgumentException("Items list was empty!");
        }
        _itemsPipeline = new PriorityQueue<Item, Item.Priority>();
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
        _processedItems = new List<(Item, Item.Priority)>();

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
        _cancellationTokenSource?.Cancel();
        Log.Information("Worker stopped!");
    }

    #endregion

    #region Private
    private bool IsWorking => _cancellationTokenSource is not null && !_cancellationTokenSource.IsCancellationRequested;

    private PriorityQueue<Item, Item.Priority> _itemsPipeline;
    private List<(Item, Item.Priority)>? _processedItems;

    private CancellationTokenSource? _cancellationTokenSource;

    private void ProcessPipelineLoop()
    {
        if (_cancellationTokenSource is null)
            throw new ArgumentException("Cancellation token was null!");
        if (_processedItems is null)
            throw new ArgumentException("Processed items list was is null!");

        Log.Information("Pipeline processing has started");
        while (!_cancellationTokenSource.IsCancellationRequested)
        {
            var item = _itemsPipeline.Dequeue();
            var analyzedItem = AnalyzeItem(item);

            if (analyzedItem is null)
                continue;
            _processedItems.Add((analyzedItem, analyzedItem.ItemPriority));

            if (_itemsPipeline.Count == 0)
                RestartPipeline();
        }
        Log.Information("Pipeline processing has ended");
    }

    private Item? AnalyzeItem(Item item)
    {
        try
        {
            var balance = item.CollectItemData();
            OnItemAnalyzedEvent?.Invoke(item, balance);
            if (item.IsProfitable(balance))
            {
                item.Buy();
                OnItemBoughtEvent?.Invoke(item);
            }
            if (item.IsBuyOrderObsolete())
            {
                item.CancelBuyOrder();
                OnItemCanceledEvent?.Invoke(item);
            }
            if (item.IsBuyOrderSatisfied())
            {
                item.Sell();
                OnItemSoldEvent?.Invoke(item);
            }
        }
        catch (Exception e)
        {
            Log.Error($"The item was skipped due to an error ->\r\nMessage: {e.Message}\r\nStack trace: {e.StackTrace}");
            OnErrorEvent?.Invoke(e);
            return null;
        }
        return item;
    }

    private void RestartPipeline()
    {
        if (_processedItems is null)
            throw new ArgumentException("Processed items list was is null!");
        _itemsPipeline = new PriorityQueue<Item, Item.Priority>(_processedItems);
        _processedItems.Clear();
    }

    #endregion
}

