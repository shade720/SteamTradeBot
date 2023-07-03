using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules.CancelRules;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules.ProfitRules;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules.SellRules;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer;

public class TradeBot : IDisposable
{
    private readonly SteamAPI _steamApi;
    private readonly IConfiguration _configuration;
    private readonly ServiceState _state;
    private readonly DbAccess _db;
    private readonly Stopwatch _stopwatch;

    private readonly MarketClient _marketClient;
    private readonly MarketRules _rules;
    private readonly ItemBuilder _itemBuilder;

    private CancellationTokenSource? _cancellationTokenSource;


    #region Public

    public TradeBot
    (
        IConfiguration configuration, 
        IDbContextFactory<MarketDataContext> marketContextFactory)
    {
        _configuration = configuration;

        _state = new ServiceState();
        _db = new DbAccess(marketContextFactory);
        _steamApi = new SteamAPI();
        _stopwatch = new Stopwatch();
        _itemBuilder = new ItemBuilder(_steamApi);
        _marketClient = new MarketClient(_steamApi, _configuration);
        var buyRules = new List<IBuyRule>
        {
            new SalesCountRule(_configuration),
            new AveragePriceRule(_configuration),
            new TrendRule(_configuration),
            new RequiredProfitRule(_configuration),
            new AvailableBalanceRule(_steamApi.GetBalance(), _configuration),
        };
        var sellRules = new List<ISellRule>()
        {
            new CurrentQuantityCheckRule(_steamApi)
        };
        var cancelRules = new List<ICancelRule>
        {
            new FitPriceRule(_steamApi, _configuration)
        };
        _rules = new MarketRules(buyRules, sellRules, cancelRules);

        Log.Logger.Information("TradeBotSingleton created!");
    }

    #region Trading

    public void StartTrading()
    {
        if (!CheckConfigurationIntegrity())
        {
            throw new ApplicationException("Configuration is corrupted. Trading not started!");
        }
        _state.WorkingState = ServiceState.ServiceWorkingState.Up;
        _stopwatch.Start();
        _cancellationTokenSource = new CancellationTokenSource();
        Task.Run(WorkerLoop);
    }

    public void StopTrading()
    {
        if (_cancellationTokenSource is null)
        {
            _state.WorkingState = ServiceState.ServiceWorkingState.Down;
            throw new Exception("Worker is already stopped!");
        }
        _cancellationTokenSource.Cancel();
        _state.WorkingState = ServiceState.ServiceWorkingState.Down;
        _stopwatch.Stop();
        _stopwatch.Reset();
    }

    public void ClearBuyOrders()
    {
        Log.Logger.Information("Start buy orders canceling...");
        var itemsWithPurchaseOrders = _db.GetBuyOrders();
        foreach (var item in itemsWithPurchaseOrders)
        {
            _marketClient.CancelBuyOrder(item);
            Log.Logger.Information("Item {0} with buy price {1} was canceled", item.EngItemName, item.Price);
        }
        Log.Logger.Information("All buy orders have been canceled!");
    }

    #endregion

    #region Auth

    public void LogIn(string login, string password, string token, string secret)
    {
        _state.IsLoggedIn = ServiceState.LogInState.Pending;
        try
        {
            _steamApi.LogIn(login, password, token, secret);
            _state.IsLoggedIn = ServiceState.LogInState.LoggedIn;
            _state.CurrentUser = login;
        }
        catch
        {
            _state.IsLoggedIn = ServiceState.LogInState.NotLoggedIn;
            _state.CurrentUser = string.Empty;
            throw;
        }
    }

    public void LogOut()
    {
        _state.IsLoggedIn = ServiceState.LogInState.NotLoggedIn;
        _state.CurrentUser = string.Empty;
        _steamApi.LogOut();
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
        return serviceStateCopy;
    }

    #endregion

    public void Dispose()
    {
        Log.Logger.Information("Disposing trade bot singleton...");
        _steamApi.Dispose();
        Log.Logger.Information("Trade bot singleton disposed!");
    }
    #endregion

    #region Private

    private void WorkerLoop()
    {
        if (_cancellationTokenSource is null)
            throw new ArgumentException("Cancellation token was null!");
        Log.Information("Worker has started");
        while (!_cancellationTokenSource.IsCancellationRequested)
        {
            var itemNames = GetItemNames();
            foreach (var item in itemNames.Select(ConstructItemPage).Where(item => item is not null))
            {
                AnalyzeItem(item!);
            }
        }
        Log.Information("Worker has ended");
    }

    private ItemPage? ConstructItemPage(string itemName)
    {
        try
        {
            var itemPage = _itemBuilder.Create(itemName)
                .SetRusItemName()
                .SetItemUrl()
                .SetGraph(int.Parse(_configuration["AnalysisIntervalDays"]!))
                .SetAvgPrice()
                .SetItemSales()
                .SetTrend()
                .SetBuyOrderBook()
                .SetSellOrderBook(int.Parse(_configuration["ListingFindRange"]!))
                .SetBalance()
                .Build();
            _db.AddNewEvent(new TradingEvent
            {
                Type = InfoType.ItemAnalyzed,
                CurrentBalance = itemPage.Balance,
                Time = DateTime.UtcNow,
                Info = itemPage.EngItemName
            });
            _state.ItemsAnalyzed++;
            return itemPage;
        }
        catch (Exception e)
        {
            Log.Error("The item was skipped due to an error ->\r\nMessage: {0}\r\nStack trace: {1}", e.Message, e.StackTrace);
            OnError(e);
            return null;
        }
    }

