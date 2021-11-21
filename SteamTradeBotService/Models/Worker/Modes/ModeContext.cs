namespace SteamTradeBotService.Models.Worker.Modes;

public class ModeContext
{
	private Mode _mode;

	public void SetMode(Mode mode)
	{
		_mode = mode;
	}

	public int ExecuteMode()
	{
		return _mode.Run();
	}
}

