using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using SteamTradeBot.Backend.Models.Abstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Factories;

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

    public async Task StartAsync()
    {
        var task = Task.Run(WorkerLoop);
        await Task.CompletedTask;
    }

    private async Task WorkerLoop()
    {
        await _stateManager.OnTradingStartedAsync();
        _cancellationTokenSource = new CancellationTokenSource();
        await foreach (var name in _itemsNamesProvider.GetNamesAsync())
        {
            if (_cancellationTokenSource.IsCancellationRequested)
                break;
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
                Log.Logger.Error("Item skipped due to error -> \r\nMessage: {0}, StackTrace: {1}", 
                    e.Message, e.StackTrace);
                await _stateManager.OnErrorAsync(e);
            }
        }
        await _stateManager.OnTradingStoppedAsync();
    }

    public async Task StopAsync()
    {
        if (_cancellationTokenSource is null)
        {
            throw new Exception("Worker is already stopped!");
        }
        _cancellationTokenSource.Cancel();
        await Task.CompletedTask;
    }
}