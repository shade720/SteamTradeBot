using System.Threading.Tasks;
using Grpc.Core;
using SteamTradeBotService.Models;
using SteamTradeBotService.Protos;
using Configuration = SteamTradeBotService.Models.Configuration;

namespace SteamTradeBotService.Services
{
    public class InterfaceListener : InterfaceService.InterfaceServiceBase
    {
        private readonly Core _core = new ();

        public override async Task<StartResponse> StartBot(StartRequest request, ServerCallContext context)
        {
            Configuration.AnalysisInterval = request.Configuration.AnalysisInterval;
            Configuration.AvailableBalance = request.Configuration.AvailableBalance;
            Configuration.AveragePrice = request.Configuration.AveragePrice;
            Configuration.CoefficientOfSales = request.Configuration.CoefficientOfSales;
            Configuration.FitPriceInterval = request.Configuration.FitPriceInterval;
            Configuration.ItemListCount = request.Configuration.ItemListCount;
            Configuration.MaxPrice = request.Configuration.MaxPrice;
            Configuration.MinPrice = request.Configuration.MinPrice;
            Configuration.MinProfit = request.Configuration.MinProfit;
            Configuration.OrderVolume = request.Configuration.OrderVolume;
            Configuration.PlaceOnListing = request.Configuration.PlaceOnListing;
            Configuration.RequiredProfit = request.Configuration.RequiredProfit;
            Configuration.SalesPerWeek = request.Configuration.SalesPerWeek;
            Configuration.Trend = request.Configuration.Trend;
            _core.StartWork();
            return new StartResponse();
        }

        public override async Task<StopResponse> StopBot(StopRequest request, ServerCallContext context)
        {
            _core.StopWork();
            return new StopResponse();
        }

        public override async Task<SetConfigurationResponse> SetConfiguration(SetConfigurationRequest request, ServerCallContext context)
        {
            Configuration.AnalysisInterval = request.Configuration.AnalysisInterval;
            Configuration.AvailableBalance = request.Configuration.AvailableBalance;
            Configuration.AveragePrice = request.Configuration.AveragePrice;
            Configuration.CoefficientOfSales = request.Configuration.CoefficientOfSales;
            Configuration.FitPriceInterval = request.Configuration.FitPriceInterval;
            Configuration.ItemListCount = request.Configuration.ItemListCount;
            Configuration.MaxPrice = request.Configuration.MaxPrice;
            Configuration.MinPrice = request.Configuration.MinPrice;
            Configuration.MinProfit = request.Configuration.MinProfit;
            Configuration.OrderVolume = request.Configuration.OrderVolume;
            Configuration.PlaceOnListing = request.Configuration.PlaceOnListing;
            Configuration.RequiredProfit = request.Configuration.RequiredProfit;
            Configuration.SalesPerWeek = request.Configuration.SalesPerWeek;
            Configuration.Trend = request.Configuration.Trend;
            return new SetConfigurationResponse();
        }

        public override async Task<LogInResponse> LogIn(LogInRequest request, ServerCallContext context)
        {
            await _core.LogIn();
            return new LogInResponse();
        }

        public override async Task<LogOutResponse> LogOut(LogOutRequest request, ServerCallContext context)
        {
            await _core.LogOut();
            return new LogOutResponse();
        }
    }
}
