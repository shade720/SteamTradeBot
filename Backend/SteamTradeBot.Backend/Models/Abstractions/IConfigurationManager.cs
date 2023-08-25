using System.Threading.Tasks;

namespace SteamTradeBot.Backend.Models.Abstractions;

public interface IConfigurationManager
{
    /// <summary>
    /// Секретный ключ клиента.
    /// </summary>
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

    /// <summary>
    /// SteamID пользователя.
    /// </summary>
    public string SteamUserId { get; }

    /// <summary>
    /// Промежуток цены, при выходе из которого заказ на покупку будет отменен.
    /// </summary>
    public double FitPriceRange { get; }

    /// <summary>
    /// Количество страниц листинга цен продажу, которые будут просмотренны.
    /// </summary>
    public int SellListingFindRange { get; }

    /// <summary>
    /// Коэффициент продаж - лежит в промежутке 0:1 - место в списке цена-продажи (чем больше коэффициент, тем ниже будет цена и тем дольше будут покупаться предметы).
    /// </summary>
    public double SalesRatio { get; }

    /// <summary>
    /// Период анализа в днях, в рамках которого анализируется график предмета.
    /// </summary>
    public int AnalysisIntervalDays { get; }

    /// <summary>
    /// Количество экземпляров предмета в заказе на покупку.
    /// </summary>
    public int OrderQuantity { get; }

    /// <summary>
    /// Минимальная цена предмета при формировании списка имен предметов, которые необходимо проанализировать.
    /// </summary>
    public double MinPrice { get; }

    /// <summary>
    /// Максимальная цена предмета при формировании списка имен предметов, которые необходимо проанализировать.
    /// </summary>
    public double MaxPrice { get; }

    /// <summary>
    /// Размер списка имен предметов, которые необходимо проанализировать.
    /// </summary>
    public int ItemListSize { get; }

    /// <summary>
    /// Коммиссия Steam, которая учитывается при поиске оптимального заказа на продажу.
    /// </summary>
    public double SteamCommission { get; }

    /// <summary>
    /// Необходимая выгода, которая учитывается при поиске оптимального заказа на продажу.
    /// </summary>
    public double RequiredProfit { get; }

    /// <summary>
    /// Коэффициент доступного баланса кошелька стим.
    /// </summary>
    public double AvailableBalance { get; }
    public Task<bool> RefreshConfigurationAsync(string apiKey, UserConfiguration userConfiguration);
}