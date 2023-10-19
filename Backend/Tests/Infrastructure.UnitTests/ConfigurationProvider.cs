using Microsoft.Extensions.Configuration;

namespace Infrastructure.UnitTests;

internal class ConfigurationProvider
{
    public static IConfiguration GetConfiguration()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json")
            .Build();
        return config;
    }
}