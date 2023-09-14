namespace SteamTradeBot.Desktop.Winforms.BusinessLogicLayer;

public class ApiKeyProvider
{
    private string? _apiKey;

    public string GetApiKey()
    {
        if (_apiKey is not null) 
            return _apiKey;
        var configuration = Program.LoadConfiguration();
        if (configuration is null)
            return string.Empty;
        _apiKey = configuration.ApiKey;
        return _apiKey;
    }
}