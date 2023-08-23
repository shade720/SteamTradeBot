using System;

namespace SteamTradeBot.Backend.Models.StateModel;

public class ServiceState
{
    public int Id { get; set; }
    public ServiceWorkingState WorkingState { get; set; } = ServiceWorkingState.Down;
    public LogInState IsLoggedIn { get; set; }
    public string ApiKey { get; set; }
    public int ItemsAnalyzed { get; set; }
    public int ItemsBought { get; set; }
    public int ItemsSold { get; set; }
    public int ItemCanceled { get; set; }
    public int Errors { get; set; }
    public int Warnings { get; set; }
    public TimeSpan Uptime { get; set; }
}