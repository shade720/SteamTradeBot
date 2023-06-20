using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.DataAccessLayer;
using SteamTradeBot.Backend.BusinessLogicLayer.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer;

public class TradeBot
{
    #region Public

    public TradeBot
    (
        IConfiguration configuration,
        IDbContextFactory<MarketDataContext> factory)
    {
        _configuration = configuration;
        _database = new DatabaseClient(factory);
        _steamApi = new SteamAPI();
    }

    public void StartTrading()
    {
        if (!CheckConfigurationIntegrity())
        {
            Log.Error("Configuration is corrupted. Trading not started!");
            return;
        }
        if (_worker is not null) 
            return;
        var workingSet = InitializePipeline();
        _worker = new Worker(workingSet);
        _worker.OnItemUpdate += OnItemUpdate;
        _worker.StartWork();
    }

    public void StopTrading()
    {
        _worker.OnItemUpdate -= OnItemUpdate;
        _worker?.StopWork();
    }

    public void ClearBuyOrders()
    {
        var itemsWithPurchaseOrders = _database.GetItems().Where(item => item.IsTherePurchaseOrder || item.ItemPriority == Priority.BuyOrder);
        foreach (var item in itemsWithPurchaseOrders)
        {
            item.CancelBuyOrder();
        }
    }

    public void RefreshWorkingSet()
    {
        _worker.OnItemUpdate -= OnItemUpdate;
        _worker?.StopWork();
        _database.ClearItems();
        var items = InitializePipeline();
        _worker = new Worker(items);
        _worker.OnItemUpdate += OnItemUpdate;
        _worker.StartWork();
    }

    public void LogIn(string login, string password, string token, string secret)
    {
        _steamApi.LogIn(login, password, token, secret);
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

    #endregion

    #region Private

    private readonly SteamAPI _steamApi;
    private readonly IConfiguration _configuration;
    private readonly DatabaseClient _database;

    private Worker _worker;

    private void OnItemUpdate(Item item) => _database.UpdateItem(item);

    private List<Item> InitializePipeline()
    {
        Log.Information("Initializing pipeline...");
        var pipeline = new List<Item>();
        var savedItems = _database.GetItems();
        if (savedItems.Any())
        {
            Log.Information("Local pipeline loaded");
            pipeline.AddRange(savedItems.Select(savedItem => savedItem.ConfigureServiceProperties(_configuration, _steamApi)));
        }
        else
        {
            Log.Information("Local pipeline not found");
            foreach (var newItem in GetItemNames().Select(itemName => new Item { EngItemName = itemName }.ConfigureServiceProperties(_configuration, _steamApi)))
            {
                _database.AddItem(newItem);
                pipeline.Add(newItem);
            }
        }
        Log.Information("Pipeline initialized");
        return pipeline;
    }

    private IEnumerable<string> GetItemNames()
    {
        Log.Information("Load pipeline from skins-table.xyz...");
        return _steamApi.GetItemNamesList(
            double.Parse(_configuration["StartPrice"]!, NumberStyles.Any, CultureInfo.InvariantCulture),
            double.Parse(_configuration["EndPrice"]!, NumberStyles.Any, CultureInfo.InvariantCulture),
            int.Parse(_configuration["Sales"]!) * 7,
            int.Parse(_configuration["PipelineSize"]!));
    }

    private bool CheckConfigurationIntegrity()
    {
        Log.Information("Check configuration integrity...");
        if (int.TryParse(_configuration["BuyQuantity"], out _) &&
            int.TryParse(_configuration["Sales"], out _) &&
            _configuration["SteamUserId"] is not null &&
            int.TryParse(_configuration["ListingFindRange"], out _) &&
            int.TryParse(_configuration["AnalysisPeriod"], out _) &&
            double.TryParse(_configuration["PriceRangeToCancel"], NumberStyles.Any, CultureInfo.InvariantCulture, out _) &&
            double.TryParse(_configuration["AvgPrice"], NumberStyles.Any, CultureInfo.InvariantCulture, out _) &&
            double.TryParse(_configuration["Trend"], NumberStyles.Any, CultureInfo.InvariantCulture, out _) &&
            double.TryParse(_configuration["SteamCommission"], NumberStyles.Any, CultureInfo.InvariantCulture, out _) &&
            double.TryParse(_configuration["RequiredProfit"], NumberStyles.Any, CultureInfo.InvariantCulture, out _)
            )
        {
            Log.Information("Check configuration integrity -> OK");
            return true;
        }
        Log.Fatal($"Configuration error");
        return false;
    }

    #endregion
}