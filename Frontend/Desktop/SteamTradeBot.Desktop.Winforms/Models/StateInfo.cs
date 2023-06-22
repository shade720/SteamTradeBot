namespace SteamTradeBot.Desktop.Winforms.Models;

public class StateInfo
{
    public ConnectionState Connection { get; set; }
    public ServiceWorkingState WorkingState { get; set; }
    public int ItemsAnalyzed { get; set; }
    public int ItemsBought { get; set; }
    public int ItemsSold { get; set; }
    public int Errors { get; set; }
    public int Warnings { get; set; }
    public TimeSpan Uptime { get; set; }
    public List<string> Events { get; set; } = new();

    public enum ServiceWorkingState
    {
        Up,
        Down
    }

    public enum ConnectionState
    {
        Connected,
        Disconnected
    }
}