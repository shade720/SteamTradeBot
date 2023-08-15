using System.Net.Http.Headers;

namespace SteamTradeBot.Desktop.Winforms.Models;

public class HttpClientProvider
{
    private const string DefaultHost = "http://localhost:5050";
    public static HttpClient Create()
    {
        var loadedConnectionInfo = Program.LoadConnectionInfo();
        var address = loadedConnectionInfo is null 
            ? DefaultHost 
            : loadedConnectionInfo.ServerAddress;
        var restClient = new HttpClient();
        restClient.BaseAddress = new Uri(address);
        restClient.DefaultRequestHeaders.Accept.Clear();
        restClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        return restClient;
    }
}