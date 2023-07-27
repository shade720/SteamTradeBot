using System;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace SteamTradeBot.Backend.Models;

public class Settings
{
    private readonly IConfiguration _configuration;

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
}