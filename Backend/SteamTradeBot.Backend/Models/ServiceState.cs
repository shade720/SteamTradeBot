using System.Collections.Generic;
using System;

namespace SteamTradeBot.Backend.Models;

public class ServiceState
{
    public ServiceWorkingState WorkingState { get; set; } = ServiceWorkingState.Down;
    public string CurrentUser { get; set; } = string.Empty;
    public int ItemsAnalyzed { get; set; }
    public int ItemsBought { get; set; }
    public int ItemsSold { get; set; }
    public int ItemCanceled { get; set; }
    public int Errors { get; set; }
    public int Warnings { get; set; }
    public TimeSpan Uptime { get; set; }
    public List<string> Events { get; set; } = new();

    public enum ServiceWorkingState
    {
        Up,
        Down
    }
}