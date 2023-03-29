﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SteamTradeBotService.BusinessLogicLayer.Database;

namespace SteamTradeBotService.BusinessLogicLayer;

public class TradeBot
{
    private readonly SteamAPI _steamApi;
    private readonly Worker _worker;
    private readonly Reporter _reporter;

    public TradeBot(IConfiguration configuration, IDbContextFactory<MarketDataContext> factory)
    {
        var database = new DatabaseClient(factory);
        _steamApi = new SteamAPI();
        _reporter = new Reporter();
        _worker = new Worker(_steamApi, database, configuration);
    }

    public void StartTrading()
    {
        if (!_worker.IsWorking)
            _worker.StartWork();
    }

    public void StopTrading()
    {
        if (_worker.IsWorking)
            _worker.StopWork();
    }

    public void ClearLots()
    {
        _worker.ClearLots();
    }

    public void LoadItemsList()
    {
        //_steamApi.GetItemNamesList();
    }

    public void SetConfiguration()
    {
        throw new System.NotImplementedException();
    }

    public void LogIn(string login, string password, string token)
    {
        _steamApi.LogIn(login, password, token);
    }

    public void LogOut()
    {
        _steamApi.LogOut();
    }
}