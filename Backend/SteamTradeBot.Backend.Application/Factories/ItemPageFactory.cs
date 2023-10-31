using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Domain.ItemPageAggregate;
using SteamTradeBot.Backend.Domain.OrderAggregate;

namespace SteamTradeBot.Backend.Application.Factories;

public sealed class ItemPageFactory
{
    private readonly ISteamApi _api;
    private readonly IConfigurationService _configurationService;

    internal ItemPageFactory(
        ISteamApi api,
        IConfigurationService configurationService)
    {
        _api = api;
        _configurationService = configurationService;
    }

    internal async Task<ItemPage> CreateAsync(string engItemName)
    {
        var itemPage = new ItemPage { EngItemName = engItemName };
        itemPage.ItemUrl = await _api.GetItemUrlAsync(itemPage.EngItemName);
        itemPage.RusItemName = await _api.GetRusItemNameAsync(itemPage.ItemUrl);

        var quantity = await _api.GetBuyOrderQuantityAsync(itemPage.ItemUrl);
        var price = await _api.GetBuyOrderPriceAsync(itemPage.ItemUrl);

        if (quantity.HasValue && price.HasValue)
        {
            itemPage.MyBuyOrder = new Order
            {
                EngItemName = itemPage.EngItemName,
                RusItemName = itemPage.RusItemName,
                ItemUrl = itemPage.ItemUrl,
                BuyPrice = price.Value,
                Quantity = quantity.Value
            };
        }

        var fromDate = DateTime.Now.AddDays(-_configurationService.AnalysisIntervalDays);
        itemPage.SalesChart = await _api.GetChartAsync(itemPage.ItemUrl, fromDate);

        itemPage.SellOrderBook = await _api.GetSellOrdersBookAsync(itemPage.ItemUrl, _configurationService.SellListingFindRange);

        var salesPerDay = itemPage.SalesChart
            .GroupBy(x => x.Date.Date)
            .Select(x => x.Sum(y => y.Quantity))
            .Average();

        itemPage.BuyOrderBook = (await _api.GetBuyOrdersBookAsync(itemPage.ItemUrl, 5))
            .OrderByDescending(x => x.Price)
            .Aggregate(new List<OrderBookItem>(), (resultOrderBook, orderBookItem) =>
            {
                var salesToCompare = resultOrderBook.Sum(x => x.Quantity);
                if (salesToCompare < salesPerDay * _configurationService.SalesRatio)
                    resultOrderBook.Add(orderBookItem);
                return resultOrderBook;
            });
        itemPage.CurrentBalance = await _api.GetBalanceAsync();
        return itemPage;
    }
}