using System;
using System.Threading;
namespace SteamTradeBotService.Models.Worker.Modes;

public class FindMode : Mode
{
	private readonly ItemListRunner _itemListRunner;
	private readonly BalanceSensor _balanceSensor;
	private const long CancellationScanInterval = 1000 * 60 * 5;
	private const int IterationDelayInMilliseconds = 3000;

	public FindMode(ItemListRunner itemListRunner, OrderCancelerSensor orderCancelerSensor,
		OrderExecutionSensor orderExecutionSensor,
		MarketOperationExecutor marketOperationExecutor, BalanceSensor balanceSensor) : base(
		orderCancelerSensor,
		orderExecutionSensor,
		marketOperationExecutor) => (_itemListRunner, _balanceSensor) = (itemListRunner, balanceSensor);

	public override int Run()
	{
		var lastBalance = _balanceSensor.CheckBalance();
		var lastTimeItemBoughtOrCancelled = DateTimeOffset.Now;
		var totalActions = 0;

		for (var i = 0; i < _itemListRunner.ListCount && Worker.WorkingState; i++)
		{
			var analyzeResult = _itemListRunner.AnalyzeNext();
			if (TryToBuy(analyzeResult, out var buyActionsCount))
			{
				lastTimeItemBoughtOrCancelled = DateTimeOffset.Now;
				totalActions += buyActionsCount;
			}

			var balanceScanResult = _balanceSensor.CheckBalance();
			if (TryToSell(lastBalance, balanceScanResult, out var sellActionsCount))
			{
				lastBalance = balanceScanResult;
				totalActions += sellActionsCount;
			}

			var currentTime = DateTimeOffset.Now;
			if (TryToCancel(lastTimeItemBoughtOrCancelled, currentTime, CancellationScanInterval,
				    out var cancelActionsCount))
			{
				lastTimeItemBoughtOrCancelled = DateTimeOffset.Now;
				totalActions += cancelActionsCount;
			}

			Thread.Sleep(IterationDelayInMilliseconds);
		}

		return totalActions;
	}
}

