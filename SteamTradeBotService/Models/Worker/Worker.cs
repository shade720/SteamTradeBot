using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using SteamTradeBotService.Clients;
using SteamTradeBotService.DatabaseModels;
using SteamTradeBotService.Models.Worker.Modes;

namespace SteamTradeBotService.Models.Worker;

public class Worker
{
	private readonly PostgresClient _database;
	private readonly Reporter _reporter;
	private readonly Browser _browser;
	private Configuration _configuration;
	
	private ItemListRunner _itemListRunner;
	private OrderCancelerSensor _orderCancelerSensor;
	private OrderExecutionSensor _orderExecutionSensor;
	private BalanceSensor _balanceSensor;
	private MarketOperationExecutor _marketOperationExecutor;

	private const int MaxAllowedActionsCountPerHour = 2000;
	private int _currentHour;
	private int _actionsPerCurrentHour;

	public static bool WorkingState { get; private set; } = WorkingState = false;

	public Worker(Browser browser, PostgresClient database, Reporter reporter) => (_browser, _database, _reporter) = (browser, database, reporter);

	public Task StartWork()
	{
		WorkingState = true;
		var actionsCount = 0;
		_currentHour = DateTime.Now.Hour;

		var itemList = _database.GetItemList();

		if (itemList is null) throw new NullReferenceException("Item list was null");

		_itemListRunner = new ItemListRunner(itemList, _browser);

		var orderList = _database.GetOrdersList() ?? new OrdersObservableCollection();
		_orderCancelerSensor = new OrderCancelerSensor(orderList, _browser);
		_orderExecutionSensor = new OrderExecutionSensor(orderList, _browser);

		_balanceSensor = new BalanceSensor(_browser);
		_marketOperationExecutor = new MarketOperationExecutor(_browser, _reporter);
		var modeContext = new ModeContext();

		orderList.Subscribe(OrdersListChangedHandler);

		while (WorkingState)
		{
			modeContext.SetMode(SelectMode(actionsCount));
			actionsCount = modeContext.ExecuteMode();
		}
		
		orderList.Unsubscribe(OrdersListChangedHandler);
		orderList.Clear();
		return Task.CompletedTask;
	}

	private void OrdersListChangedHandler(object sender, NotifyCollectionChangedEventArgs args)
	{
		if (args.NewItems != null)
		{
			_database.AddOrUpdateOrdersList(args.NewItems.Cast<Order>());
		}
	}

	private Mode SelectMode(int actionsPerformedOnPreviousIteration)
	{
		var currentBalance = _balanceSensor.CheckBalance();
		var balanceLimit = _configuration.MinPrice;
		if (IsSpeedExceeded(actionsPerformedOnPreviousIteration)) return new WaitMode();
		if (currentBalance < balanceLimit)
		{
			return new SellMode(_orderCancelerSensor, _orderExecutionSensor, _marketOperationExecutor, _balanceSensor);
		}
		return new FindMode(_itemListRunner, _orderCancelerSensor, _orderExecutionSensor, _marketOperationExecutor, _balanceSensor);
	}

	private bool IsSpeedExceeded(int actionsPerformed)
	{
		if (_currentHour == DateTime.Now.Hour)
		{
			_actionsPerCurrentHour += actionsPerformed;
			return MaxAllowedActionsCountPerHour < _actionsPerCurrentHour;
		}
		_actionsPerCurrentHour = 0;
		_actionsPerCurrentHour += actionsPerformed;
		return false;
	}

	public void SetConfiguration(Configuration incomingConfiguration)
	{
		_configuration = incomingConfiguration;
		_itemListRunner.SetConfiguration(incomingConfiguration);
		_orderCancelerSensor.SetConfiguration(incomingConfiguration);
		_orderExecutionSensor.SetConfiguration(incomingConfiguration);
	}

	public Task StopWork()
	{
		WorkingState = false;
		return Task.CompletedTask;
	}
}

