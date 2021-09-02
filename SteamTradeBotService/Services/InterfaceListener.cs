using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using SteamTradeBotService.Models;
using SteamTradeBotService.Protos;

namespace SteamTradeBotService.Services
{
    public class InterfaceListener : InterfaceService.InterfaceServiceBase
    {
        public override async Task<StartResponse> StartBot(StartRequest request, ServerCallContext context)
        {
            var core = new Core(new Configuration());
            await core.StartWork();
            return new StartResponse();
        }

        public override Task<StopResponse> StopBot(StopRequest request, ServerCallContext context)
        {
            return base.StopBot(request, context);
        }

        public override Task<SetConfigurationResponse> SetConfiguration(SetConfigurationRequest request, ServerCallContext context)
        {
            return base.SetConfiguration(request, context);
        }
    }
}
