using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using SteamTradeBotService.DatabaseModels;

namespace SteamTradeBotService.Models
{
	public class OrdersObservableCollection
	{
		private readonly ObservableCollection<Order> _orders = new();

		public void Add(Order order)
		{
			_orders.Add(order);
		}

		public void RemoveAt(int index)
		{
			_orders.RemoveAt(index);
		}

		public void Update(int index, Order order)
		{
			_orders.RemoveAt(index);
			_orders.Insert(index, order);
		}

		public void Clear()
		{
			_orders.Clear();
		}

		public void Subscribe(Action<object?, NotifyCollectionChangedEventArgs> handler)
		{
			_orders.CollectionChanged += handler.Invoke;
		}

		public void Unsubscribe(Action<object?, NotifyCollectionChangedEventArgs> handler)
		{
			_orders.CollectionChanged -= handler.Invoke;
		}
	}
}
