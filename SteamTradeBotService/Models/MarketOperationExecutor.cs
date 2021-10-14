using Serilog;

namespace SteamTradeBotService.Models
{
    public class MarketOperationExecutor : BaseComponent
    {
        private readonly Browser _browser;

        public MarketOperationExecutor(Browser browser)
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
