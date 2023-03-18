using Grpc.Net.Client;
using Interface;

namespace TradeBotClient;

class TradeBotAPIClient
{
    private readonly SteamTradeBot.SteamTradeBotClient _tradeBotClient = new(GrpcChannel.ForAddress("http://localhost:5050"));
    public delegate void BalanceWriter(double balance);
    public delegate void MessageWriter(string message);

    public BalanceWriter BalanceWriteEvent;
    public MessageWriter MessageWriteEvent;

    public async Task StartBot(Configuration configuration)
    {
        _tradeBotClient.StartBot(new StartRequest());
    }
    
    public async Task StopBot()
    {
        _tradeBotClient.StopBot(new StopRequest());
    }

    public async Task LogIn(string login, string password, string token)
    {
        _tradeBotClient.LogIn(new LogInRequest {Login = login, Password = password, Token = token});
    }

    public async Task LogOut()
    {
        _tradeBotClient.LogOut(new LogOutRequest());
    }
    
    public async Task ClearItems()
    {

    }

    public async Task LoadItemsList()
    {
        
    }
}