using System;
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
            Configuration.SetConfiguration(request.Configuration);
            _core.StartWork();
            return new StartResponse();
        }

        public override Task<StopResponse> StopBot(StopRequest request, ServerCallContext context)
        {
            _core.StopWork();
            return Task.FromResult(new StopResponse());
        }

        public override async Task<SetConfigurationResponse> SetConfiguration(SetConfigurationRequest request, ServerCallContext context)
        {
            Configuration.SetConfiguration(request.Configuration);
            return new SetConfigurationResponse();
        }

        public override async Task<LogInResponse> LogIn(LogInRequest request, ServerCallContext context)
        {
            await _core.LogIn(request.Login, request.Password, request.Code);
            return new LogInResponse();
        }

        public override async Task<LogOutResponse> LogOut(LogOutRequest request, ServerCallContext context)
        {
            await _core.LogOut();
            return new LogOutResponse();
        }
    }
}
