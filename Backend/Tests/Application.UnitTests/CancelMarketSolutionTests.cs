using Microsoft.EntityFrameworkCore;
using Moq;
using SteamTradeBot.Backend.Application.Solutions;
using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.ItemPageAggregate;
using SteamTradeBot.Backend.Domain.OrderAggregate;
using SteamTradeBot.Backend.Infrastructure;

namespace Application.UnitTests;

public class CancelMarketSolutionTests
{
    private readonly CancelMarketSolution _sut;

    private readonly Mock<ISteamApi> _steamApiMock;
    private readonly Mock<IConfigurationService> _configurationServiceMock;
    private readonly Mock<ITradingEventHandler> _tradingEventHandlerMock;
    private readonly Mock<IOrdersRepository> _ordersRepositoryMock;

    public CancelMarketSolutionTests()
    {
        _steamApiMock = new Mock<ISteamApi>();
        _configurationServiceMock = new Mock<IConfigurationService>();
        _tradingEventHandlerMock = new Mock<ITradingEventHandler>();

        var contextMock = new Mock<IDbContextFactory<TradeBotDataContext>>();
        _ordersRepositoryMock = new Mock<IOrdersRepository>(contextMock.Object);

        _sut = new CancelMarketSolution(_steamApiMock.Object, _configurationServiceMock.Object,
            _tradingEventHandlerMock.Object, _ordersRepositoryMock.Object);
    }

    [Fact]
    public async Task Perform_MyOrderIsNull()
    {
        // Arrange
        const string testItemUrl = "TestUrl1";
        const string testEngItemName = "TestItem1";
        const string testRusItemName = "ТестовыйПредмет1";
        const double estimatedBuyPrice = 10.0;
        const double estimatedSellPrice = 13.0;

        var testItemPage = new ItemPage
        {
            EngItemName = testEngItemName,
            RusItemName = testRusItemName,
            EstimatedBuyPrice = estimatedBuyPrice,
            EstimatedSellPrice = estimatedSellPrice,
            ItemUrl = testItemUrl
        };

        // Act, Assert
        await Assert.ThrowsAsync<Exception>(async () => await _sut.PerformAsync(testItemPage));
    }

    [Fact]
    public async Task Perform_StoredOrderIsNull()
    {
        // Arrange
        var apiKey = Guid.NewGuid().ToString();
        const OrderType storedOrderType = OrderType.BuyOrder;
        const string testItemUrl = "TestUrl1";
        const string testEngItemName = "TestItem1";
        const string testRusItemName = "ТестовыйПредмет1";
        const double estimatedBuyPrice = 10.0;
        const double estimatedSellPrice = 13.0;
        const Order nullOrder = null;

        var testItemPage = new ItemPage
        {
            EngItemName = testEngItemName,
            RusItemName = testRusItemName,
            EstimatedBuyPrice = estimatedBuyPrice,
            EstimatedSellPrice = estimatedSellPrice,
            ItemUrl = testItemUrl
        };

        _configurationServiceMock
            .Setup(x => x.ApiKey)
            .Returns(apiKey);

        _ordersRepositoryMock
            .Setup(x => x.GetOrderAsync(testEngItemName, apiKey, storedOrderType))
            .ReturnsAsync(nullOrder);

        // Act, Assert
        await Assert.ThrowsAsync<Exception>(async () => await _sut.PerformAsync(testItemPage));
    }

    [Fact]
    public async Task Perform()
    {
        #region Arrange

        const int storedOrderQuantity = 1;
        const OrderType storedOrderType = OrderType.BuyOrder;

        const string testItemUrl = "TestUrl1";
        const string testEngItemName = "TestItem1";
        const string testRusItemName = "ТестовыйПредмет1";
        const double estimatedBuyPrice = 10.0;
        const double estimatedSellPrice = 13.0;

        var apiKey = Guid.NewGuid().ToString();

        var boughtItemPage = new ItemPage
        {
            EngItemName = testEngItemName,
            RusItemName = testRusItemName,
            ItemUrl = testItemUrl,
            MyBuyOrder = new Order
            {
                ItemUrl = testItemUrl,
                EngItemName = testEngItemName,
                RusItemName = testRusItemName,
                Quantity = storedOrderQuantity,
                BuyPrice = estimatedBuyPrice
            }
        };

        var storedBuyOrder = new Order
        {
            ApiKey = apiKey,
            EngItemName = testEngItemName,
            RusItemName = testRusItemName,
            OrderType = storedOrderType,
            BuyPrice = estimatedBuyPrice,
            SellPrice = estimatedSellPrice,
            Quantity = storedOrderQuantity
        };

        _configurationServiceMock
            .Setup(x => x.ApiKey)
            .Returns(apiKey);

        _ordersRepositoryMock
            .Setup(x => x.GetOrderAsync(testEngItemName, apiKey, storedOrderType))
            .ReturnsAsync(storedBuyOrder);

        _steamApiMock
            .Setup(x => x.CancelBuyOrderAsync(boughtItemPage.MyBuyOrder.ItemUrl))
            .ReturnsAsync(true);

        #endregion

        #region Act

        await _sut.PerformAsync(boughtItemPage);

        #endregion

        #region Assert

        _ordersRepositoryMock.Verify(x =>
            x.GetOrderAsync(testEngItemName, apiKey, storedOrderType), Times.Once);

        _steamApiMock.Verify(x =>
            x.CancelBuyOrderAsync(boughtItemPage.MyBuyOrder.ItemUrl), Times.Once);

        _ordersRepositoryMock.Verify(x =>
            x.RemoveOrderAsync(It.Is<Order>(order => OrderEquals(order, storedBuyOrder))), Times.Once);

        _tradingEventHandlerMock.Verify(x =>
            x.OnItemCancellingAsync(It.Is<Order>(order => OrderEquals(order, storedBuyOrder))), Times.Once);

        #endregion
    }

    private static bool OrderEquals(Order leftOrder, Order rightOrder)
    {
        return leftOrder.EngItemName == rightOrder.EngItemName &&
               leftOrder.RusItemName == rightOrder.RusItemName &&
               leftOrder.ApiKey == rightOrder.ApiKey &&
               double.Abs(leftOrder.BuyPrice - rightOrder.BuyPrice) < 0.01 &&
               double.Abs(leftOrder.SellPrice - rightOrder.SellPrice) < 0.01 &&
               leftOrder.Quantity == rightOrder.Quantity &&
               leftOrder.OrderType == rightOrder.OrderType;
    }
}