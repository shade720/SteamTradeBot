namespace SteamTradeBotService.Models.Worker
{
	public class BalanceSensor
	{
		private readonly Browser _browser;

		public BalanceSensor(Browser browser)
		{
			_browser = browser;
		}

		public double CheckBalance() 
		{
			return 0;
		}

		public double CheckEntireBalance() 
		{
			return 0;
		}
	}
}
