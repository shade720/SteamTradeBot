using System.Collections.Generic;

namespace SteamTradeBotService.Models.Worker;

public class OrderExecutionSensor
{
	private readonly OrdersObservableCollection _myOrders;
	private readonly Browser _browser;
	private Configuration _configuration;

	public OrderExecutionSensor(OrdersObservableCollection myOrders, Browser browser)
	{
		_myOrders = myOrders;
		_browser = browser;
	}

	public List<SellConfiguration> ScanInventory()
	{
		return new List<SellConfiguration>();
	}

	public void UpdateMyItemList(OrdersObservableCollection incomingList)
	{

	}

	public void SetConfiguration(Configuration incomingConfiguration)
	{
		_configuration = incomingConfiguration;
	}
}