using Microsoft.EntityFrameworkCore;
using Moq;
using SteamTradeBot.Backend.Application.Solutions;
using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.ItemPageAggregate;
using SteamTradeBot.Backend.Domain.OrderAggregate;
using SteamTradeBot.Backend.Infrastructure;

namespace Application.UnitTests;

public class SellMarketSolutionTests
{
    private readonly SellMarketSolution _sut;

    private readonly Mock<ISteamApi> _steamApiMock;
    private readonly Mock<IConfigurationService> _configurationServiceMock;
    private readonly Mock<ITradingEventHandler> _tradingEventHandlerMock;
    private readonly Mock<IOrdersRepository> _ordersRepositoryMock;

    public SellMarketSolutionTests()
    {
        _steamApiMock = new Mock<ISteamApi>();
        _configurationServiceMock = new Mock<IConfigurationService>();
        _tradingEventHandlerMock = new Mock<ITradingEventHandler>();

        var contextMock = new Mock<IDbContextFactory<TradeBotDataContext>>();
        _ordersRepositoryMock = new Mock<IOrdersRepository>(contextMock.Object);

        _sut = new SellMarketSolution(_steamApiMock.Object, _configurationServiceMock.Object,
            _tradingEventHandlerMock.Object, _ordersRepositoryMock.Object);
    }

