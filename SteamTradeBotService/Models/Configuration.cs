using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamTradeBotService.Models
{
    public static class Configuration
    {
        public static double SalesPerWeek;
        public static double PlaceOnListing;
        public static double CoefficientOfSales;
        public static double RequiredProfit;
        public static double MinProfit;
        public static double AvailableBalance;
        public static double FitPriceInterval;
        public static double AveragePrice;
        public static string AnalysisInterval;
        public static double MinPrice;
        public static double MaxPrice;
        public static double OrderVolume;
        public static double ItemListCount;
        public static double Trend;

        public static void SetConfiguration(Protos.Configuration configuration)
        {
            AnalysisInterval = configuration.AnalysisInterval;
            AvailableBalance = configuration.AvailableBalance;
            AveragePrice = configuration.AveragePrice;
            CoefficientOfSales = configuration.CoefficientOfSales;
            FitPriceInterval = configuration.FitPriceInterval;
            ItemListCount = configuration.ItemListCount;
            MaxPrice = configuration.MaxPrice;
            MinPrice = configuration.MinPrice;
            MinProfit = configuration.MinProfit;
            OrderVolume = configuration.OrderVolume;
            PlaceOnListing = configuration.PlaceOnListing;
            RequiredProfit = configuration.RequiredProfit;
            SalesPerWeek = configuration.SalesPerWeek;
            Trend = configuration.Trend;
        }
    }
}
