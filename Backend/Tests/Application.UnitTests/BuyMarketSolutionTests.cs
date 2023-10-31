using Microsoft.EntityFrameworkCore;
using Moq;
using SteamTradeBot.Backend.Application.Solutions;
using SteamTradeBot.Backend.Domain;
using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.ItemModel;

namespace Application.UnitTests;

public class BuyMarketSolutionTests
{
    private readonly BuyMarketSolution _sut;

    private readonly Mock<ISteamApi> _steamApiMock;
    private readonly Mock<IConfigurationService> _configurationServiceMock;
    private readonly Mock<ITradingEventHandler> _tradingEventHandlerMock;
    private readonly Mock<OrdersRepository> _ordersRepositoryMock;

    public BuyMarketSolutionTests()
    {
        _steamApiMock = new Mock<ISteamApi>();
        _configurationServiceMock = new Mock<IConfigurationService>();
        _tradingEventHandlerMock = new Mock<ITradingEventHandler>();

        var contextMock = new Mock<IDbContextFactory<TradeBotDataContext>>();
        _ordersRepositoryMock = new Mock<OrdersRepository>(contextMock.Object);

        _sut = new BuyMarketSolution(_steamApiMock.Object, _configurationServiceMock.Object,
            _tradingEventHandlerMock.Object, _ordersRepositoryMock.Object);
    }

    [Fact]
    public async Task Perform()
    {
        // Arrange
        var testGuid = Guid.NewGuid();
        const int expectedQuantity = 1;
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

        var testOrder = new Order
        {
            ApiKey = testGuid.ToString(),
            EngItemName = testEngItemName,
            RusItemName = testRusItemName,
            ItemUrl = testItemUrl,
            OrderType = OrderType.BuyOrder,
            BuyPrice = estimatedBuyPrice,
            SellPrice = estimatedSellPrice,
            Quantity = expectedQuantity
        };

        _configurationServiceMock
            .Setup(x => x.ApiKey)
            .Returns(testGuid.ToString());
        _configurationServiceMock.Setup(x => x.OrderQuantity)
            .Returns(expectedQuantity);

        // Act
        await _sut.PerformAsync(testItemPage);

        // Assert
        _steamApiMock.Verify(x => x.PlaceBuyOrderAsync(testOrder.ItemUrl, testOrder.BuyPrice, testOrder.Quantity), Times.Once);
        _ordersRepositoryMock.Verify(x => x.AddOrUpdateOrderAsync(It.Is<Order>(order => OrderEquals(order, testOrder))), Times.Once);
        _tradingEventHandlerMock.Verify(x => x.OnItemBuyingAsync(It.Is<Order>(order => OrderEquals(order, testOrder))), Times.Once);
    }

    [Fact]
    public async Task Perform_WithNullPriceOrQuantity()
    {
        // Arrange

        var testItemPage1 = new ItemPage
        {
            EstimatedBuyPrice = null,
            EstimatedSellPrice = 10,
        };
        var testItemPage2 = new ItemPage
        {
            EstimatedBuyPrice = 10,
            EstimatedSellPrice = null,
        };
        var testItemPage3 = new ItemPage
        {
            EstimatedBuyPrice = null,
            EstimatedSellPrice = null,
        };

        // Act, Assert
        await Assert.ThrowsAsync<Exception>(async () => await _sut.PerformAsync(testItemPage1));
        await Assert.ThrowsAsync<Exception>(async () => await _sut.PerformAsync(testItemPage2));
        await Assert.ThrowsAsync<Exception>(async () => await _sut.PerformAsync(testItemPage3));
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