    private void AnalyzeItem(ItemPage itemPage)
    {
        try
        {
            if (_rules.CanBuyItem(itemPage))
            {
                FormBuyOrder(itemPage);
                return;
            }

            if (_rules.IsNeedCancelItem(itemPage))
            {
                FormOrderCancelling(itemPage);
                return;
            }

            if (_rules.CanSellItem(itemPage))
            {
                FormSellOrder(itemPage);
                return;
            }
        }
        catch (Exception e)
        {
            Log.Error("The item was skipped due to an error ->\r\nMessage: {0}\r\nStack trace: {1}", e.Message, e.StackTrace);
            OnError(e);
        }
    }

    private void FormOrderCancelling(ItemPage item)
    {
        var order = _db.GetBuyOrders().FirstOrDefault(x => x.EngItemName == item.EngItemName && x.RusItemName == item.RusItemName && x.ItemUrl == item.ItemUrl && Math.Abs(x.Price - item.BuyPrice) < 0.001);
        _marketClient.CancelBuyOrder(order);
        _db.RemoveBuyOrder(order);
        _db.AddNewEvent(new TradingEvent
        {
            Type = InfoType.ItemCanceled,
            Time = DateTime.UtcNow,
            Info = order.EngItemName
        });
        _state.ItemCanceled++;
        _state.Events.Add($"{DateTime.UtcNow}-{order.EngItemName}-Canceled-{order.Price}");
    }

    private void FormSellOrder(ItemPage item)
    {
        var buyOrder = _db.GetBuyOrders().FirstOrDefault(x => x.EngItemName == item.EngItemName && x.RusItemName == item.RusItemName && x.ItemUrl == item.ItemUrl);
        var steamCommission = double.Parse(_configuration["SteamCommission"]!, NumberStyles.Any,
            CultureInfo.InvariantCulture);
        var requiredProfit = double.Parse(_configuration["RequiredProfit"]!, NumberStyles.Any,
            CultureInfo.InvariantCulture);
        var sellOrder = new SellOrder
        {
            EngItemName = item.EngItemName,
            RusItemName = item.RusItemName,
            ItemUrl = item.ItemUrl,
            Price = buyOrder.Price * (1 + steamCommission) + requiredProfit
        };

        _marketClient.Sell(sellOrder);
        _db.AddOrUpdateSellOrder(sellOrder);
        _db.AddNewEvent(new TradingEvent
        {
            Type = InfoType.ItemSold,
            Time = DateTime.UtcNow,
            Info = sellOrder.EngItemName,
            SellPrice = sellOrder.Price,
            Profit = sellOrder.Price - buyOrder.Price
        });
        _state.ItemsSold++;
        _state.Events.Add($"{DateTime.UtcNow}-{sellOrder.EngItemName}-Sold-{sellOrder.Price}");
    }

    private void FormBuyOrder(ItemPage item)
    {
        var buyOrder = new BuyOrder
        {
            EngItemName = item.EngItemName,
            RusItemName = item.RusItemName,
            ItemUrl = item.ItemUrl,
            Price = item.BuyPrice,
            Quantity = int.Parse(_configuration["OrderQuantity"]!)
        };
        _marketClient.Buy(buyOrder);

        _db.AddOrUpdateBuyOrder(buyOrder);
        _db.AddNewEvent(new TradingEvent
        {
            Type = InfoType.ItemBought,
            Time = DateTime.UtcNow,
            Info = buyOrder.EngItemName,
            BuyPrice = buyOrder.Price
        });
        _state.ItemsBought++;
        _state.Events.Add($"{DateTime.UtcNow}-{buyOrder.EngItemName}-Bought-{buyOrder.Price}");
    }


    private IEnumerable<string> GetItemNames()
    {
        try
        {
            Log.Information("Load items for analysis....");
            var ordersNames = _db.GetBuyOrders().Select(x => x.EngItemName);
            var loadedItemList = _steamApi.GetItemNamesList(
                    double.Parse(_configuration["MinPrice"]!, NumberStyles.Any, CultureInfo.InvariantCulture),
                    double.Parse(_configuration["MaxPrice"]!, NumberStyles.Any, CultureInfo.InvariantCulture),
                    int.Parse(_configuration["SalesPerWeek"]!) * 7,
                    int.Parse(_configuration["ItemListSize"]!))
                .Where(itemName => ordersNames.All(order => order != itemName))
                .ToList();
            Log.Information("Pipeline initialized");
            return loadedItemList;
        }
        catch (Exception e)
        {
            Log.Logger.Error("Error due to getting items list. Message: {0}, StackTrace: {1}", e.Message, e.StackTrace);
            return new List<string>();
        }
    }

    #region WorkerEventHandlers

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

    #endregion

    #endregion
}