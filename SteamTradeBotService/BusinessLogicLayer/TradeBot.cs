using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using SteamTradeBotService.BusinessLogicLayer.Database;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using Serilog;

namespace SteamTradeBotService.BusinessLogicLayer;

public class TradeBot
{
    private readonly SteamAPI _steamApi;
    private readonly Worker _worker;
    private readonly Reporter _reporter;

    public TradeBot
    (
        IConfiguration configuration, 
        IDbContextFactory<MarketDataContext> factory)
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

    public void ClearBuyOrders()
    {
        _worker.ClearBuyOrders();
    }

    public void RefreshItemsList()
    {
        //_steamApi.GetItemNamesList();
    }

    public void LogIn(string login, string password, string token)
    {
        _steamApi.LogIn(login, password, token);
    }

    public void LogOut()
    {
        _steamApi.LogOut();
    }

    public bool SetConfiguration(string settingsJsonString)
    {
        var appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        var appSettingsJson = File.ReadAllText(appSettingsPath);

        var jsonSettings = new JsonSerializerSettings();
        jsonSettings.Converters.Add(new ExpandoObjectConverter());
        jsonSettings.Converters.Add(new StringEnumConverter());

        dynamic? oldConfig = JsonConvert.DeserializeObject<ExpandoObject>(appSettingsJson, jsonSettings);
        dynamic? newConfig = JsonConvert.DeserializeObject<ExpandoObject>(settingsJsonString, jsonSettings);

        if (oldConfig is null)
        {
            Log.Logger.Error("Cannot deserialize appsettings.json file");
            return false;
        }
        if (newConfig is null)
        {
            Log.Logger.Error("Cannot deserialize new settings file");
            return false;
        }

        var newConfigDict = (IDictionary<string, object>)newConfig;
        var oldConfigDict = (IDictionary<string, object>)oldConfig;

        foreach (var pair in newConfigDict)
        {
            if (oldConfigDict.ContainsKey(pair.Key))
                oldConfigDict[pair.Key] = pair.Value;
        }

        var newAppSettingsJson = JsonConvert.SerializeObject(oldConfig, Formatting.Indented, jsonSettings);
        File.WriteAllText(appSettingsPath, newAppSettingsJson);
        return true;
    }
}