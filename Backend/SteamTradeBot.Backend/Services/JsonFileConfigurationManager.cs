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
using SteamTradeBot.Backend.Models.Abstractions;

namespace SteamTradeBot.Backend.Services;

public class JsonFileConfigurationManager : IConfigurationManager
{
    #region Public

    public JsonFileConfigurationManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SetConfigurationContextForUser(string apiKey)
    {
        if (!File.Exists(GetFilePathForUser(apiKey)))
            throw new Exception("Configuration does not exist");
        _targetSection = _configuration.GetSection(apiKey);
    }

    public static void AddUsersConfigurations(ConfigurationManager configurationBuilder)
    {
        if (!Directory.Exists(UserSettingsFolderPath))
            Directory.CreateDirectory(UserSettingsFolderPath);
        var builder = configurationBuilder.SetBasePath(Environment.CurrentDirectory);
        foreach (var configFilePath in Directory.GetFiles(UserSettingsFolderPath, "*.json", SearchOption.AllDirectories))
        {
            builder.AddJsonFile(configFilePath, true, true);
        }
    }

    public async Task RefreshConfigurationAsync(string apiKey, UserConfiguration userConfiguration)
    {
        if (!File.Exists(GetFilePathForUser(apiKey)))
            AddUserConfigurationFile(apiKey, userConfiguration);

        Log.Logger.Information("Start settings update...");
        var currentSettings = await File.ReadAllTextAsync(GetFilePathForUser(apiKey));

        userConfiguration.ApiKey = apiKey;

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
        await File.WriteAllTextAsync(GetFilePathForUser(apiKey), updatedSettings);

        if (_targetSection is null)
            SetConfigurationContextForUser(apiKey);

        if (!CheckIntegrity())
        {
            await File.WriteAllTextAsync(GetFilePathForUser(apiKey), currentSettings);
            Log.Logger.Information("Settings have not been updated. UserConfiguration was corrupted.");
        }
        Log.Logger.Information("Settings have been updated!");
    }

    #region Settings

    public string ApiKey => _targetSection.GetValue<string>("ApiKey", string.Empty)!;
    public double Trend => _targetSection.GetValue<double>("Trend");
    public double AveragePriceRatio => _targetSection.GetValue<double>("AveragePriceRatio");
    public int SalesPerDay => _targetSection.GetValue<int>("SalesPerDay");
    public string SteamUserId => _targetSection.GetValue("SteamUserId", string.Empty)!;
    public double FitPriceRange => _targetSection.GetValue<double>("FitPriceRange");
    public int SellListingFindRange => _targetSection.GetValue<int>("SellListingFindRange");
    public int SalesRatio => _targetSection.GetValue<int>("SalesRatio");
    public int AnalysisIntervalDays => _targetSection.GetValue<int>("AnalysisIntervalDays");
    public int OrderQuantity => _targetSection.GetValue<int>("OrderQuantity");
    public double MinPrice => _targetSection.GetValue<double>("MinPrice");
    public double MaxPrice => _targetSection.GetValue<double>("MaxPrice");
    public int ItemListSize => _targetSection.GetValue<int>("ItemListSize");
    public double SteamCommission => _targetSection.GetValue<double>("SteamCommission");
    public double RequiredProfit => _targetSection.GetValue<double>("RequiredProfit");
    public double AvailableBalance => _targetSection.GetValue<double>("AvailableBalance");

    #endregion

    #endregion

    #region Private

    private const string UserSettingsFolderName = "UserSettings";
    private static readonly string UserSettingsFolderPath = Path.Combine(Environment.CurrentDirectory, UserSettingsFolderName);

    private readonly IConfiguration _configuration;
    private IConfigurationSection? _targetSection;

    private static void AddUserConfigurationFile(string apiKey, UserConfiguration userConfiguration)
    {
        Directory.CreateDirectory(GetFolderPathForUser(apiKey));
        userConfiguration.ApiKey = apiKey;
        var configurationJson = JsonConvert.SerializeObject(new Dictionary<string, UserConfiguration> { { apiKey, userConfiguration } });
        File.WriteAllText(GetFilePathForUser(apiKey), configurationJson);
    }

    private bool CheckIntegrity()
    {
        Log.Information("Check userConfiguration integrity...");
        try
        {
            var orderQuantity = OrderQuantity;
            var salesPerWeek = SalesPerDay;
            var steamUserId = SteamUserId;
            var sellListingFindRange = SellListingFindRange;
            var buyListingFindRange = SalesRatio;
            var analysisIntervalDays = AnalysisIntervalDays;
            var fitPriceRange = FitPriceRange;
            var averagePrice = AveragePriceRatio;
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

    private static string GetFileNameForUser(string username) =>
        $"appsettings-{username}.json" ;

    private static string GetFilePathForUser(string username)
        => Path.Combine(UserSettingsFolderPath, username, GetFileNameForUser(username));

    private static string GetFolderPathForUser(string username)
        => Path.Combine(UserSettingsFolderPath, username);

    #endregion
}