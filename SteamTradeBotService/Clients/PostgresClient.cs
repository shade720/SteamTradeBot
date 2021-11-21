using Microsoft.EntityFrameworkCore;
using SteamTradeBotService.DatabaseModels;
using System.Collections.Generic;
using SteamTradeBotService.Models;

namespace SteamTradeBotService.Clients;

public class PostgresClient
{
	public void AddOrUpdateItemList(List<Item> itemList)
	{
		
	}
	public List<Item> GetItemList()
	{
		return new List<Item>();
	}
	public void ClearItemList()
	{
		
	}

	public void AddOrUpdateOrdersList(IEnumerable<Order> order)
	{
		
	}
	public OrdersObservableCollection GetOrdersList()
	{
		return new OrdersObservableCollection();
	}
	public void ClearOrdersList()
	{
		
	}
}

