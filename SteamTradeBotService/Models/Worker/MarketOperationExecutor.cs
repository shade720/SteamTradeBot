using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using SteamTradeBotService.DatabaseModels;

namespace SteamTradeBotService.Models.Worker;

public class MarketOperationExecutor
{
	private readonly Browser _browser;
	private readonly Reporter _reporter;

	public MarketOperationExecutor(Browser browser, Reporter reporter)
	{
		_browser = browser;
		_reporter = reporter;
	}

	public void BuyItem(PurchaseConfiguration purchaseConfiguration)
	{
		Log.Information("buy");
	}

	public void SellItems(List<SellConfiguration> sellingItemsConfigurationList)
	{
		Log.Information("sell");
	}

	public void CancelItems(List<Item> cancellingItemsNamesList)
	{
		Log.Information("cancel");
	}

	public Task CancelAll()
	{
		return Task.CompletedTask;
	}
}