using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SteamTradeBotService.Models
{
    public class Canceler : Base
    {
        private List<string> _myItemsList;
        private readonly Browser _browser;

        public Canceler(List<string> myItemList, Browser browser)
        {
            _myItemsList = myItemList;
            _browser = browser;
        }

        public async Task Run(CancellationToken token)
        {
            await Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Thread.Sleep(5000);
                    _core.Notify(this, "cancel");
                }
            });
        }
        public void UpdateMyItemList(List<string> incomingList)
        {
            _myItemsList = new List<string>(incomingList);
        }
    }
}
