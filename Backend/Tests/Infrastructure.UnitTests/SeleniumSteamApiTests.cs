using OpenQA.Selenium.Chrome;
using SteamTradeBot.Backend.Infrastructure.SteamConnectors.Selenium;

namespace Infrastructure.UnitTests;

public class SeleniumSteamApiTests : IClassFixture<SeleniumSteamApiFixture>
{
    private readonly SeleniumSteamApiFixture _fixture;

    public SeleniumSteamApiTests(SeleniumSteamApiFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestGetBalanceAsync()
    {
        var balance = await _fixture.Sut.GetBalanceAsync();
        Assert.NotEqual(0, balance);
    }

    #region OrderBooks

    [Fact]
    public async Task TestGetBuyOrdersBookAsync()
    {
        // Arrange

        // Act
        var orderBook = await _fixture.Sut.GetBuyOrdersBookAsync(_fixture.TestBuyItemUrl, 5);

        // Assert
        Assert.NotNull(orderBook);
        Assert.NotEmpty(orderBook);
        Assert.Equal(5, orderBook.Count);
    }

    [Fact]
    public async Task TestGetSellOrdersBookAsync()
    {
        // Arrange

        // Act
        var orderBook = await _fixture.Sut.GetSellOrdersBookAsync(_fixture.TestSellItemUrl, 1);

        // Assert
        Assert.NotNull(orderBook);
        Assert.NotEmpty(orderBook);
        Assert.Equal(10, orderBook.Sum(x => x.Quantity));
    }

    #endregion

    #region Chart

    [Fact]
    public async Task TestGetChart_WeekBefore()
    {
        // Arrange
        const int daysBefore = 7;

        // Act
        var chart = await _fixture.Sut.GetChartAsync(_fixture.TestBuyItemUrl, DateTime.Now.AddDays(-daysBefore));

        // Assert
        Assert.NotNull(chart);
        Assert.NotEmpty(chart);
        Assert.Equal(daysBefore, chart.GroupBy(x => x.Date.Day).Count());
    }

    [Fact]
    public async Task TestGetChart_MonthBefore()
    {
        // Arrange
        const int monthsBefore = 1;
        var fromDate = DateTime.Now.AddMonths(-monthsBefore);

        // Act
        var chart = await _fixture.Sut.GetChartAsync(_fixture.TestBuyItemUrl, fromDate);

        // Assert
        Assert.NotNull(chart);
        Assert.NotEmpty(chart);

        var expectedDays = (DateTime.Now - DateTime.Now.AddMonths(-monthsBefore)).Days + 1;
        var actualDays = chart.GroupBy(x => x.Date.Day).Count();
        Assert.Equal(expectedDays, actualDays);
    }

    #endregion

    #region SellOrder

    [Fact]
    public async Task TestSellOrder()
    {
        // Arrange
        const double sellPrice = 10000.0;

        // Act
        var successfullyPlaced = await _fixture.Sut.PlaceSellOrderAsync(_fixture.TestSellItemName, sellPrice, _fixture.TestUserId);

        var placedSellOrderPrice = await _fixture.Sut.GetSellOrderPriceAsync(_fixture.TestSellItemUrl);

        var successfullyCanceled = await _fixture.Sut.CancelSellOrderAsync(_fixture.TestSellItemUrl);

        // Assert
        Assert.True(successfullyPlaced);
        Assert.True(successfullyCanceled);
        Assert.Equal(sellPrice, placedSellOrderPrice);
    }

    #endregion

    #region BuyOrder

    [Fact]
    public async Task TestBuyOrder()
    {
        // Arrange
        const double buyPrice = 1.0;
        const int buyQuantity = 2;
        const double additionalValue = 0.01;

        // Act
        var successfullyPlaced = await _fixture.Sut.PlaceBuyOrderAsync(_fixture.TestBuyItemUrl, buyPrice, buyQuantity);

        var placedBuyOrderPrice = await _fixture.Sut.GetBuyOrderPriceAsync(_fixture.TestBuyItemUrl);
        var placedBuyOrderQuantity = await _fixture.Sut.GetBuyOrderQuantityAsync(_fixture.TestBuyItemUrl);

        var successfullyCanceled = await _fixture.Sut.CancelBuyOrderAsync(_fixture.TestBuyItemUrl);

        // Assert
        Assert.True(successfullyPlaced);

        Assert.Equal(buyPrice + additionalValue, placedBuyOrderPrice);
        Assert.Equal(buyQuantity, placedBuyOrderQuantity);

        Assert.True(successfullyCanceled);
    }

    #endregion
}

public class SeleniumSteamApiFixture : IDisposable
{
    internal readonly SeleniumSteamApi Sut;
    public readonly string TestBuyItemUrl;
    public readonly string TestSellItemUrl;
    public readonly string TestUserId;
    public readonly string TestSellItemName;

    public SeleniumSteamApiFixture()
    {
        Sut = new SeleniumSteamApi(new SeleniumWebDriver(() =>
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--disable-gpu");
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddArgument("--disable-setuid-sandbox");
            chromeOptions.AddArgument("--disable-dev-shm-usage");
            chromeOptions.AddArgument("--start-maximized");
            chromeOptions.AddArgument("--window-size=1920,1080");
            chromeOptions.AddArgument("--headless");
            chromeOptions.AddArgument("--disable-logging");
            chromeOptions.AddArgument("--log-level=3");
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.EnableVerboseLogging = false;
            driverService.SuppressInitialDiagnosticInformation = true;
            return new ChromeDriver(driverService, chromeOptions);
        }));

        var configuration = ConfigurationProvider.GetConfiguration();

        Sut.LogIn(configuration["TestLogin"], configuration["TestPassword"], configuration["TestSecret"]).GetAwaiter().GetResult();

        TestBuyItemUrl = configuration["TestBuyItemUrl"];
        TestSellItemUrl = configuration["TestSellItemUrl"];
        TestUserId = configuration["TestUserId"];
        TestSellItemName = configuration["TestSellItemName"];
    }

    public void Dispose()
    {
        Sut.Dispose();
    }
}