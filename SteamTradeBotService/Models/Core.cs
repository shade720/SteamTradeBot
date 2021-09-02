using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SteamTradeBotService.Clients;

namespace SteamTradeBotService.Models
{
    public class Core : ICore
    {
        private ListRunner _listRunner;
        private Executor _executor;
        private Canceller _canceller;
        private InventorySensor _inventorySensor;
        private ItemListLoader _itemListLoader;
        private readonly PostgresClient _postgresClient;
        private Callbacks _callbacks;
        private CancellationTokenSource _token;
        
        
        public Core (Configuration configuration)
        {
            _callbacks = new Callbacks();
            _postgresClient = new PostgresClient();
            _itemListLoader = new ItemListLoader();
            
            _itemListLoader.SetCore(this);
        }

        public async Task StartWork()
        {
            var itemList = _postgresClient.GetItemList();
            var myItemList = _postgresClient.GetMyItemsList();
            _token = new CancellationTokenSource();

            var browser = new Browser();

            _listRunner = new ListRunner(itemList, _token.Token);
            _listRunner.SetCore(this);
            _listRunner.SetBrowser(browser);

            _inventorySensor = new InventorySensor(myItemList, _token.Token);
            _inventorySensor.SetCore(this);
            _inventorySensor.SetBrowser(browser);

            _canceller = new Canceller(myItemList, _token.Token);
            _canceller.SetCore(this);
            _canceller.SetBrowser(browser);

            _executor = new Executor();
            _executor.SetCore(this);
            _executor.SetBrowser(browser);

            await _listRunner.Run();
            await _inventorySensor.Run();
            await _canceller.Run();
        }

        public void StopWork()
        {
            _token.Cancel();
        }
        
        public async Task LoadItemListAsync()
        {
            var newItemsList = await _itemListLoader.Load();
            _listRunner.SetItemsList(newItemsList);
        }
        
        public void Notify(object sender, string ev)
        {
            if (ev == "buy")
            {
                _executor.BuyItem();
            }
        }
    }
}
