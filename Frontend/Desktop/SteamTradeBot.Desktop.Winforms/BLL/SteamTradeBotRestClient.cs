using System.Net.Http.Headers;

namespace SteamTradeBot.Desktop.Winforms.BLL;

internal class SteamTradeBotRestClient
{
    private readonly HttpClient _restClient;
    private const string BaseAddress = "http://localhost:4354/api/";

    public SteamTradeBotRestClient()
    {
        _restClient = new HttpClient();
        _restClient.BaseAddress = new Uri(BaseAddress);
        _restClient.DefaultRequestHeaders.Accept.Clear();
        _restClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task Start()
    {
        await _restClient.GetAsync("activation");
    }

    public async Task Stop()
    {
        await _restClient.GetAsync("deactivation");
    }
}