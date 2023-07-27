using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules.CancelRules;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules.ProfitRules;
using SteamTradeBot.Backend.BusinessLogicLayer.Rules.SellRules;
using SteamTradeBot.Backend.DataAccessLayer;
using SteamTradeBot.Backend.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer;

public class TradeBot : IDisposable
{
    private readonly SteamAPI _steamApi;
    private readonly Settings _settings;
    private readonly StateManager _stateManager;
    private readonly MarketDbAccess _marketDb;
    
    private readonly MarketClient _marketClient;
    private readonly MarketRules _rules;
    private readonly ItemBuilder _itemBuilder;

    private CancellationTokenSource? _cancellationTokenSource;

    #region Public

    public TradeBot
    (
        Settings settings,
        IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory,
        StateManager stateManager)
    {
        _settings = settings;
        _marketDb = new MarketDbAccess(tradeBotDataContextFactory);
        _stateManager = stateManager;

        _steamApi = new SteamAPI();
        _itemBuilder = new ItemBuilder(_steamApi);
        _marketClient = new MarketClient(_steamApi, _settings);
        var buyRules = new List<IBuyRule>
        {
            new SalesCountRule(_settings),
            new AveragePriceRule(_settings),
            new TrendRule(_settings),
            new RequiredProfitRule(_settings),
            new AvailableBalanceRule(_settings),
            new OrderAlreadyExistRule()
        };
        var sellRules = new List<ISellRule>
        {
            new CurrentQuantityCheckRule(_marketDb)
        };
        var cancelRules = new List<ICancelRule>
        {
            new FitPriceRule(_settings, _marketDb)
        };
        _rules = new MarketRules(buyRules, sellRules, cancelRules);

        Log.Logger.Information("TradeBotSingleton created!");
    }

    #region Trading

    public void StartTrading()
    {
        if (!_settings.CheckIntegrity())
        {
            throw new ApplicationException("Configuration is corrupted. Trading not started!");
        }
        _stateManager.OnTradingStarted();
        _cancellationTokenSource = new CancellationTokenSource();
        Task.Run(WorkerLoop);
    }

    public void StopTrading()
    {
        _stateManager.OnTradingStopped();
        if (_cancellationTokenSource is null)
        {
            throw new Exception("Worker is already stopped!");
        }
        _cancellationTokenSource.Cancel();
    }

    public void ClearBuyOrders()
    {
        Log.Logger.Information("Start buy orders canceling...");
        var itemsWithPurchaseOrders = _marketDb.GetBuyOrders();
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
        _stateManager.OnLogInPending();
        try
        {
            _steamApi.LogIn(login, password, token, secret);
            _stateManager.OnLoggedIn(login);
        }
        catch
        {
            _stateManager.OnLoggedOut();
            throw;
        }
    }

    public void LogOut()
    {
        _stateManager.OnLoggedOut();
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
        var currentConfigDict = (IDictionary<string, object>)((IDictionary<string, object>)currentConfig)["TradeBotSettings"];

        foreach (var pair in newConfigDict)
        {
            if (currentConfigDict.ContainsKey(pair.Key))
                currentConfigDict[pair.Key] = pair.Value;
        }

        var updatedSettings = JsonConvert.SerializeObject(currentConfig, Formatting.Indented, jsonSettings);
        File.WriteAllText(ConfigurationPath, updatedSettings);

        if (!_settings.CheckIntegrity())
        {
            File.WriteAllText(ConfigurationPath, currentSettings);
            Log.Logger.Information("Settings have not been updated. Configuration was corrupted.");
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
            foreach (var itemPage in itemNames.Select(ConstructItemPage).Where(item => item is not null))
            {
                CheckCondition(itemPage!);
                if (_cancellationTokenSource.IsCancellationRequested)
                    break;
            }
        }
        Log.Information("Worker has stopped");
    }

    private ItemPage? ConstructItemPage(string itemName)
    {
        try
        {
            Log.Information("Get {0} page...", itemName);
            var itemPage = _itemBuilder.Create(itemName)
                .SetItemUrl()
                .SetRusItemName()
                .SetGraph(_settings.AnalysisIntervalDays)
                .SetAvgPrice()
                .SetItemSales()
                .SetTrend()
                .SetBuyOrderBook(_settings.BuyListingFindRange)
                .SetSellOrderBook(_settings.SellListingFindRange)
                .SetBalance()
                .SetMyBuyOrder()
                .Build();
            Log.Information("Get {0} page -> OK", itemName);
            _stateManager.OnItemAnalyzed(itemPage);
            return itemPage;
        }
        catch (Exception e)
        {
            Log.Error("The item was skipped due to an error ->\r\nMessage: {0}\r\nStack trace: {1}", e.Message, e.StackTrace);
            _stateManager.OnError(e);
            return null;
        }
    }

