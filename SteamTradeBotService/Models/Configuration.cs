using System.Threading.Tasks;

namespace SteamTradeBotService.Models
{
    public class Configuration
    {
        public double SalesPerWeek;
        public double PlaceOnListing;
        public double CoefficientOfSales;
        public double RequiredProfit;
        public double MinProfit;
        public double AvailableBalance;
        public double FitPriceInterval;
        public double AveragePrice;
        public string AnalysisInterval;
        public double MinPrice;
        public double MaxPrice;
        public double OrderVolume;
        public double ItemListCount;
        public double Trend;

        public Task SetConfiguration(Protos.Configuration configuration)
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
            return Task.CompletedTask;
        }
    }
}
