using System.Threading.Tasks;
using Grpc.Core;
using SteamTradeBotService.Models;
using SteamTradeBotService.Protos;

namespace SteamTradeBotService.Services
{
    public class InterfaceListener : InterfaceService.InterfaceServiceBase
    {
        private readonly Core _core = new ();

        public override async Task<StartResponse> StartBot(StartRequest request, ServerCallContext context)
        {
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
