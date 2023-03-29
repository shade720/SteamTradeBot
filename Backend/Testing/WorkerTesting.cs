using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json.Linq;
using SteamTradeBotService.BusinessLogicLayer;
using SteamTradeBotService.BusinessLogicLayer.Database;
using Xunit;

namespace Testing;

public class WorkerTesting : IClassFixture<WorkerFixture>
{ 
    private readonly WorkerFixture _fixture;

    public WorkerTesting(WorkerFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void StartWorker()
    {
        _fixture.Sut.StartWork();
        Thread.Sleep(60000);
    }

    [Fact]
    public void StopWorker()
    {
        _fixture.Sut.StopWork();
    }
}

public class WorkerFixture : IDisposable
{
    private const string ConnectionString = "Host=localhost;Port=32768;Database=MarketDatabase;Username=postgres;Password=postgrespw";
    public Worker Sut { get; }
    private readonly SteamAPI _steamAPI;
    private readonly DatabaseClient _database;
    private const string Login = "mihaylyukov001";
    private const string Password = "YtnGfhjkz132@";
    private const string UserId = "76561198081058441";

    public WorkerFixture()
    {
        var mockDbFactory = new Mock<IDbContextFactory<MarketDataContext>>();
        mockDbFactory
            .Setup(f => f.CreateDbContext())
            .Returns(() => new MarketDataContext(new DbContextOptionsBuilder<MarketDataContext>()
                .UseNpgsql(ConnectionString)
                .Options));
        IConfiguration config = new ConfigurationBuilder()
            .Build();
        _database = new DatabaseClient(mockDbFactory.Object);
        _steamAPI = new SteamAPI();
        var myJObject = JObject.Parse(File.ReadAllText("76561198081058441.maFile"));
        var secret = myJObject.SelectToken("shared_secret")?.Value<string>();
        var token = _steamAPI.GetToken(secret);
        _steamAPI.LogIn(Login, Password, token);

        var configuredItems = _database.GetItems().Select(item => item.ConfigureServiceProperties(config, _steamAPI));
        Sut = new Worker(configuredItems.ToList());
    }

    public void Dispose()
    {
        Sut.StopWork();
        _steamAPI.LogOut();
        _steamAPI.Dispose();
    }
}