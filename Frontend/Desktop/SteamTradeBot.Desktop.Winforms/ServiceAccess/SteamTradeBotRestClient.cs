using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamTradeBot.Desktop.Winforms.Models;

namespace SteamTradeBot.Desktop.Winforms.ServiceAccess;

public class SteamTradeBotRestClient
{
    private readonly HttpClientProvider _clientProvider;
    private DateTime _lastStateCheck = DateTime.MinValue;

    public SteamTradeBotRestClient(HttpClientProvider clientProvider)
    {
        _clientProvider = clientProvider;
    }

    public async Task Start(Credentials credentials, Configuration configuration)
    {
        using var restClient = HttpClientProvider.Create();
        restClient.Timeout = TimeSpan.MaxValue;
        var serializedCredentials = JsonConvert.SerializeObject(credentials);
        var serializedConfiguration = JsonConvert.SerializeObject(configuration);

        var credentialsJObject = JObject.Parse(serializedCredentials);
        var configurationJObject = JObject.Parse(serializedConfiguration);
        credentialsJObject.Merge(configurationJObject, new JsonMergeSettings {MergeArrayHandling = MergeArrayHandling.Merge});

        var response = await restClient.PostAsync("/api/activation", new StringContent(credentialsJObject.ToString(), Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
    }

    public async Task Stop()
    {
        using var restClient = HttpClientProvider.Create();
        var response = await restClient.PostAsync("/api/deactivation", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task UploadSettings(string configurationJson)
    {
        using var restClient = HttpClientProvider.Create();
        var response = await restClient.PostAsync("/api/configuration", new StringContent(configurationJson, Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
    }

    public async Task CancelOrders()
    {
        using var restClient = HttpClientProvider.Create();
        var response = await restClient.PostAsync("/api/orderscanceling", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task<StateInfo> CheckState()
    {
        using var restClient = HttpClientProvider.Create();
        try
        {
            var response = await restClient.GetAsync($"/api/state?fromTicks={_lastStateCheck.Ticks}");
            _lastStateCheck = DateTime.UtcNow;
            var stateInfoJson = await response.Content.ReadAsStringAsync();
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
        using var restClient = HttpClientProvider.Create();
        var response = await restClient.GetAsync("logs");
        return await response.Content.ReadAsStringAsync();
    }
}