using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamTradeBotService.Models
{
    public class Executor : Base
    {
        private readonly Browser _browser;

        public Executor(Browser browser)
        {
            _browser = browser;
        }

        public void BuyItem()
        {
        }

        public void SellItem()
        {
        }

        public void CancelItem()
        {
        }
    }
}