    [Fact]
    public async Task Perform_CallPlaceSellOrderOnce()
    {
        #region Arrange

        const int storedOrderQuantity = 1;
        const OrderType storedOrderType = OrderType.BuyOrder;

        const string testItemUrl = "TestUrl1";
        const string testEngItemName = "TestItem1";
        const string testRusItemName = "ТестовыйПредмет1";
        const double estimatedBuyPrice = 10.0;
        const double estimatedSellPrice = 13.0;
        const int defaultInventoryFindRange = 10;

        var apiKey = Guid.NewGuid().ToString();
        var testSteamUserId = ConfigurationProvider.GetConfiguration()["TestUserId"];

        var boughtItemPage = new ItemPage
        {
            EngItemName = testEngItemName,
            RusItemName = testRusItemName,
            ItemUrl = testItemUrl,
            MyBuyOrder = null
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

        var expectedSellOrder = new Order
        {
            ApiKey = apiKey,
            EngItemName = boughtItemPage.EngItemName,
            RusItemName = boughtItemPage.RusItemName,
            OrderType = OrderType.SellOrder,
            BuyPrice = storedBuyOrder.BuyPrice,
            SellPrice = storedBuyOrder.SellPrice,
            Quantity = 1
        };

        _configurationServiceMock
            .Setup(x => x.ApiKey)
            .Returns(apiKey);
        _configurationServiceMock
            .Setup(x => x.SteamUserId)
            .Returns(testSteamUserId);

        _ordersRepositoryMock
            .Setup(x => x.GetOrderAsync(testEngItemName, apiKey, storedOrderType))
            .ReturnsAsync(storedBuyOrder);

        _steamApiMock
            .Setup(x => x.PlaceSellOrderAsync(testEngItemName, expectedSellOrder.SellPrice, testSteamUserId, defaultInventoryFindRange))
            .ReturnsAsync(true);

        #endregion

        #region Act

        await _sut.PerformAsync(boughtItemPage);

        #endregion

        #region Assert

        _ordersRepositoryMock.Verify(x => 
            x.GetOrderAsync(testEngItemName, apiKey, storedOrderType), Times.Once);

        _steamApiMock.Verify(x => 
            x.PlaceSellOrderAsync(expectedSellOrder.EngItemName, expectedSellOrder.SellPrice, testSteamUserId, defaultInventoryFindRange), Times.Once);

        _ordersRepositoryMock.Verify(x => 
            x.RemoveOrderAsync(It.Is<Order>(order => OrderEquals(order, storedBuyOrder))), Times.Once);

        _ordersRepositoryMock.Verify(x => 
            x.AddOrUpdateOrderAsync(It.Is<Order>(order => OrderEquals(order, expectedSellOrder))), Times.Once);

        _tradingEventHandlerMock.Verify(x => 
            x.OnItemSellingAsync(It.Is<Order>(order => OrderEquals(order, expectedSellOrder))), Times.Once);

        #endregion
    }

    [Fact]
    public async Task Perform_CallPlaceSellOrderThrice()
    {
        #region Arrange

        const int storedOrderQuantity = 3;
        const OrderType storedOrderType = OrderType.BuyOrder;

        const string testItemUrl = "TestUrl1";
        const string testEngItemName = "TestItem1";
        const string testRusItemName = "ТестовыйПредмет1";
        const double estimatedBuyPrice = 10.0;
        const double estimatedSellPrice = 13.0;
        const int defaultInventoryFindRange = 10;

        var apiKey = Guid.NewGuid().ToString();
        var testSteamUserId = ConfigurationProvider.GetConfiguration()["TestUserId"];

        var boughtItemPage = new ItemPage
        {
            EngItemName = testEngItemName,
            RusItemName = testRusItemName,
            ItemUrl = testItemUrl,
            MyBuyOrder = null
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

        var expectedSellOrder = new Order
        {
            ApiKey = apiKey,
            EngItemName = boughtItemPage.EngItemName,
            RusItemName = boughtItemPage.RusItemName,
            OrderType = OrderType.SellOrder,
            BuyPrice = storedBuyOrder.BuyPrice,
            SellPrice = storedBuyOrder.SellPrice,
            Quantity = 1
        };

        _configurationServiceMock
            .Setup(x => x.ApiKey)
            .Returns(apiKey);
        _configurationServiceMock
            .Setup(x => x.SteamUserId)
            .Returns(testSteamUserId);

        _ordersRepositoryMock
            .Setup(x => x.GetOrderAsync(testEngItemName, apiKey, storedOrderType))
            .ReturnsAsync(storedBuyOrder);

        _steamApiMock
            .Setup(x => x.PlaceSellOrderAsync(testEngItemName, expectedSellOrder.SellPrice, testSteamUserId, defaultInventoryFindRange))
            .ReturnsAsync(true);

        #endregion

        #region Act

        await _sut.PerformAsync(boughtItemPage);

        #endregion

        #region Assert

        _ordersRepositoryMock.Verify(x =>
            x.GetOrderAsync(testEngItemName, apiKey, storedOrderType), Times.Once);

        _steamApiMock.Verify(x =>
            x.PlaceSellOrderAsync(expectedSellOrder.EngItemName, expectedSellOrder.SellPrice, testSteamUserId, defaultInventoryFindRange), Times.Exactly(3));

        _ordersRepositoryMock.Verify(x =>
            x.AddOrUpdateOrderAsync(It.Is<Order>(order => OrderEquals(order, storedBuyOrder))), Times.Exactly(2));

        _ordersRepositoryMock.Verify(x =>
            x.RemoveOrderAsync(It.Is<Order>(order => OrderEquals(order, storedBuyOrder))), Times.Once);

        _ordersRepositoryMock.Verify(x =>
            x.AddOrUpdateOrderAsync(It.Is<Order>(order => OrderEquals(order, expectedSellOrder))), Times.Exactly(3));

        _tradingEventHandlerMock.Verify(x =>
            x.OnItemSellingAsync(It.Is<Order>(order => OrderEquals(order, expectedSellOrder))), Times.Exactly(3));

        #endregion
    }

    [Fact]
    public async Task Perform_CallPlaceSellOrderTwice()
    {
        #region Arrange

        const int storedOrderQuantity = 3;
        const OrderType storedOrderType = OrderType.BuyOrder;

        const string testItemUrl = "TestUrl1";
        const string testEngItemName = "TestItem1";
        const string testRusItemName = "ТестовыйПредмет1";
        const double estimatedBuyPrice = 10.0;
        const double estimatedSellPrice = 13.0;
        const int defaultInventoryFindRange = 10;

        var apiKey = Guid.NewGuid().ToString();
        var testSteamUserId = ConfigurationProvider.GetConfiguration()["TestUserId"];

        var boughtItemPage = new ItemPage
        {
            EngItemName = testEngItemName,
            RusItemName = testRusItemName,
            ItemUrl = testItemUrl,
            MyBuyOrder = new Order {Quantity = 1}
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

        var expectedSellOrder = new Order
        {
            ApiKey = apiKey,
            EngItemName = boughtItemPage.EngItemName,
            RusItemName = boughtItemPage.RusItemName,
            OrderType = OrderType.SellOrder,
            BuyPrice = storedBuyOrder.BuyPrice,
            SellPrice = storedBuyOrder.SellPrice,
            Quantity = 1
        };

        _configurationServiceMock
            .Setup(x => x.ApiKey)
            .Returns(apiKey);
        _configurationServiceMock
            .Setup(x => x.SteamUserId)
            .Returns(testSteamUserId);

        _ordersRepositoryMock
            .Setup(x => x.GetOrderAsync(testEngItemName, apiKey, storedOrderType))
            .ReturnsAsync(storedBuyOrder);

        _steamApiMock
            .Setup(x => x.PlaceSellOrderAsync(testEngItemName, expectedSellOrder.SellPrice, testSteamUserId, defaultInventoryFindRange))
            .ReturnsAsync(true);

        #endregion

        #region Act

        await _sut.PerformAsync(boughtItemPage);

        #endregion

        #region Assert

        _ordersRepositoryMock.Verify(x =>
            x.GetOrderAsync(testEngItemName, apiKey, storedOrderType), Times.Once);

        _steamApiMock.Verify(x =>
            x.PlaceSellOrderAsync(expectedSellOrder.EngItemName, expectedSellOrder.SellPrice, testSteamUserId, defaultInventoryFindRange), Times.Exactly(2));

        _ordersRepositoryMock.Verify(x =>
            x.AddOrUpdateOrderAsync(It.Is<Order>(order => OrderEquals(order, storedBuyOrder))), Times.Exactly(2));

        _ordersRepositoryMock.Verify(x =>
            x.AddOrUpdateOrderAsync(It.Is<Order>(order => OrderEquals(order, expectedSellOrder))), Times.Exactly(2));

        _tradingEventHandlerMock.Verify(x =>
            x.OnItemSellingAsync(It.Is<Order>(order => OrderEquals(order, expectedSellOrder))), Times.Exactly(2));

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