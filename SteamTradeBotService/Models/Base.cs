using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamTradeBotService.Models
{
    public class Base
    {
        protected ICore _core;

        public Base(ICore core = null, Browser browser = null)
        {
            _core = core;
        }

        public void SetCore(ICore core)
        {
            _core = core;
        }
    }
}
