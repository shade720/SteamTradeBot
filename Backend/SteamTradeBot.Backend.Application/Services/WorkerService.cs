using SteamTradeBot.Backend.Application.Factories;
using SteamTradeBot.Backend.Domain.Abstractions;

namespace SteamTradeBot.Backend.Application.Services;

public class WorkerService
{
    private readonly ItemsNamesProvider _itemsNamesProvider;
    private readonly ItemPageFactory _itemPageFactory;
    private readonly SolutionsFactory _solutionsFactory;
    private readonly ITradingEventHandler _tradingEventHandler;

    private CancellationTokenSource? _cancellationTokenSource;

    public WorkerService
    (
        ItemsNamesProvider itemsNamesProvider,
        ItemPageFactory itemPageFactory,
        SolutionsFactory solutionsFactory,
        ITradingEventHandler tradingEventHandler)
    {
        _itemsNamesProvider = itemsNamesProvider;
        _itemPageFactory = itemPageFactory;
        _solutionsFactory = solutionsFactory;
        _tradingEventHandler = tradingEventHandler;
    }

    public async Task StartAsync()
    {
        var task = Task.Run(WorkerLoop);
        await Task.CompletedTask;
    }

    private async Task WorkerLoop()
    {
        await _tradingEventHandler.OnTradingStartedAsync();
        _cancellationTokenSource = new CancellationTokenSource();
        await foreach (var name in _itemsNamesProvider.GetNamesAsync())
        {
            if (_cancellationTokenSource.IsCancellationRequested)
                break;
            try
            {
                var itemPage = await _itemPageFactory.CreateAsync(name);
                await _tradingEventHandler.OnItemAnalyzingAsync(itemPage);

                var solution = await _solutionsFactory.GetSolutionAsync(itemPage);
                if (solution is null)
                    continue;
                await solution.PerformAsync(itemPage);
            }
            catch (Exception e)
            {
                await _tradingEventHandler.OnErrorAsync(e);
            }
        }
        await _tradingEventHandler.OnTradingStoppedAsync();
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