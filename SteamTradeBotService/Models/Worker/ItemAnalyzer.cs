namespace SteamTradeBotService.Models.Worker;
public class ItemAnalyzer
{
	private Browser _browser;
	private Configuration _configuration;
	public ItemAnalyzer(Browser browser)
	{
		_browser = browser;
	}

	public PurchaseConfiguration AnalyzeItem(string itemName)
	{
		return new PurchaseConfiguration();
	}

	public void SetConfiguration(Configuration incomingConfiguration)
	{
		_configuration = incomingConfiguration;
	}
}