    private void CheckCondition(ItemPage itemPage)
    {
        try
        {
            if (_rules.CanSellItem(itemPage))
            {
                FormSellOrder(itemPage);
                return;
            }
            if (_rules.IsNeedCancelItem(itemPage))
            {
                FormOrderCancelling(itemPage);
                return;
            }
            if (_rules.CanBuyItem(itemPage))
            {
                FormBuyOrder(itemPage);
                return;
            }
        }
        catch (Exception e)
        {
            Log.Error("The item was skipped due to an error ->\r\nMessage: {0}\r\nStack trace: {1}", e.Message, e.StackTrace);
            _stateManager.OnError(e);
        }
    }

    private void FormOrderCancelling(ItemPage item)
    {
        var order = _marketDb.GetBuyOrders().FirstOrDefault(x => x.EngItemName == item.EngItemName);
        _marketClient.CancelBuyOrder(order);
        _marketDb.RemoveBuyOrder(order);
        _stateManager.OnItemCancelling(order);
    }

    private void FormSellOrder(ItemPage item)
    {
        var buyOrder = _marketDb.GetBuyOrders().FirstOrDefault(x => x.EngItemName == item.EngItemName);
        if (buyOrder is null)
            throw new Exception("Can't find local buy order for sell order forming");
        var sellOrder = new SellOrder
        {
            EngItemName = item.EngItemName,
            RusItemName = item.RusItemName,
            ItemUrl = item.ItemUrl,
            Price = buyOrder.Price * (1 + _settings.SteamCommission) + _settings.RequiredProfit,
            Quantity = item.MyBuyOrder is null ? buyOrder.Quantity : buyOrder.Quantity - item.MyBuyOrder.Quantity
        };

        _marketClient.Sell(sellOrder);

        if (item.MyBuyOrder is null)
        {
            _marketDb.RemoveBuyOrder(buyOrder);
        }
        if (item.MyBuyOrder is not null && item.MyBuyOrder.Quantity > 0)
        {
            buyOrder.Quantity = item.MyBuyOrder.Quantity;
            _marketDb.AddOrUpdateBuyOrder(buyOrder);
        }

        _marketDb.AddOrUpdateSellOrder(sellOrder);
        _stateManager.OnItemSelling(sellOrder);
    }

    private void FormBuyOrder(ItemPage item)
    {
        var price = item.BuyOrderBook.FirstOrDefault(buyOrder => item.SellOrderBook.Any(sellOrder => sellOrder.Price + _settings.RequiredProfit > buyOrder.Price * (1 + _settings.SteamCommission)));
        if (price is null)
            throw new Exception("Sell order not found");
        var buyOrder = new BuyOrder
        {
            EngItemName = item.EngItemName,
            RusItemName = item.RusItemName,
            ItemUrl = item.ItemUrl,
            Price = price.Price,
            Quantity = _settings.OrderQuantity
        };
        _marketClient.Buy(buyOrder);
        _marketDb.AddOrUpdateBuyOrder(buyOrder);
        _stateManager.OnItemBuying(buyOrder);
    }

    private IEnumerable<string> GetItemNames()
    {
        try
        {
            Log.Information("Load items for analysis....");
            var loadedItemNamesList = _steamApi.GetItemNamesList(
                    _settings.MinPrice,
                    _settings.MaxPrice,
                    _settings.SalesPerWeek * 7,
                    _settings.ItemListSize)
                .ToList();
            var existingOrdersItemNames = _marketDb.GetBuyOrders().Select(x => x.EngItemName);
            foreach (var itemName in existingOrdersItemNames)
            {
                loadedItemNamesList.Insert(0, itemName);
                Log.Logger.Information("Add {0} as existing order",itemName);
            }
            Log.Information("Pipeline initialized");
            return loadedItemNamesList;
        }
        catch (Exception e)
        {
            Log.Logger.Error("Error due to getting items list. Message: {0}, StackTrace: {1}", e.Message, e.StackTrace);
            return new List<string>();
        }
    }

    #endregion
}