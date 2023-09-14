namespace SteamTradeBot.Desktop.Winforms.Models;

public class ApplicationSettings
{
    public string ApiKey { get; set; } = string.Empty;
    public double Trend { get; init; } = 0.1;
    public double AveragePriceRatio { get; init; } = 0.01;
    public int SalesPerDay { get; init; } = 100;
    public string SteamUserId { get; init; } = string.Empty;
    public double SteamCommission { get; init; } = 0.14;
    public double FitPriceRange { get; init; } = 0.5;
    public int SellListingFindRange { get; init; } = 1;
    public double SalesRatio { get; init; } = 0.6;
    public int AnalysisIntervalDays { get; init; } = 7;
    public int OrderQuantity { get; init; } = 1;
    public double MinPrice { get; init; } = 0.1;
    public double MaxPrice { get; init; } = 0.5;
    public int ItemListSize { get; init; } = 20;
    public double RequiredProfit { get; init; } = 0.1;
    public double AvailableBalance { get; init; } = 1.0;
    public string ServerAddress { get; set; } = "http://localhost:5050";
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
}