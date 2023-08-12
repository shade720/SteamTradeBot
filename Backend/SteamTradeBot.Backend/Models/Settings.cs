using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Runtime;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Serilog;

namespace SteamTradeBot.Backend.Models;

public class Settings
{
    private readonly IConfiguration _configuration;
    private const string SettingsFileName = "appsettings.json";
    private static readonly string ConfigurationPath = Path.Combine(Environment.CurrentDirectory, SettingsFileName);

    public Settings(IConfiguration configuration)
    {
        _configuration = configuration.GetSection("TradeBotSettings");
    }

    public double Trend => _configuration.GetValue<double>("Trend");
    public double AveragePrice => _configuration.GetValue<double>("AveragePrice");
    public int SalesPerWeek => _configuration.GetValue<int>("SalesPerWeek");
    public string SteamUserId => _configuration.GetValue<string>("SteamUserId");
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

    public bool CheckIntegrity()
    {
        try
        {
            Log.Information("Check configuration integrity...");
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

                Log.Information("Check configuration integrity -> OK");
                return true;
            }
            catch (Exception e)
            {
                Log.Fatal("Configuration error:\r\nMessage: {0}\r\nStackTrace: {1}", e.Message, e.StackTrace);
                return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

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
        var targetSection = (IDictionary<string, object>)currentConfigDict["TradeBotSettings"];

        foreach (var pair in newConfigDict)
        {
            if (targetSection.ContainsKey(pair.Key))
                targetSection[pair.Key] = pair.Value;
        }

        var updatedSettings = JsonConvert.SerializeObject(currentConfig, Formatting.Indented, jsonSettings);
        File.WriteAllText(ConfigurationPath, updatedSettings);

        if (!CheckIntegrity())
        {
            File.WriteAllText(ConfigurationPath, currentSettings);
            Log.Logger.Information("Settings have not been updated. Configuration was corrupted.");
        }
        Log.Logger.Information("Settings have been updated!");
    }
}