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

    public delegate void OnWorkingSetFullyAnalyzed();
    public event OnWorkingSetFullyAnalyzed? OnWorkingSetFullyAnalyzedEvent;

    public Worker(List<Item> workingSet)
    {
        if (workingSet.Count == 0)
        {
            throw new ArgumentException("Items list was empty!");
        }
        _itemsPipeline = new Queue<Item>();
        foreach (var item in workingSet)
            _itemsPipeline.Enqueue(item);
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

    public bool IsWorking => _cancellationTokenSource is not null && !_cancellationTokenSource.IsCancellationRequested;
    private readonly Queue<Item> _itemsPipeline;
    private CancellationTokenSource? _cancellationTokenSource;

    private void ProcessPipelineLoop()
    {
        if (_cancellationTokenSource is null)
            throw new ArgumentException("Cancellation token was null!");

        Log.Information("Pipeline processing has started");
        while (!_cancellationTokenSource.IsCancellationRequested)
        {
            var item = _itemsPipeline.Dequeue();
            AnalyzeItem(item);
            if (_itemsPipeline.Count == 0)
                break;
        }
        Log.Information("Pipeline processing has ended");
        if (!_cancellationTokenSource.IsCancellationRequested)
            OnWorkingSetFullyAnalyzedEvent?.Invoke();
    }

    private void AnalyzeItem(Item item)
    {
        try
        {
            item.CollectItemData(out var balance);
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
            Log.Error("The item was skipped due to an error ->\r\nMessage: {0}\r\nStack trace: {1}", e.Message, e.StackTrace);
            OnErrorEvent?.Invoke(e);
        }
    }

    #endregion
}

