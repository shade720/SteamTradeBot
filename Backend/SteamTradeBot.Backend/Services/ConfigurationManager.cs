using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Serilog;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.Services;

public class ConfigurationManager
{
    #region Public

    #region Settings

    public string Login => _configuration.GetValue("Login", string.Empty)!;
    public string Password => _configuration.GetValue("Password", string.Empty)!;
    public string Secret => _configuration.GetValue("Secret", string.Empty)!;
    public double Trend => _configuration.GetValue<double>("Trend");
    public double AveragePrice => _configuration.GetValue<double>("AveragePrice");
    public int SalesPerWeek => _configuration.GetValue<int>("SalesPerWeek");
    public string SteamUserId => _configuration.GetValue("SteamUserId", string.Empty)!;
    public double FitPriceRange => _configuration.GetValue<double>("FitPriceRange");
    public int SellListingFindRange => _configuration.GetValue<int>("SellListingFindRange");
    public int BuyListingFindRange => _configuration.GetValue<int>("BuyListingFindRange");
    public int AnalysisIntervalDays => _configuration.GetValue<int>("AnalysisIntervalDays");
    public int OrderQuantity => _configuration.GetValue<int>("OrderQuantity");
    public double MinPrice => _configuration.GetValue<double>("MinPrice");
    public double MaxPrice => _configuration.GetValue<double>("MaxPrice");
    public int ItemListSize => _configuration.GetValue<int>("ItemListSize");
    public double SteamCommission => _configuration.GetValue<double>("SteamCommission");
    public double RequiredProfit => _configuration.GetValue<double>("RequiredProfit");
    public double AvailableBalance => _configuration.GetValue<double>("AvailableBalance");

    #endregion

    public ConfigurationManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SetConfigurationContextForUser(string username, UserConfiguration userConfiguration)
    {
        if (_currentUser == username)
            return;
        _currentUser = username;
        if (!File.Exists(GetFilePathForUser(username)))
        {
            AddUserConfigurationFile(username, userConfiguration);
        }
        _configuration = _configuration.GetSection(username);
    }

    public async Task RefreshConfigurationAsync(UserConfiguration userConfiguration)
    {
        Log.Logger.Information("Start settings update...");
        var currentSettings = await File.ReadAllTextAsync(GetFilePathForUser(_currentUser));

        var jsonSettings = new JsonSerializerSettings();
        jsonSettings.Converters.Add(new ExpandoObjectConverter());
        jsonSettings.Converters.Add(new StringEnumConverter());

        dynamic? currentConfig = JsonConvert.DeserializeObject<ExpandoObject>(currentSettings, jsonSettings);
        dynamic? newConfig = JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(userConfiguration), jsonSettings);

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
        await File.WriteAllTextAsync(GetFilePathForUser(_currentUser), updatedSettings);

        if (!CheckIntegrity())
        {
            await File.WriteAllTextAsync(GetFilePathForUser(_currentUser), currentSettings);
            Log.Logger.Information("Settings have not been updated. UserConfiguration was corrupted.");
        }
        Log.Logger.Information("Settings have been updated!");
    }

    #endregion

    private const string UserSettingsFolderName = "UserSettings";
    private const string ConfigurationFileName = "appsettings.json";
    private static readonly string UserSettingsFolderPath = Path.Combine(Environment.CurrentDirectory, UserSettingsFolderName);

    private string _currentUser = "default";
    private IConfiguration _configuration;

    private static void AddUserConfigurationFile(string username, UserConfiguration userConfiguration)
    {
        Directory.CreateDirectory(GetFolderPathForUser(username));
        var configurationJson = JsonConvert.SerializeObject(new Dictionary<string, UserConfiguration> { { username, userConfiguration } });
        File.WriteAllText(GetFilePathForUser(username), configurationJson);
    }

    private bool CheckIntegrity()
    {
        try
        {
            Log.Information("Check userConfiguration integrity...");
            try
            {
                var orderQuantity = OrderQuantity;
                var salesPerWeek = SalesPerWeek;
                var steamUserId = SteamUserId;
                var sellListingFindRange = SellListingFindRange;
                var buyListingFindRange = BuyListingFindRange;
                var analysisIntervalDays = AnalysisIntervalDays;
                var fitPriceRange = FitPriceRange;
                var averagePrice = AveragePrice;
                var trend = Trend;
                var steamCommission = SteamCommission;
                var requiredProfit = RequiredProfit;
                var availableBalance = AvailableBalance;
                var minPrice = MinPrice;
                var maxPrice = MaxPrice;
                var itemListSize = ItemListSize;

                if (orderQuantity is <= 0 or > 10)
                {
                    Log.Fatal("Quantity can not be less than 0 or greater than 10!");
                    return false;
                }

                if (salesPerWeek <= 0)
                {
                    Log.Fatal("Sales can not be less than 0!");
                    return false;
                }

                if (availableBalance is <= 0.0 or > 1.0)
                {
                    Log.Fatal("Available balance range from 0.0 to 1.0");
                    return false;
                }

                if (steamCommission is <= 0.0 or > 1.0)
                {
                    Log.Fatal("Steam commission range from 0.0 to 1.0");
                    return false;
                }

                Log.Information("Check userConfiguration integrity -> OK");
                return true;
            }
            catch (Exception e)
            {
                Log.Fatal("UserConfiguration error:\r\nMessage: {0}\r\nStackTrace: {1}", e.Message, e.StackTrace);
                return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static string GetFilePathForUser(string username)
        => Path.Combine(UserSettingsFolderPath, username, ConfigurationFileName);

    private static string GetFolderPathForUser(string username)
        => Path.Combine(UserSettingsFolderPath, username);
}