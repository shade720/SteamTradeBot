namespace SteamTradeBot.Desktop.Winforms.Models;

public class StateInfo
{
    public ConnectionState Connection { get; set; }
    public ServiceState Service { get; set; }
    public int ItemsAnalyzed { get; set; }
    public int ItemsBought { get; set; }
    public int ItemsSold { get; set; }
    public int Errors { get; set; }
    public int Warnings { get; set; }
    public DateTime Uptime { get; set; }
    
    public enum ServiceState
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