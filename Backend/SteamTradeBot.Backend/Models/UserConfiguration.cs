namespace SteamTradeBot.Backend.Models;

public class UserConfiguration
{
    public string ApiKey { get; set; }
    /// <summary>
    /// Тренд цены - лежит в промежутке -Infinity:+Infinity - угол наклона усредненного графика продаж.
    /// </summary>
    public double Trend { get; set; }

    /// <summary>
    /// Коэффициент средней цены - лежит в промежутке 0:2 - значение 1 соответствует фактической средней цене из графика, в большую сторону повышает планку средней цены.
    /// </summary>
    public double AveragePriceRatio { get; set; }

    /// <summary>
    /// Количество продаж предмета за один день.
    /// </summary>
    public int SalesPerDay { get; set; }
    public string SteamUserId { get; set; }
    public double FitPriceRange { get; set; }
    public int SellListingFindRange { get; set; }

    /// <summary>
    /// Коэффициент продаж - лежит в промежутке 0:1 - место в списке цена-продажи (чем больше коэффициент, тем ниже будет цена и тем дольше будут покупаться предметы).
    /// </summary>
    public int SalesRatio { get; set; }
    public int AnalysisIntervalDays { get; set; }
    public int OrderQuantity { get; set; }
    public double MinPrice { get; set; }
    public double MaxPrice { get; set; }
    public int ItemListSize { get; set; }
    public double SteamCommission { get; set; }
    public double RequiredProfit { get; set; }
    public double AvailableBalance { get; set; }
}