using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Factories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.Services;

public class WorkerService
{
    private readonly ItemsNamesProvider _itemsNamesProvider;
    private readonly ItemPageFactory _itemPageFactory;
    private readonly SolutionsFactory _solutionsFactory;

    private CancellationTokenSource? _cancellationTokenSource;

    public WorkerService
    (
        ItemsNamesProvider itemsNamesProvider,
        ItemPageFactory itemPageFactory,
        SolutionsFactory solutionsFactory)
    {
        _itemsNamesProvider = itemsNamesProvider;
        _itemPageFactory = itemPageFactory;
        _solutionsFactory = solutionsFactory;
    }

    public async Task Start()
    {
        await Task.Run(WorkerLoop);
    }

    private void WorkerLoop()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        Log.Information("Worker has started");
        foreach (var name in _itemsNamesProvider.Names)
        {
            try
            {
                var itemPage = _itemPageFactory.Create(name);
                var solution = _solutionsFactory.GetSolution(itemPage);
                solution?.Perform(itemPage);
            }
            catch (Exception e)
            {
                Log.Logger.Error("Item skipped due to error -> \r\nMessage: {0}, StackTrace: {1}", e.Message, e.StackTrace);
                continue;
            }

            if (_cancellationTokenSource.IsCancellationRequested) 
                break;
        }
        Log.Information("Worker has stopped");
    }

    public async Task Stop()
    {
        if (_cancellationTokenSource is null)
        {
            throw new Exception("Worker is already stopped!");
        }
        await Task.Run(_cancellationTokenSource.Cancel);
    }
}