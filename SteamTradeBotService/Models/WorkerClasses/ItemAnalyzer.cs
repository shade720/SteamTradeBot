using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamTradeBotService.Models
{
    public class ItemAnalyzer
    {
        private Browser _browser;
        public ItemAnalyzer(Browser browser)
        {
            _browser = browser;
        }

        public bool AnalyzeItem(string itemName)
        {
            return true;
        }
    }
}
