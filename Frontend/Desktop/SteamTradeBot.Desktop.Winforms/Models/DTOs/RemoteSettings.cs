namespace SteamTradeBot.Desktop.Winforms.Models.DTOs;

public class RemoteSettings
{
    public string ApiKey { get; set; }
    public double Trend { get; set; }
    public double AveragePriceRatio { get; set; }
    public int SalesPerDay { get; set; }
    public string SteamUserId { get; set; } = string.Empty;
    public double SteamCommission { get; set; }
    public double FitPriceRange { get; set; }
    public int SellListingFindRange { get; set; }
    public double SalesRatio { get; set; }
    public int AnalysisIntervalDays { get; set; } = 7;
    public int OrderQuantity { get; set; }
    public double MinPrice { get; set; }
    public double MaxPrice { get; set; }
    public int ItemListSize { get; set; }
    public double RequiredProfit { get; set; }
    public double AvailableBalance { get; set; }

    public RemoteSettings(ApplicationSettings appsettings)
    {
        ApiKey = appsettings.ApiKey;
        Trend = appsettings.Trend;
        AveragePriceRatio = appsettings.AveragePriceRatio;
        SalesPerDay = appsettings.SalesPerDay;
        SteamUserId = appsettings.SteamUserId;
        SteamCommission = appsettings.SteamCommission;
        FitPriceRange = appsettings.FitPriceRange;
        SellListingFindRange = appsettings.SellListingFindRange;
        SalesRatio = appsettings.SalesRatio;
        AnalysisIntervalDays = appsettings.AnalysisIntervalDays;
        OrderQuantity = appsettings.OrderQuantity;
        MaxPrice = appsettings.MaxPrice;
        MinPrice = appsettings.MinPrice;
        ItemListSize = appsettings.ItemListSize;
        RequiredProfit = appsettings.RequiredProfit;
        AvailableBalance = appsettings.AvailableBalance;
    }
}