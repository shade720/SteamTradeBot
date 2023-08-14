using SteamTradeBot.Backend.Models;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.Models.Abstractions;

public interface IConfigurationManager
{
    public string Login { get; }
    public string Password { get; }
    public string Secret { get; }
    public double Trend { get; }
    public double AveragePrice { get; }
    public int SalesPerDay { get; }
    public string SteamUserId { get; }
    public double FitPriceRange { get; }
    public int SellListingFindRange { get; }
    public int SalesRatio { get; }
    public int AnalysisIntervalDays { get; }
    public int OrderQuantity { get; }
    public double MinPrice { get; }
    public double MaxPrice { get; }
    public int ItemListSize { get; }
    public double SteamCommission { get; }
    public double RequiredProfit { get; }
    public double AvailableBalance { get; }
    public Task RefreshConfigurationAsync(UserConfiguration userConfiguration);
}