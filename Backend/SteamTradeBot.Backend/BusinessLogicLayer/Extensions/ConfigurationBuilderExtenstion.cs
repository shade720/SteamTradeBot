using Microsoft.Extensions.Configuration;
using System.IO;
using System;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Extensions;

public static class ConfigurationBuilderExtenstion
{
    private const string UserSettingsFolderName = "UserSettings";
    private static readonly string UserSettingsFolderPath = Path.Combine(Environment.CurrentDirectory, UserSettingsFolderName);

    public static void AddUsersConfigurations(this ConfigurationManager configurationBuilder)
    {
        if (!Directory.Exists(UserSettingsFolderPath))
            Directory.CreateDirectory(UserSettingsFolderPath);
        var builder = configurationBuilder.SetBasePath(Environment.CurrentDirectory);
        foreach (var configFilePath in Directory.GetFiles(UserSettingsFolderPath,"*.json", SearchOption.AllDirectories))
        {
            builder.AddJsonFile(configFilePath, true, true);
        }
    }
}