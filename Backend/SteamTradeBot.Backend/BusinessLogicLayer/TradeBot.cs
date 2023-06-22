using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.DataAccessLayer;
using SteamTradeBot.Backend.BusinessLogicLayer.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer;

public class TradeBot : IDisposable
{
    #region Public

    public TradeBot
    (
        IConfiguration configuration, 
        IDbContextFactory<MarketDataContext> marketContextFactory,
        IDbContextFactory<HistoryDataContext> historyContextFactory,
        ServiceState state)
    {
        _configuration = configuration;
        _state = state;
        _db = new DbAccess(marketContextFactory, historyContextFactory);
        _steamApi = new SteamAPI();
        _stopwatch = new Stopwatch();
        Log.Logger.Information("TradeBotSingleton created!");
        _state.Events.Add($"{DateTime.UtcNow}-EngItemName-Sold-{45.1244}-{60.31}");
        _state.Events.Add($"{DateTime.UtcNow}-EngItemName-Bought-{45.1244}");
    }

    #region Trading

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
        _worker.OnItemAnalyzedEvent += OnItemAnalyzed;
        _worker.OnItemCanceledEvent += OnItemCanceled;
        _worker.OnItemBoughtEvent += OnItemBought;
        _worker.OnItemSoldEvent += OnItemSold;
        _worker.OnErrorEvent += OnError;
        _worker.StartWork();
        _stopwatch.Start();
        _state.WorkingState = ServiceWorkingState.Up;
    }

    public void StopTrading()
    {
        if (_worker is null)
            throw new ApplicationException("Worker was null (StopTrading)");
        _worker.OnItemAnalyzedEvent -= OnItemAnalyzed;
        _worker.OnItemCanceledEvent -= OnItemCanceled;
        _worker.OnItemBoughtEvent -= OnItemBought;
        _worker.OnItemSoldEvent -= OnItemSold;
        _worker.OnErrorEvent -= OnError;
        _worker?.StopWork();
        _stopwatch.Stop();
        _stopwatch.Reset();
        _state.WorkingState = ServiceWorkingState.Down;
    }

    public void ClearBuyOrders()
    {
        var itemsWithPurchaseOrders = _db.GetItems().Where(item => item.IsTherePurchaseOrder || item.ItemPriority == Priority.BuyOrder);
        foreach (var item in itemsWithPurchaseOrders)
        {
            item.CancelBuyOrder();
        }
    }

    public void RefreshWorkingSet()
    {
        if (_worker is null)
            throw new ApplicationException("Worker was null (RefreshWorkingSet)");
        _worker.OnItemAnalyzedEvent -= OnItemAnalyzed;
        _worker.OnItemCanceledEvent -= OnItemCanceled;
        _worker.OnItemBoughtEvent -= OnItemBought;
        _worker.OnItemSoldEvent -= OnItemSold;
        _worker.OnErrorEvent -= OnError;
        _worker?.StopWork();
        _db.ClearItems();
        var items = InitializePipeline();
        _worker = new Worker(items);
        _worker.OnItemAnalyzedEvent += OnItemAnalyzed;
        _worker.OnItemCanceledEvent += OnItemCanceled;
        _worker.OnItemBoughtEvent += OnItemBought;
        _worker.OnItemSoldEvent += OnItemSold;
        _worker.OnErrorEvent += OnError;
        _worker.StartWork();
    }

    #endregion

    #region Auth

    public void LogIn(string login, string password, string token, string secret)
    {
        _steamApi.LogIn(login, password, token, secret);
    }

    public void LogOut()
    {
        _steamApi.LogOut();
    }

    #endregion

    #region Configuration

    private const string SettingsFileName = "appsettings.json";
    private static readonly string ConfigurationPath = Path.Combine(Environment.CurrentDirectory, SettingsFileName);

    public static bool SetConfiguration(string newSettings)
    {
        var currentSettings = File.ReadAllText(ConfigurationPath);

        var jsonSettings = new JsonSerializerSettings();
        jsonSettings.Converters.Add(new ExpandoObjectConverter());
        jsonSettings.Converters.Add(new StringEnumConverter());

        dynamic? currentConfig = JsonConvert.DeserializeObject<ExpandoObject>(currentSettings, jsonSettings);
        dynamic? newConfig = JsonConvert.DeserializeObject<ExpandoObject>(newSettings, jsonSettings);

        if (currentConfig is null)
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
        var currentConfigDict = (IDictionary<string, object>)currentConfig;

        foreach (var pair in newConfigDict)
        {
            if (currentConfigDict.ContainsKey(pair.Key))
                currentConfigDict[pair.Key] = pair.Value;
        }

        var updatedSettings = JsonConvert.SerializeObject(currentConfig, Formatting.Indented, jsonSettings);
        File.WriteAllText(ConfigurationPath, updatedSettings);
        return true;
    }

    #endregion

    #region Logs

    private static readonly string LogsPath = Path.Combine(Environment.CurrentDirectory, "Logs");

    public static string GetLogs()
    {
        var logFiles = Directory.GetFiles(LogsPath);
        var sb = new StringBuilder();
        foreach (var file in logFiles)
        {
            using var sr = new StreamReader(new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            sb.Append(sr.ReadToEnd());
        }
        return sb.ToString();
    }

    #endregion

    #region State

    public ServiceState GetServiceState()
    {
        var serviceStateCopy = new ServiceState
        {
            WorkingState = _state.WorkingState,
            ItemsAnalyzed = _state.ItemsAnalyzed,
            ItemsBought = _state.ItemsBought,
            ItemsSold = _state.ItemsSold,
            ItemCanceled = _state.ItemCanceled,
            Errors = _state.Errors,
            Warnings = _state.Warnings,
            Events = new List<string>(_state.Events),
            Uptime = _stopwatch.Elapsed,
        };
        _state.Events.Clear();
        return serviceStateCopy;
    }

    #endregion

    public void Dispose()
    {
        Log.Logger.Information("Disposing trade bot singleton...");
        _steamApi?.Dispose();
        Log.Logger.Information("Trade bot singleton disposed!");
    }
    #endregion

    #region Private

    private readonly SteamAPI _steamApi;
    private readonly IConfiguration _configuration;
    private readonly ServiceState _state;
    private readonly DbAccess _db;
    private readonly Stopwatch _stopwatch;

    private Worker? _worker;

    private List<Item> InitializePipeline()
    {
        Log.Information("Initializing pipeline...");
        var pipeline = new List<Item>();
        var savedItems = _db.GetItems();
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
                _db.AddItem(newItem);
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
            double.Parse(_configuration["MinPrice"]!, NumberStyles.Any, CultureInfo.InvariantCulture),
            double.Parse(_configuration["MaxPrice"]!, NumberStyles.Any, CultureInfo.InvariantCulture),
            int.Parse(_configuration["SalesPerWeek"]!) * 7,
            int.Parse(_configuration["ItemListSize"]!));
    }

    private bool CheckConfigurationIntegrity()
    {
        Log.Information("Check configuration integrity...");
        if (int.TryParse(_configuration["OrderQuantity"], out _) &&
            int.TryParse(_configuration["SalesPerWeek"], out _) &&
            _configuration["SteamUserId"] is not null &&
            int.TryParse(_configuration["ListingFindRange"], out _) &&
            int.TryParse(_configuration["AnalysisIntervalDays"], out _) &&
            double.TryParse(_configuration["FitPriceRange"], NumberStyles.Any, CultureInfo.InvariantCulture, out _) &&
            double.TryParse(_configuration["AveragePrice"], NumberStyles.Any, CultureInfo.InvariantCulture, out _) &&
            double.TryParse(_configuration["Trend"], NumberStyles.Any, CultureInfo.InvariantCulture, out _) &&
            double.TryParse(_configuration["SteamCommission"], NumberStyles.Any, CultureInfo.InvariantCulture, out _) &&
            double.TryParse(_configuration["RequiredProfit"], NumberStyles.Any, CultureInfo.InvariantCulture, out _))
        {
            Log.Information("Check configuration integrity -> OK");
            return true;
        }
        Log.Fatal("Configuration error");
        return false;
    }

    #region WorkerEventHandlers

    private void OnItemAnalyzed(Item item, double balance)
    {
        _db.UpdateItem(item);
        _db.AddNewStateInfo(new StateChangingEvent
        {
            Type = InfoType.ItemAnalyzed,
            CurrentBalance = balance,
            Time = DateTime.UtcNow,
            Info = item.EngItemName
        });
        _state.ItemsAnalyzed++;
    }

    private void OnError(Exception exception)
    {
        _db.AddNewStateInfo(new StateChangingEvent
        {
            Type = InfoType.Error,
            Time = DateTime.UtcNow,
            Info = $"Message: {exception.Message}, StackTrace: {exception.StackTrace}"
        });
        _state.Errors++;
    }

    private void OnItemSold(Item item)
    {
        _db.UpdateItem(item);
        _db.AddNewStateInfo(new StateChangingEvent
        {
            Type = InfoType.ItemSold,
            Time = DateTime.UtcNow,
            Info = item.EngItemName,
            SellPrice = item.SellPrice
        });
        _state.ItemsSold++;
        _state.Events.Add($"{DateTime.UtcNow}-{item.EngItemName}-Sold-{item.BuyPrice}-{item.SellPrice}");
    }

    private void OnItemBought(Item item)
    {
        _db.UpdateItem(item);
        _db.AddNewStateInfo(new StateChangingEvent
        {
            Type = InfoType.ItemBought,
            Time = DateTime.UtcNow,
            Info = item.EngItemName,
            BuyPrice = item.BuyPrice
        });
        _state.ItemsBought++;
        _state.Events.Add($"{DateTime.UtcNow}-{item.EngItemName}-Bought-{item.BuyPrice}");
    }

    private void OnItemCanceled(Item item)
    {
        _db.UpdateItem(item);
        _db.AddNewStateInfo(new StateChangingEvent
        {
            Type = InfoType.ItemCanceled,
            Time = DateTime.UtcNow,
            Info = item.EngItemName
        });
        _state.ItemCanceled++;
        _state.Events.Add($"{DateTime.UtcNow}-{item.EngItemName}-Canceled-{item.BuyPrice}");
    }

    #endregion

    #endregion
}