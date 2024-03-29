﻿using System.Text;
using Serilog;

namespace SteamTradeBot.Backend.Application.Services;

public class LogsProviderService
{
    private const string LogsPath = "Logs";

    public async Task<string> GetLogs(string apiKey)
    {
        Log.Logger.Information("Provide server logs...");
        var logFiles = Directory.GetFiles(LogsPath);
        var sb = new StringBuilder();
        foreach (var file in logFiles)
        {
            using var sr = new StreamReader(new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            sb.Append(await sr.ReadToEndAsync());
        }
        Log.Logger.Information("Server logs have been provided!");
        return sb.ToString();
    }
}