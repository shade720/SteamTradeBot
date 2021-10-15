using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SteamTradeBotService.Protos;

namespace SteamTradeBotService.Models
{
    public class AccountLogger
    {
        public bool LogState { get; private set; }
        private Browser _browser;

        public AccountLogger(Browser browser)
        {
            _browser = browser;
        }

        public async Task<DefaultResponse> LogIn()
        {
            LogState = false;
            return new DefaultResponse { Code = ReplyCode.Failure, Message = "Unreachable" };
        }

        public async Task<DefaultResponse> LogOut()
        {
            LogState = false;
            return new DefaultResponse { Code = ReplyCode.Failure, Message = "Unreachable" };
        }
    }
}
