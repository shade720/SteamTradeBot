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
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer;

public class TradeBot : IDisposable
{
    #region Public

    public TradeBot
    (
        IConfiguration configuration, 
        IDbContextFactory<MarketDataContext> marketContextFactory,
        ServiceState state)
    {
        _configuration = configuration;
        _state = state;
        _db = new DbAccess(marketContextFactory);
        _steamApi = new SteamAPI();
        _stopwatch = new Stopwatch();
        Log.Logger.Information("TradeBotSingleton created!");
    }

    #region Trading

    public void StartTrading()
    {
        if (!CheckConfigurationIntegrity())
        {
            throw new ApplicationException("Configuration is corrupted. Trading not started!");
        }
        if (_worker is not null)
            return;
        var workingSet = InitializePipeline();
        _worker = InitWorker(workingSet);
        _worker.StartWork();
        _stopwatch.Start();
        _state.WorkingState = ServiceState.ServiceWorkingState.Up;
    }

    public void StopTrading()
    {
        if (_worker is null)
            throw new ApplicationException("Worker was null (StopTrading).");
        ClearWorker(_worker);
        _stopwatch.Stop();
        _stopwatch.Reset();
        _state.WorkingState = ServiceState.ServiceWorkingState.Down;
    }

    public void ClearBuyOrders()
    {
        Log.Logger.Information("Start buy orders canceling...");
        var itemsWithPurchaseOrders = _db.GetOrders().Where(item => item.IsTherePurchaseOrder);
        foreach (var item in itemsWithPurchaseOrders)
        {
            item.CancelBuyOrder();
            Log.Logger.Information("Item {0} with buy price {1} was canceled", item.EngItemName, item.BuyPrice);
        }
        Log.Logger.Information("All buy orders have been canceled!");
    }

    public void ReinitializeWorker()
    {
        Log.Logger.Information("Start working set refreshing...");
        if ( _worker is not null)
            ClearWorker(_worker);
        var items = InitializePipeline();
        _worker = InitWorker(items);
        Log.Logger.Information("Working set have been refreshed!");
    }

    #endregion

    #region Auth

    public void LogIn(string login, string password, string token, string secret)
    {
        _state.IsLoggedIn = ServiceState.LogInState.Pending;
        _steamApi.LogIn(login, password, token, secret);
        _state.IsLoggedIn = ServiceState.LogInState.LoggedIn;
        _state.CurrentUser = login;
    }

    public void LogOut()
    {
        _steamApi.LogOut();
        _state.CurrentUser = string.Empty;
        _state.IsLoggedIn = ServiceState.LogInState.NotLoggedIn;
    }

    #endregion

    #region Configuration

    private const string SettingsFileName = "appsettings.json";
    private static readonly string ConfigurationPath = Path.Combine(Environment.CurrentDirectory, SettingsFileName);

    public void SetConfiguration(string newSettings)
    {
        Log.Logger.Information("Start settings update...");
        var currentSettings = File.ReadAllText(ConfigurationPath);

        var jsonSettings = new JsonSerializerSettings();
        jsonSettings.Converters.Add(new ExpandoObjectConverter());
        jsonSettings.Converters.Add(new StringEnumConverter());

        dynamic? currentConfig = JsonConvert.DeserializeObject<ExpandoObject>(currentSettings, jsonSettings);
        dynamic? newConfig = JsonConvert.DeserializeObject<ExpandoObject>(newSettings, jsonSettings);

        if (currentConfig is null)
        {
            Log.Logger.Error("Cannot deserialize appsettings.json file");
            return;
        }
        if (newConfig is null)
        {
            Log.Logger.Error("Cannot deserialize new settings file");
            return;
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

        if (!CheckConfigurationIntegrity())
        {
            File.WriteAllText(ConfigurationPath, currentSettings);
            Log.Logger.Information("Settings have not been updated!");
        }
        Log.Logger.Information("Settings have been updated!");
    }

    #endregion

    #region Logs

    private const string LogsPath = "Logs";

    public string GetLogs()
    {
        Log.Logger.Information("Provide server logs...");
        var logFiles = Directory.GetFiles(LogsPath);
        var sb = new StringBuilder();
        foreach (var file in logFiles)
        {
            using var sr = new StreamReader(new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            sb.Append(sr.ReadToEnd());
        }
        Log.Logger.Information("Server logs have been provided!");
        return sb.ToString();
    }

    #endregion

    #region State

    public ServiceState GetServiceState()
    {
        _state.WorkingState = _worker is null ? ServiceState.ServiceWorkingState.Down : ServiceState.ServiceWorkingState.Up;
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
            CurrentUser = _state.CurrentUser,
            IsLoggedIn = _state.IsLoggedIn,
        };
        _state.Events.Clear();
        return serviceStateCopy;
    }

    #endregion

    public void Dispose()
    {
        Log.Logger.Information("Disposing trade bot singleton...");
        if (_worker is not null)
            ClearWorker(_worker);
        _steamApi.Dispose();
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
        Log.Information("Load items for analysis....");
        var itemWithOrders = _db.GetOrders().Where(x => x.IsTherePurchaseOrder);
        var loadedItemList = GetItemNames()
            .Where(itemName => itemWithOrders.All(order => order.EngItemName != itemName))
            .Select(itemName => new Item {EngItemName = itemName}.ConfigureServiceProperties(_configuration, _steamApi));
        Log.Information("Pipeline initialized");
        return loadedItemList.ToList();
    }

    private Worker InitWorker(List<Item> items)
    {
        var worker = new Worker(items);
        worker.OnItemAnalyzedEvent += OnItemAnalyzed;
        worker.OnItemCanceledEvent += OnItemCanceled;
        worker.OnItemBoughtEvent += OnItemBought;
        worker.OnItemSoldEvent += OnItemSold;
        worker.OnErrorEvent += OnError;
        worker.OnWorkingSetFullyAnalyzedEvent += ReinitializeWorker;
        return worker;
    }

    private void ClearWorker(Worker worker)
    {
        worker.OnItemAnalyzedEvent -= OnItemAnalyzed;
        worker.OnItemCanceledEvent -= OnItemCanceled;
        worker.OnItemBoughtEvent -= OnItemBought;
        worker.OnItemSoldEvent -= OnItemSold;
        worker.OnErrorEvent -= OnError;
        worker.OnWorkingSetFullyAnalyzedEvent -= ReinitializeWorker;
        worker?.StopWork();
    }

    private IEnumerable<string> GetItemNames()
    {
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
        _db.AddNewEvent(new TradingEvent
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
        _db.AddNewEvent(new TradingEvent
        {
            Type = InfoType.Error,
            Time = DateTime.UtcNow,
            Info = $"Message: {exception.Message}, StackTrace: {exception.StackTrace}"
        });
        _state.Errors++;
    }

    private void OnItemSold(Item item)
    {
        _db.AddOrUpdateOrder(item);
        _db.AddNewEvent(new TradingEvent
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
        _db.AddOrUpdateOrder(item);
        _db.AddNewEvent(new TradingEvent
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
        _db.RemoveOrder(item);
        _db.AddNewEvent(new TradingEvent
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