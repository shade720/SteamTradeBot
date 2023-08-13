using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Factories;
using System;
using System.Threading;
using System.Threading.Tasks;
using SteamTradeBot.Backend.Models.Abstractions;

namespace SteamTradeBot.Backend.Services;

public class WorkerService
{
    private readonly ItemsNamesProvider _itemsNamesProvider;
    private readonly ItemPageFactory _itemPageFactory;
    private readonly SolutionsFactory _solutionsFactory;
    private readonly IStateManager _stateManager;

    private CancellationTokenSource? _cancellationTokenSource;

    public WorkerService
    (
        ItemsNamesProvider itemsNamesProvider,
        ItemPageFactory itemPageFactory,
        SolutionsFactory solutionsFactory,
        IStateManager stateManager)
    {
        _itemsNamesProvider = itemsNamesProvider;
        _itemPageFactory = itemPageFactory;
        _solutionsFactory = solutionsFactory;
        _stateManager = stateManager;
    }

    public async Task Start()
    {
        await Task.Run(WorkerLoop);
    }

    private async Task WorkerLoop()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        Log.Information("Worker has started");
        _stateManager.OnTradingStarted();
        await foreach (var name in _itemsNamesProvider.GetNamesAsync())
        {
            try
            {
                var itemPage = await _itemPageFactory.CreateAsync(name);
                var solution = await _solutionsFactory.GetSolutionAsync(itemPage);
                if (solution is null)
                    continue;
                await solution.PerformAsync(itemPage);
            }
            catch (Exception e)
            {
                Log.Logger.Error("Item skipped due to error -> \r\nMessage: {0}, StackTrace: {1}", e.Message, e.StackTrace);
                await _stateManager.OnErrorAsync(e);
                continue;
            }

            if (_cancellationTokenSource.IsCancellationRequested) 
                break;
        }
        _stateManager.OnTradingStopped();
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