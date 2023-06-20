﻿using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace SteamTradeBot.Desktop.Winforms.BLL;

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
        await _restClient.PostAsync("logout", null);
    }

    public async Task Start()
    {
        await _restClient.PostAsync("activation", null);
    }

    public async Task Stop()
    {
        await _restClient.PostAsync("deactivation", null);
    }
    
    public void Dispose()
    {
        _restClient.Dispose();
    }
}