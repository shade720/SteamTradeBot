using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SteamTradeBotService.Models
{
    public class InventorySensor : Base
    {
        private List<string> _myItemsList;
        private Browser _browser;

        public InventorySensor(List<string> myItemsList, Browser browser)
        {
            _myItemsList = myItemsList;
            _browser = browser;
        }

        public async Task Run(CancellationToken token)
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(5000);
                    _core.Notify(this, "sell");
                }
            }, token);
        }

        public void UpdateMyItemList(List<string> incomingList)
        {
            _myItemsList = new List<string>(incomingList);
        }
    }
}
