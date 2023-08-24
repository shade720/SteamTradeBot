using System.Threading.Tasks;

namespace SteamTradeBot.Backend.Models.Abstractions;

public interface IConfigurationManager
{
    public string ApiKey { get; }

    /// <summary>
    /// Тренд цены - лежит в промежутке -Infinity:+Infinity - угол наклона усредненного графика продаж.
    /// </summary>
    public double Trend { get; }

    /// <summary>
    /// Коэффициент средней цены - лежит в промежутке 0:2 - значение 1 соответствует фактической средней цене из графика, в большую сторону повышает планку средней цены.
    /// </summary>
    public double AveragePriceRatio { get; }

    /// <summary>
    /// Количество продаж предмета за один день.
    /// </summary>
    public int SalesPerDay { get; }
    public string SteamUserId { get; }
    public double FitPriceRange { get; }
    public int SellListingFindRange { get; }

    /// <summary>
    /// Коэффициент продаж - лежит в промежутке 0:1 - место в списке цена-продажи (чем больше коэффициент, тем ниже будет цена и тем дольше будут покупаться предметы).
    /// </summary>
    public int SalesRatio { get; }
    public int AnalysisIntervalDays { get; }
    public int OrderQuantity { get; }
    public double MinPrice { get; }
    public double MaxPrice { get; }
    public int ItemListSize { get; }
    public double SteamCommission { get; }
    public double RequiredProfit { get; }
    public double AvailableBalance { get; }
    public Task<bool> RefreshConfigurationAsync(string apiKey, UserConfiguration userConfiguration);
}