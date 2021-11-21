using System;
using System.Threading;

namespace SteamTradeBotService.Models.Worker.Modes;

public class WaitMode : Mode
{
	private const int IterationDelayInMilliseconds = 60000;  
	public override int Run()
	{
		var startHour = DateTime.Now.Hour;
		var currentHour = startHour;
		while (startHour == currentHour)
		{
			currentHour = DateTime.Now.Hour;
			Thread.Sleep(IterationDelayInMilliseconds);
		}
		return 0;
	}
}