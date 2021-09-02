using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamTradeBotService.Models
{
    public class Base
    {
        protected ICore _core;
        protected Browser _browser;

        public Base(ICore core = null, Browser browser = null)
        {
            _core = core;
            _browser = browser;
        }

        public void SetCore(ICore core)
        {
            _core = core;
        }

        public void SetBrowser(Browser browser)
        {
            _browser = browser;
        }
    }
}
