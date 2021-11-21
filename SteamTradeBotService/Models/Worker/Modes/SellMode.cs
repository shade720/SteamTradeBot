using System;
using System.Threading;

namespace SteamTradeBotService.Models.Worker.Modes;

public class SellMode : Mode
{
	private readonly BalanceSensor _balanceSensor;
	private const int IterationDelayInMilliseconds = 3000;
	private const int IterationCount = 400;
	private const long CancellationScanInterval = 1000 * 60 * 5;

	public SellMode(OrderCancelerSensor orderCancelerSensor, OrderExecutionSensor orderExecutionSensor,
		MarketOperationExecutor marketOperationExecutor, BalanceSensor balanceSensor) : base(
		orderCancelerSensor, orderExecutionSensor, marketOperationExecutor) => _balanceSensor = balanceSensor;
	
	public override int Run()
	{
		var lastBalance = _balanceSensor.CheckBalance();
		var lastTimeItemBoughtOrCancelled = DateTimeOffset.Now;
		var totalActions = 0;

		for (var i = 0; i < IterationCount && Worker.WorkingState; i++)
		{
			var balanceScanResult = _balanceSensor.CheckBalance();
			if (TryToSell(lastBalance, balanceScanResult, out var sellActionsCount))
			{
				lastBalance = balanceScanResult;
				totalActions += sellActionsCount;
			}

			var currentTime = DateTimeOffset.Now;
			if (TryToCancel(lastTimeItemBoughtOrCancelled, currentTime, CancellationScanInterval, out var cancelActionsCount))
			{
				lastTimeItemBoughtOrCancelled = DateTimeOffset.Now;
				totalActions += cancelActionsCount;
			}
			Thread.Sleep(IterationDelayInMilliseconds);
		}

		return totalActions;
	}
}