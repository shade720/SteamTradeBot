using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamTradeBotService.Models
{
    public class Analyzer
    {
        private Browser _browser;
        public Analyzer(Browser browser)
        {
            _browser = browser;
        }

        public bool AnalyzeItem(string itemName)
        {
            return true;
        }
    }
}
