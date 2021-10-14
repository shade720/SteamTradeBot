using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SteamTradeBotService.Models
{
    public class OrderExecutionSensor : BaseComponent
    {
        private List<string> _myItemsList;
        private Browser _browser;

        public OrderExecutionSensor(List<string> myItemsList, Browser browser)
        {
            _myItemsList = myItemsList;
            _browser = browser;
        }

        public async Task Run(CancellationToken token)
        {
            await Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Thread.Sleep(5000);
                    _core.Notify(this, "sell");
                }
            });
        }

        public void UpdateMyItemList(List<string> incomingList)
        {
            _myItemsList = new List<string>(incomingList);
        }
    }
}
