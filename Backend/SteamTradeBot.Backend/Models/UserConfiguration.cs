namespace SteamTradeBot.Backend.Models;

public class UserConfiguration
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string Secret { get; set; }
    public double Trend { get; set; }
    public double AveragePrice { get; set; }
    public int SalesPerWeek { get; set; }
    public string SteamUserId { get; set; }
    public double FitPriceRange { get; set; }
    public int SellListingFindRange { get; set; }
    public int BuyListingFindRange { get; set; }
    public int AnalysisIntervalDays { get; set; }
    public int OrderQuantity { get; set; }
    public double MinPrice { get; set; }
    public double MaxPrice { get; set; }
    public int ItemListSize { get; set; }
    public double SteamCommission { get; set; }
    public double RequiredProfit { get; set; }
    public double AvailableBalance { get; set; }
}