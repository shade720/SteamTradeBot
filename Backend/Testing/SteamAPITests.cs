using Newtonsoft.Json.Linq;
using SteamTradeBotService.BusinessLogicLayer;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Testing;

[TestCaseOrderer("Testing.PriorityOrderer", "Testing")]
public class SteamAPITests : IClassFixture<SteamAPIFixture>
{
    private readonly SteamAPIFixture _fixture;
    private const string ItemUrl = "https://steamcommunity.com/market/listings/730/AK-47%20%7C%20Ice%20Coaled%20%28Minimal%20Wear%29";
    private const string SellItemUrl = "https://steamcommunity.com/market/listings/730/P90%20%7C%20Teardown%20%28Field-Tested%29";
    private const string SellItemRusName = "P90 | Демонтаж (После полевых испытаний)";
    private const string Login = "mihaylyukov001";
    private const string Password = "YtnGfhjkz132@";
    private const string UserId = "76561198081058441";

    public SteamAPITests(SteamAPIFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact, TestPriority(15)]
    public void TestGetItemNamesList()
    {
        var result = _fixture.Sut.GetItemNamesList(1.0, 5.0, 700, 100).ToList();
        Assert.NotNull(result);
        Assert.True(result.All(string.IsNullOrEmpty));
        Assert.NotEmpty(result);
        Assert.Equal(100, result.Count);
    }

    [Fact, TestPriority(0)]
    public void LogInWithSecret()
    {
        var myJObject = JObject.Parse(File.ReadAllText("76561198081058441.maFile"));
        var secret = myJObject.SelectToken("shared_secret")?.Value<string>();
        var token = _fixture.Sut.GetToken(secret);
        _fixture.Sut.LogIn(Login, Password, token);
    }

    [Fact, TestPriority(1)]
    public void TestGetBalance()
    {
        var result = _fixture.Sut.GetBalance(ItemUrl);
        Assert.InRange(result, 300.0, 400.0);
    }

    [Fact]
    public void TestGetBuyOrderQuantity()
    {
        var result = _fixture.Sut.GetBuyOrderQuantity(ItemUrl);
        Assert.Equal(1, result);
    }

    [Fact]
    public void TestGetGraph()
    {
        var result = _fixture.Sut.GetGraph(ItemUrl);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void TestGetBuyOrdersBook()
    {
        var result = _fixture.Sut.GetBuyOrdersBook(ItemUrl);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(6, result.Count);
    }

    [Fact]
    public void TestGetSellOrdersBook()
    {
        var result = _fixture.Sut.GetSellOrdersBook(ItemUrl, 1);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(10, result.Sum(x => x.Quantity));
    }

    [Fact]
    public void TestGetSellOrdersBook_FindRange_2()
    {
        var result = _fixture.Sut.GetSellOrdersBook(ItemUrl, 2);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(20, result.Sum(x => x.Quantity));
    }

    [Fact]
    public void TestGetRusItemName()
    {
        var result = _fixture.Sut.GetRusItemName(ItemUrl);
        Assert.Equal("AK-47 | Ледяной уголь (Немного поношенное)", result);
    }

    [Fact, TestPriority(5)]
    public void TestPlaceBuyOrder()
    {
        var result = _fixture.Sut.PlaceBuyOrder(ItemUrl, 10, 1);
        var result1 = _fixture.Sut.CancelBuyOrder(ItemUrl);
        Assert.True(result);
        Assert.True(result1);
    }

    [Fact, TestPriority(5)]
    public void TestPlaceSellOrder()
    {
        var result = _fixture.Sut.PlaceSellOrder(SellItemRusName, 10000, UserId);
        var result1 = _fixture.Sut.CancelSellOrder(SellItemUrl);
        Assert.True(result);
        Assert.True(result1);
    }
}

public class SteamAPIFixture : IDisposable
{
    public SteamAPI Sut { get; }

    public SteamAPIFixture()
    {
        Sut = new SteamAPI();
    }

    public void Dispose()
    {
        Sut.Dispose();
    }
}

public class PriorityOrderer : ITestCaseOrderer
{
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
    {
        var sortedMethods = new SortedDictionary<int, List<TTestCase>>();

        foreach (var testCase in testCases)
        {
            var priority = 0;

            foreach (var attr in testCase.TestMethod.Method.GetCustomAttributes((typeof(TestPriorityAttribute).AssemblyQualifiedName)))
                priority = attr.GetNamedArgument<int>("Priority");

            GetOrCreate(sortedMethods, priority).Add(testCase);
        }

        foreach (var list in sortedMethods.Keys.Select(priority => sortedMethods[priority]))
        {
            list.Sort((x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.TestMethod.Method.Name, y.TestMethod.Method.Name));
            foreach (var testCase in list)
                yield return testCase;
        }
    }

    private static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
    {
        if (dictionary.TryGetValue(key, out var result)) return result;
        result = new TValue();
        dictionary[key] = result;
        return result;
    }
}
[AttributeUsage(AttributeTargets.Method)]
public class TestPriorityAttribute : Attribute
{
    public TestPriorityAttribute(int priority)
    {
        Priority = priority;
    }

    public int Priority { get; }
}