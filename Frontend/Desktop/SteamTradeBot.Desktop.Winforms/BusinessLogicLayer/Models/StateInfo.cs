namespace SteamTradeBot.Desktop.Winforms.BusinessLogicLayer.Models;

public class StateInfo
{
    public ConnectionState Connection { get; set; }
    public ServiceWorkingState WorkingState { get; set; }
    public LogInState IsLoggedIn { get; set; }
    public string CurrentUser { get; set; }
    public int ItemsAnalyzed { get; set; }
    public int ItemsBought { get; set; }
    public int ItemsSold { get; set; }
    public int Errors { get; set; }
    public int Warnings { get; set; }
    public TimeSpan Uptime { get; set; }

    public enum ServiceWorkingState
    {
        Down,
        Up,
    }

    public enum ConnectionState
    {
        Connected,
        Disconnected,
    }
    public enum LogInState
    {
        NotLoggedIn,
        Pending,
        LoggedIn,
    }
}