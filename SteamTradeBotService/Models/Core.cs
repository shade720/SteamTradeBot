using System.Threading;
using System.Threading.Tasks;
using SteamTradeBotService.Clients;

namespace SteamTradeBotService.Models
{
    public class Core : ICore
    {
        private readonly ListRunner _listRunner;
        private readonly Executor _executor;
        private readonly Canceler _canceler;
        private readonly InventorySensor _inventorySensor;
        private readonly ItemListLoader _itemListLoader;
        private readonly PostgresClient _postgresClient;
        private static CancellationTokenSource _token;
        private readonly Account _account;

        public Core()
        {
            _postgresClient = new PostgresClient();
            var itemList = _postgresClient.GetItemList();
            var myOrdersList = _postgresClient.GetOrdersList();

            var browser = new Browser();

            _account = new Account(browser);
            _account.SetCore(this);

            _listRunner = new ListRunner(itemList, browser);
            _listRunner.SetCore(this);

            _inventorySensor = new InventorySensor(myOrdersList, browser);
            _inventorySensor.SetCore(this);

            _canceler = new Canceler(myOrdersList, browser);
            _canceler.SetCore(this);

            _executor = new Executor(browser);
            _executor.SetCore(this);

            _itemListLoader = new ItemListLoader();
            _itemListLoader.SetCore(this);
        }

        public async Task StartWork()
        {
            _token = new CancellationTokenSource();
            await Task.WhenAll
                (
                    Task.Run(() => _listRunner.Run(_token.Token)), 
                    Task.Run(() => _inventorySensor.Run(_token.Token)), 
                    Task.Run(() => _canceler.Run(_token.Token))
                );
        }

        public void StopWork()
        {
            _token.Cancel();
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
            switch (ev)
            {
                case "buy":
                    _executor.BuyItem();
                    Reports.MessageWriteEvent?.Invoke("buy");
                    break;
                case "sell":
                    _executor.SellItem();
                    Reports.MessageWriteEvent?.Invoke("sell");
                    break;
                case "cancel":
                    _executor.CancelItem();
                    Reports.MessageWriteEvent?.Invoke("cancel");
                    break;
            }
        }
    }
}
