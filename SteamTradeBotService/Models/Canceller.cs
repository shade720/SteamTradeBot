using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SteamTradeBotService.Models
{
    public class Canceller : Base
    {
        private List<string> _myItemsList;
        private readonly Browser _browser;

        public Canceller(List<string> myItemList, Browser browser)
        {
            _myItemsList = myItemList;
            _browser = browser;
        }

        public async Task Run(CancellationToken token)
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(5000);
                    _core.Notify(this, "cancel");
                }
            }, token);
        }
        public void UpdateMyItemList(List<string> incomingList)
        {
            _myItemsList = new List<string>(incomingList);
        }
    }
}
