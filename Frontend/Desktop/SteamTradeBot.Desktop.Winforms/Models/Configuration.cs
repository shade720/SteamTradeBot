namespace SteamTradeBot.Desktop.Winforms.Models;

public class Configuration
{
    public int SalesPerWeek { get; set; }
    public string SteamUserId { get; set; } = string.Empty;
    public double SteamCommission { get; set; }
    public int ListingFindRange { get; set; }
    public double RequiredProfit { get; set; }
    public double AvailableBalance { get; set; }
    public double FitPriceRange { get; set; }
    public double AveragePrice { get; set; }
    public int AnalysisIntervalDays { get; set; } = 7;
    public int OrderQuantity { get; set; }
    public double Trend { get; set; }
    public double MinPrice { get; set; }
    public double MaxPrice { get; set; }
    public int ItemListSize { get; set; }
    public bool RememberMe { get; set; }

    public bool CheckIntegrity()
    {
        return false;
    }
}