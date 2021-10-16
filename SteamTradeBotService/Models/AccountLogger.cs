using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SteamTradeBotService.Protos;
using SteamTradeBotService.Services;

namespace SteamTradeBotService.Models
{
    public class AccountLogger
    {
        public bool LogState { get; private set; }
        private readonly Browser _browser;
        private readonly Reporter _reporter;

        public AccountLogger(Browser browser, Reporter reporter)
        {
            _browser = browser;
            _reporter = reporter;
        }

        public async Task<DefaultResponse> LogIn()
        {
            LogState = false;
            _reporter.WriteMessage("started");
            return new DefaultResponse { Code = ReplyCode.Failure, Message = "Unreachable" };
        }

        public async Task<DefaultResponse> LogOut()
        {
            LogState = false;
            _reporter.WriteMessage("stopped");
            return new DefaultResponse { Code = ReplyCode.Failure, Message = "Unreachable" };
        }
    }
}
