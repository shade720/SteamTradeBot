using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;
using SteamTradeBot.Backend.Models;
using SteamTradeBot.Backend.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.Services;

public class JsonFileBasedConfigurationManagerService : IConfigurationManager
{
    #region Public

    public JsonFileBasedConfigurationManagerService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    #region Settings

    public string ApiKey => _configuration.GetValue<string>("ApiKey", string.Empty)!;
    public double Trend => _configuration.GetValue<double>("Trend");
    public double AveragePriceRatio => _configuration.GetValue<double>("AveragePriceRatio");
    public int SalesPerDay => _configuration.GetValue<int>("SalesPerDay");
    public string SteamUserId => _configuration.GetValue("SteamUserId", string.Empty)!;
    public double FitPriceRange => _configuration.GetValue<double>("FitPriceRange");
    public int SellListingFindRange => _configuration.GetValue<int>("SellListingFindRange");
    public double SalesRatio => _configuration.GetValue<double>("SalesRatio");
    public int AnalysisIntervalDays => _configuration.GetValue<int>("AnalysisIntervalDays");
    public int OrderQuantity => _configuration.GetValue<int>("OrderQuantity");
    public double MinPrice => _configuration.GetValue<double>("MinPrice");
    public double MaxPrice => _configuration.GetValue<double>("MaxPrice");
    public int ItemListSize => _configuration.GetValue<int>("ItemListSize");
    public double SteamCommission => _configuration.GetValue<double>("SteamCommission");
    public double RequiredProfit => _configuration.GetValue<double>("RequiredProfit");
    public double AvailableBalance => _configuration.GetValue<double>("AvailableBalance");

    #endregion

    public async Task<bool> RefreshConfigurationAsync(string apiKey, UserConfiguration userConfiguration)
    {
        Log.Logger.Information("Start settings update...");
        var currentSettings = await File.ReadAllTextAsync(SettingsPath);

        userConfiguration.ApiKey = apiKey;

        var jsonSettings = new JsonSerializerSettings();
        jsonSettings.Converters.Add(new ExpandoObjectConverter());
        jsonSettings.Converters.Add(new StringEnumConverter());

        dynamic? currentConfig = JsonConvert.DeserializeObject<ExpandoObject>(currentSettings, jsonSettings);
        dynamic? newConfig = JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(userConfiguration), jsonSettings);

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
        await File.WriteAllTextAsync(SettingsPath, updatedSettings);

        if (!CheckIntegrity())
        {
            await File.WriteAllTextAsync(SettingsPath, currentSettings);
            Log.Logger.Information("Settings have not been updated. UserConfiguration was corrupted.");
            return false;
        }
        Log.Logger.Information("Settings have been updated!");
        return true;
    }

    #endregion

    #region Private
    
    private static readonly string SettingsPath = Path.Combine(Environment.CurrentDirectory, "appsettings.json");

    private readonly IConfiguration _configuration;

    private bool CheckIntegrity()
    {
        Log.Information("Check userConfiguration integrity...");
        try
        {
            if (OrderQuantity is <= 0 or > 10)
            {
                Log.Fatal("Quantity can not be less than 0 or greater than 10!");
                return false;
            }

            if (SalesPerDay <= 0)
            {
                Log.Fatal("Sales can not be less than 0!");
                return false;
            }

            if (AvailableBalance is <= 0.0 or > 1.0)
            {
                Log.Fatal("Available balance range from 0.0 to 1.0");
                return false;
            }

            if (SteamCommission is <= 0.0 or > 1.0)
            {
                Log.Fatal("Steam commission range from 0.0 to 1.0");
                return false;
            }

            if (FitPriceRange < 0)
            {
                Log.Fatal("FitPriceRange must be greater than 0.0");
                return false;
            }

            if (SellListingFindRange is <= 0 or > 5)
            {
                Log.Fatal("SellListingFindRange range from 1 to 5");
                return false;
            }

            if (SalesRatio is < 0.0 or > 1.0)
            {
                Log.Fatal("SalesRatio range from 0.0 to 1.0");
                return false;
            }

            if (RequiredProfit < 0)
            {
                Log.Fatal("RequiredProfit must be positive");
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

    #endregion
}