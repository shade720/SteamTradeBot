using System.Threading.Tasks;
using Grpc.Net.Client;
using SteamTradeBotService.Protos;

namespace Interface
{
    class TradeBotClient
    {
        private readonly InterfaceService.InterfaceServiceClient _client = new(GrpcChannel.ForAddress("http://localhost:5001"));

        public async Task StartBot(Configuration configuration)
        {
            await _client.StartBotAsync(new StartRequest {Configuration = new SteamTradeBotService.Protos.Configuration
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

        public async Task StopBot()
        {
            await _client.StopBotAsync(new StopRequest());
        }

        public async Task LogIn()
        {
            await _client.LogInAsync(new LogInRequest());
        }

        public async Task LogOut()
        {
            await _client.LogOutAsync(new LogOutRequest());
        }

        public async Task SetConfiguration(Configuration configuration)
        {
            await _client.SetConfigurationAsync(new SetConfigurationRequest
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
            await _client.LoadItemListAsync(new LoadItemListRequest());
        }
    }
}
