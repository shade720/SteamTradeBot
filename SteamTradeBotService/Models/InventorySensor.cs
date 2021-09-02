using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SteamTradeBotService.Models
{
    public class InventorySensor : Base
    {
        private List<string> _myItemsList;
        private readonly CancellationToken _token;

        public InventorySensor(List<string> myItemsList, CancellationToken token)
        {
            _token = token;
            _myItemsList = myItemsList;
        }

        public async Task Run()
        {
            await Task.Run(() =>
            {
                while (!_token.IsCancellationRequested)
                {

                }
            });
        }

        public void UpdateMyItemList(List<string> incomingList)
        {
            _myItemsList = new List<string>(incomingList);
        }
    }
}
