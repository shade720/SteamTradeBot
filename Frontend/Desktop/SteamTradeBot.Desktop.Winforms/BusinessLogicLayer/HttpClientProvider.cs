using System.Net.Http.Headers;

namespace SteamTradeBot.Desktop.Winforms.BusinessLogicLayer;

public class HttpClientProvider
{
    private const string DefaultHost = "http://localhost:5050";
    private string? _host;

    public HttpClient Create()
    {
        if (_host is null)
        {
            var configuration = Program.LoadConfiguration();
            _host = configuration is null ? DefaultHost : configuration.ServerAddress;
        }
        var restClient = new HttpClient();
        restClient.BaseAddress = new Uri(_host);
        restClient.DefaultRequestHeaders.Accept.Clear();
        restClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        return restClient;
    }
}