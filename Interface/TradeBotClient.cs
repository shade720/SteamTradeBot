using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using SteamTradeBotService.Protos;

namespace Interface
{
    class TradeBotClient
    {
        private readonly InterfaceService.InterfaceServiceClient _clientInterface = new (GrpcChannel.ForAddress("https://localhost:5051"));
        private readonly ReportService.ReportServiceClient _clientReports = new (GrpcChannel.ForAddress("https://localhost:5051"));
        private CancellationTokenSource _token;

        public delegate void BalanceWriter (double balance);
        public delegate void MessageWriter (string message);

        public BalanceWriter BalanceWriteEvent;
        public MessageWriter MessageWriteEvent;

        public async Task StartBot(Configuration configuration)
        {
            await _clientInterface.StartBotAsync(new StartRequest {Configuration = new SteamTradeBotService.Protos.Configuration
                {
                    SalesPerWeek = configuration.SalesPerWeek,
                    PlaceOnListing = configuration.PlaceOnListing,
                    AnalysisInterval = configuration.AnalysisInterval,
                    AvailableBalance = configuration.AvailableBalance,
                    AveragePrice = configuration.AveragePrice,
                    CoefficientOfSales = configuration.CoefficientOfSales,
                    RequiredProfit = configuration.RequiredProfit,
                    FitPriceInterval = configuration.FitPriceInterval,
                    ItemListCount = configuration.ItemListCount,
                    Trend = configuration.Trend,
                    MaxPrice = configuration.MaxPrice,
                    OrderVolume = configuration.OrderVolume,
                    MinPrice = configuration.MinPrice,
                    MinProfit = configuration.MinProfit,
                }
            });
            await SubscribeWriteBacks();
        }

        public async Task SubscribeWriteBacks()
        {
            using var call = _clientReports.WriteBack(new WriteBackRequest());
            _token = new CancellationTokenSource();

            while (await call.ResponseStream.MoveNext(_token.Token))
            {
                switch (call.ResponseStream.Current.EventTypeCase)
                {
                    case WriteBackResponse.EventTypeOneofCase.Balance:
                        BalanceWriteEvent?.Invoke(call.ResponseStream.Current.Balance);
                        break;
                    case WriteBackResponse.EventTypeOneofCase.Message:
                        MessageWriteEvent?.Invoke(call.ResponseStream.Current.Message);
                        break;
                    case WriteBackResponse.EventTypeOneofCase.None:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

        }

        public async Task StopBot()
        {
            await _clientInterface.StopBotAsync(new StopRequest());
            _token.Cancel();
        }

        public async Task LogIn()
        {
            await _clientInterface.LogInAsync(new LogInRequest());
        }

        public async Task LogOut()
        {
            await _clientInterface.LogOutAsync(new LogOutRequest());
        }

        public async Task SetConfiguration(Configuration configuration)
        {
            await _clientInterface.SetConfigurationAsync(new SetConfigurationRequest
            {
                Configuration = new SteamTradeBotService.Protos.Configuration
                {
                    SalesPerWeek = configuration.SalesPerWeek,
                    PlaceOnListing = configuration.PlaceOnListing,
                    AnalysisInterval = configuration.AnalysisInterval,
                    AvailableBalance = configuration.AvailableBalance,
                    AveragePrice = configuration.AveragePrice,
                    CoefficientOfSales = configuration.CoefficientOfSales,
                    RequiredProfit = configuration.RequiredProfit,
                    FitPriceInterval = configuration.FitPriceInterval,
                    ItemListCount = configuration.ItemListCount,
                    Trend = configuration.Trend,
                    MaxPrice = configuration.MaxPrice,
                    OrderVolume = configuration.OrderVolume,
                    MinPrice = configuration.MinPrice,
                    MinProfit = configuration.MinProfit,
                }
            });
        }

        public async Task ClearItems()
        {

        }

        public async Task LoadItemsList()
        {
            await _clientInterface.LoadItemListAsync(new LoadItemListRequest());
        }
    }
}
