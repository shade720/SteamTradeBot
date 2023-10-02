﻿using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Factories;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Services;

public class WorkerService
{
    private readonly ItemsNamesProvider _itemsNamesProvider;
    private readonly ItemPageFactory _itemPageFactory;
    private readonly SolutionsFactory _solutionsFactory;
    private readonly IStateService _stateService;

    private CancellationTokenSource? _cancellationTokenSource;

    public WorkerService
    (
        ItemsNamesProvider itemsNamesProvider,
        ItemPageFactory itemPageFactory,
        SolutionsFactory solutionsFactory,
        IStateService stateService)
    {
        _itemsNamesProvider = itemsNamesProvider;
        _itemPageFactory = itemPageFactory;
        _solutionsFactory = solutionsFactory;
        _stateService = stateService;
    }

    public async Task StartAsync()
    {
        var task = Task.Run(WorkerLoop);
        await Task.CompletedTask;
    }

    private async Task WorkerLoop()
    {
        await _stateService.OnTradingStartedAsync();
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
                await _stateService.OnErrorAsync(e);
            }
        }
        await _stateService.OnTradingStoppedAsync();
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