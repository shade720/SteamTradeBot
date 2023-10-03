namespace SteamTradeBot.Desktop.Winforms.BusinessLogicLayer.Models;

public class LogInfo
{
    public DateTime Date { get; init; }
    public LogLevel Level { get; init; }
    public string Message { get; init; }
}

public enum LogLevel
{
    Fatal,
    Error,
    Warning,
    Information
}