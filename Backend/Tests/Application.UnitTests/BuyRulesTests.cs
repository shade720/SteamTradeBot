using Moq;
using SteamTradeBot.Backend.Application.Rules.BuyRules;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Domain.Abstractions.Rules;
using SteamTradeBot.Backend.Domain.ItemPageAggregate;

namespace Application.UnitTests;

public class AvailableBalanceRuleTests
{
    private readonly IBuyRule _sut;

    private readonly Mock<IConfigurationService> _configurationServiceMock;
    private readonly Mock<IOrdersRepository> _ordersRepositoryMock;

    public AvailableBalanceRuleTests()
    {
        _configurationServiceMock = new Mock<IConfigurationService>();
        _ordersRepositoryMock = new Mock<IOrdersRepository>();

        _sut = new AvailableBalanceRule(_configurationServiceMock.Object, _ordersRepositoryMock.Object);
    }

    [Fact]
    public async Task IsFollowedAsync()
    {
        // Arrange
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


        // Act

        // Assert
    }
}