using Serilog;

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
            Log.Information("buy");
        }

        public void SellItem()
        {
            Log.Information("sell");
        }

        public void CancelItem()
        {
            Log.Information("cancell");
        }
    }
}
