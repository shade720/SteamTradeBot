using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using SteamTradeBot.Desktop.Winforms.Models;

namespace SteamTradeBot.Desktop.Winforms.ServiceAccess;

public class SteamTradeBotRestClient : IDisposable
{
    private readonly HttpClient _restClient;
    private const string BaseAddress = "http://localhost:5050/api/";

    public SteamTradeBotRestClient()
    {
        _restClient = new HttpClient();
        _restClient.BaseAddress = new Uri(BaseAddress);
        _restClient.DefaultRequestHeaders.Accept.Clear();
        _restClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task LogIn(Credentials credentials)
    {
        var response = await _restClient.PostAsync("login", new StringContent(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
    }

    public async Task LogOut()
    {
        var response = await _restClient.PostAsync("logout", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task Start()
    {
        var response = await _restClient.PostAsync("activation", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task Stop()
    {
        var response = await _restClient.PostAsync("deactivation", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task UploadSettings(string configurationJson)
    {
        var response = await _restClient.PostAsync("configuration", new StringContent(configurationJson, Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
    }

    public async Task CancelOrders()
    {
        var response = await _restClient.PostAsync("orderscanceling", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task<StateInfo> CheckState()
    {
        try
        {
            var response = await _restClient.GetAsync("state");
            var stateInfoJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<StateInfo>(stateInfoJson)!;
        }
        catch
        {
            return new StateInfo
            {
                Connection = StateInfo.ConnectionState.Disconnected,
                WorkingState = StateInfo.ServiceWorkingState.Down
            };
        }
    }

    public async Task<string> GetLogFile()
    {
        var response = await _restClient.GetAsync("logs");
        return await response.Content.ReadAsStringAsync();
    }

    public void Dispose()
    {
        _restClient.Dispose();
    }
}