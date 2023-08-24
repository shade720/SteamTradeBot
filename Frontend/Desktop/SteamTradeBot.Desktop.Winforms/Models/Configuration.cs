namespace SteamTradeBot.Desktop.Winforms.Models;

public class Configuration
{
    public string ApiKey { get; set; }
    public double Trend { get; set; }
    public double AveragePriceRatio { get; set; }
    public int SalesPerDay { get; set; }
    public string SteamUserId { get; set; } = string.Empty;
    public double SteamCommission { get; set; }
    public double FitPriceRange { get; set; }
    public int SellListingFindRange { get; set; }
    public int SalesRatio { get; set; }
    public int AnalysisIntervalDays { get; set; } = 7;
    public int OrderQuantity { get; set; }
    public double MinPrice { get; set; }
    public double MaxPrice { get; set; }
    public int ItemListSize { get; set; }
    public double RequiredProfit { get; set; }
    public double AvailableBalance { get; set; }

    public bool CheckIntegrity()
    {
        return false;
    }
}