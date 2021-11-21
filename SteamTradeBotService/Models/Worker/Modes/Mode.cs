using System;

namespace SteamTradeBotService.Models.Worker.Modes
{
	public abstract class Mode
	{
		private readonly OrderCancelerSensor _orderCancelerSensor;
		private readonly OrderExecutionSensor _orderExecutionSensor;
		private readonly MarketOperationExecutor _marketOperationExecutor;
		public abstract int Run();

		protected Mode() { }

		protected Mode(OrderCancelerSensor orderCancelerSensor, OrderExecutionSensor orderExecutionSensor, MarketOperationExecutor marketOperationExecutor)
		{
			_orderCancelerSensor = orderCancelerSensor;
			_orderExecutionSensor = orderExecutionSensor;
			_marketOperationExecutor = marketOperationExecutor;
		}

		protected bool TryToBuy(PurchaseConfiguration analyzeResult, out int actions)
		{
			actions = 1;
			if (analyzeResult is null) return false;
			_marketOperationExecutor.BuyItem(analyzeResult);
			return true;
		}

		protected bool TryToSell(double lastBalance, double newBalance, out int actions)
		{
			actions = 0;
			if (!(lastBalance > newBalance)) return false;
			var scanResult = _orderExecutionSensor.ScanInventory();
			actions++;
			if (scanResult is null) return false;
			_marketOperationExecutor.SellItems(scanResult);
			actions = scanResult.Count * 3;
			return true;
		}

		protected bool TryToCancel(DateTimeOffset oldTime, DateTimeOffset currentTime, long cancellationScanInterval, out int actions)
		{
			actions = 0;
			if (oldTime.ToUnixTimeMilliseconds() - currentTime.ToUnixTimeMilliseconds() <= cancellationScanInterval) return false;
			var scanResult = _orderCancelerSensor.ScanMyOrders();
			actions++;
			if (scanResult is null) return false;
			_marketOperationExecutor.CancelItems(scanResult);
			actions = scanResult.Count * 2;
			return true;
		}
	}
}
