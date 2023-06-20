namespace SteamTradeBot.Desktop.Winforms;

public class Configuration
{
    public double SalesPerWeek { get; set; }
    public double PlaceOnListing { get; set; }
    public double CoefficientOfSales { get; set; }
    public double RequiredProfit { get; set; }
    public double MinProfit { get; set; }
    public double AvailableBalance { get; set; }
    public double FitPriceInterval { get; set; }
    public double AveragePrice { get; set; }
    public string AnalysisInterval { get; set; }
    public double MinPrice { get; set; }
    public double MaxPrice { get; set; }
    public double OrderVolume { get; set; }
    public double ItemListCount { get; set; }
    public double Trend { get; set; }
    public bool RememberMe { get; set; }
}