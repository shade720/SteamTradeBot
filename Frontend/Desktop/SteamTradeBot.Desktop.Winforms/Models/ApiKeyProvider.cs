namespace SteamTradeBot.Desktop.Winforms.Models;

public class ApiKeyProvider
{
    public string GetApiKey()
    {
        var loadedConnectionInfo = Program.LoadCredentials();
        return loadedConnectionInfo.ApiKey;
    }
}