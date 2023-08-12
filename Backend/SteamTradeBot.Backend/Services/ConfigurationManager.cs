using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Serilog;
using SteamTradeBot.Backend.Models.Configuration;

namespace SteamTradeBot.Backend.Services;

public class ConfigurationManager
{
    private readonly IOptionsSnapshot<UserConfigurations> _userConfigurationsOptions;
    private string _username;

    public ConfigurationManager(IOptionsSnapshot<UserConfigurations> userConfigurationsOptions)
    {
        _userConfigurationsOptions = userConfigurationsOptions;
    }

    public void SetForUser(string username, Configuration? configuration = null)
    {
        _userConfigurationsOptions.Value.Configurations.Add(new UserConfiguration(){ User = username, Configuration = configuration });
        //var userConfigurations = _userConfigurationsOptions.Value.Configurations.ToDictionary(x=> x.User, y => y.Configuration);
        //if (!userConfigurations.ContainsKey(username))
        //{
        //    if (configuration is null)
        //        throw new ArgumentException("Configuration does not exist and not provided.");
        //    userConfigurations.Add(username, configuration);
        //}
        _username = username;
    }

    public Configuration GetConfiguration()
    {
        if (string.IsNullOrEmpty(_username))
            throw new Exception("Can't get configuration for 'unknown user'");

        var userConfigurations = _userConfigurationsOptions.Value.Configurations.ToDictionary(x => x.User, y => y.Configuration);
        return userConfigurations[_username];
    }

    public bool CheckIntegrity()
    {
        Log.Information("Check configuration integrity...");
        try
        {
            var configurationForCheck = GetConfiguration();

            //if (orderQuantity is <= 0 or > 10)
            //{
            //    Log.Fatal("Quantity can not be less than 0 or greater than 10!");
            //    return false;
            //}

            //if (salesPerWeek <= 0)
            //{
            //    Log.Fatal("Sales can not be less than 0!");
            //    return false;
            //}

            //if (availableBalance is <= 0.0 or > 1.0)
            //{
            //    Log.Fatal("Available balance range from 0.0 to 1.0");
            //    return false;
            //}

            //if (steamCommission is <= 0.0 or > 1.0)
            //{
            //    Log.Fatal("Steam commission range from 0.0 to 1.0");
            //    return false;
            //}

            Log.Information("Check configuration integrity -> OK");
            return true;
        }
        catch (Exception e)
        {
            Log.Fatal("Configuration error:\r\nMessage: {0}\r\nStackTrace: {1}", e.Message, e.StackTrace);
            return false;
        }
    }

    public void RefreshConfiguration(Configuration configuration)
    {
        Log.Logger.Information("Start settings update...");
        if (string.IsNullOrEmpty(_username))
            throw new Exception("Can't refresh configuration for 'unknown user'");

        var userConfigurations = _userConfigurationsOptions.Value.Configurations.ToDictionary(x => x.User, y => y.Configuration);
        userConfigurations[_username] = configuration;

        Log.Logger.Information("Settings have been updated!");
    }
}