using System.Threading;
using System.Threading.Tasks;
using SteamTradeBotService.Clients;

namespace SteamTradeBotService.Models
{
    public class Core : ICore
    {
        private readonly ListRunner _listRunner;
        private readonly Executor _executor;
        private readonly Canceller _canceller;
        private readonly InventorySensor _inventorySensor;
        private readonly ItemListLoader _itemListLoader;
        private readonly PostgresClient _postgresClient;
        private Callbacks _callbacks;
        private CancellationTokenSource _token;
        private readonly Account _account;

        public Core ()
        {
            _postgresClient = new PostgresClient();
            var itemList = _postgresClient.GetItemList();
            var myItemList = _postgresClient.GetMyItemsList();

            var browser = new Browser();

            _account = new Account(browser);
            _account.SetCore(this);

            _listRunner = new ListRunner(itemList, browser);
            _listRunner.SetCore(this);

            _inventorySensor = new InventorySensor(myItemList, browser);
            _inventorySensor.SetCore(this);

            _canceller = new Canceller(myItemList, browser);
            _canceller.SetCore(this);

            _executor = new Executor(browser);
            _executor.SetCore(this);

            _itemListLoader = new ItemListLoader();
            _itemListLoader.SetCore(this);

            _callbacks = new Callbacks();
        }

        public async Task StartWork()
        {
            _token = new CancellationTokenSource();
            await _listRunner.Run(_token.Token);
            await _inventorySensor.Run(_token.Token);
            await _canceller.Run(_token.Token);
        }

        public async Task StopWork()
        {
            await Task.Run(()=>_token.Cancel());
        }

        public async Task LogIn()
        {
            await _account.Log();
        }

        public async Task LogOut()
        {
            await _account.Out();
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

            if (ev == "sell")
            {
                _executor.SellItem();
            }

            if (ev == "cancel")
            {
                _executor.CancelItem();
            }
        }
    }
}
