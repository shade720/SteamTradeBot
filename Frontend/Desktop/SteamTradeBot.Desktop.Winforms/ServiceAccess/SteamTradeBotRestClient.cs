using Newtonsoft.Json;
using SteamTradeBot.Desktop.Winforms.Models;
using System.Text;

namespace SteamTradeBot.Desktop.Winforms.ServiceAccess;

public class SteamTradeBotRestClient
{
    private readonly HttpClientProvider _clientProvider;
    private readonly ApiKeyProvider _keyProvider;

    public SteamTradeBotRestClient(
        HttpClientProvider clientProvider, 
        ApiKeyProvider keyProvider)
    {
        _clientProvider = clientProvider;
        _keyProvider = keyProvider;
    }

    public async Task Start(Credentials credentials)
    {
        using var restClient = _clientProvider.Create();
        var serializedCredentials = JsonConvert.SerializeObject(credentials);
        var response = await restClient.PostAsync($"/api/activate?apiKey={_keyProvider.GetApiKey()}", new StringContent(serializedCredentials, Encoding.UTF8, "application/json"));
        await GetResponseContent(response);
    }

    public async Task Stop()
    {
        using var restClient = _clientProvider.Create();
        var response = await restClient.PostAsync($"/api/deactivate?apiKey={_keyProvider.GetApiKey()}", null);
        await GetResponseContent(response);
    }

    public async Task UploadSettings(string configurationJson)
    {
        using var restClient = _clientProvider.Create();
        var response = await restClient.PostAsync($"/api/refreshConfiguration?apiKey={_keyProvider.GetApiKey()}", new StringContent(configurationJson, Encoding.UTF8, "application/json"));
        await GetResponseContent(response);
    }

    public async Task CancelOrders()
    {
        using var restClient = _clientProvider.Create();
        var response = await restClient.PostAsync($"/api/cancelOrders?apiKey={_keyProvider.GetApiKey()}", null);
        await GetResponseContent(response);
    }

    public async Task<StateInfo> CheckState()
    {
        using var restClient = _clientProvider.Create();
        try
        {
            var response = await restClient.GetAsync($"/api/state?apiKey={_keyProvider.GetApiKey()}");
            var stateInfoJson = await GetResponseContent(response);
            var stateInfoObject = JsonConvert.DeserializeObject<StateInfo>(stateInfoJson);
            return stateInfoObject ?? new StateInfo
            {
                Connection = StateInfo.ConnectionState.Disconnected, 
                WorkingState = StateInfo.ServiceWorkingState.Down
            };
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
        using var restClient = _clientProvider.Create();
        var response = await restClient.GetAsync($"logs?apiKey={_keyProvider.GetApiKey()}");
        return await GetResponseContent(response);
    }

    private static async Task<string> GetResponseContent(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
            return content;
        throw new Exception($"Service error.\r\nCode: {response.StatusCode}\r\nMessage: {content}");
    }
}