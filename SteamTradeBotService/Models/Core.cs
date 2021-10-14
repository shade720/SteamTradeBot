using System.Threading.Tasks;

namespace SteamTradeBotService.Models
{
    public class Core : ICore
    {
        private AccountLogger _accountLogger;

        public Core()
        {

        }

        public async Task StartWork()
        {

        }

        public void StopWork()
        {
            
        }

        public async Task LogIn(string login, string password, string code)
        {

        }

        public async Task LogOut()
        {

        }

        public async Task LoadItemListAsync()
        {
            
        }

        public void Notify(object sender, string ev)
        {
            //switch (ev)
            //{
            //    case "buy":
            //        _executor.BuyItem();
            //        Reporter.MessageWriteEvent?.Invoke("buy");
            //        break;
            //    case "sell":
            //        _executor.SellItem();
            //        Reporter.MessageWriteEvent?.Invoke("sell");
            //        break;
            //    case "cancel":
            //        _executor.CancelItem();
            //        Reporter.MessageWriteEvent?.Invoke("cancel");
            //        break;
            //}
        }
    }
}
