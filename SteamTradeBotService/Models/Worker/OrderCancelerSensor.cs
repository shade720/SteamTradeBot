using System.Collections.Generic;
using SteamTradeBotService.DatabaseModels;

namespace SteamTradeBotService.Models.Worker;
public class OrderCancelerSensor
{
	private readonly OrdersObservableCollection _myOrders;
	private readonly Browser _browser;
	private Configuration _configuration;

	public OrderCancelerSensor(OrdersObservableCollection myOrders, Browser browser) => (_myOrders, _browser) = (myOrders, browser);
	
	public List<Item> ScanMyOrders()
	{
		return new List<Item>();
	}

	public void UpdateMyItemList(OrdersObservableCollection incomingList)
	{

	}
	public void SetConfiguration(Configuration incomingConfiguration)
	{
		_configuration = incomingConfiguration;
	}
}

