using System;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Models.DTOs;

public class ServiceState
{
    public int ItemsAnalyzed { get; set; }
    public int ItemsBought { get; set; }
    public int ItemsSold { get; set; }
    public int Errors { get; set; }
    public int Warnings { get; set; }
    public TimeSpan Uptime { get; set; }
}