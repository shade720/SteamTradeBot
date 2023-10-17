using System.Diagnostics;

namespace SteamTradeBot.Backend.Application.Factories;

internal sealed class CurrentUptimeProvider
{
    private Timer? _timer;
    private readonly Stopwatch _stopwatch;

    private const int UptimeUpdateDelayMs = 1000;
    private const int StartDelay = 0;

    public delegate Task UptimeUpdateHandler(TimeSpan currentUptime);
    public event UptimeUpdateHandler? UptimeUpdate;

    public CurrentUptimeProvider()
    {
        _stopwatch = new Stopwatch();
    }

    public void StartCountdown()
    {
        _timer = new Timer(UpdateTimer, null, StartDelay, UptimeUpdateDelayMs);
        _stopwatch.Start();
    }

    public TimeSpan GetCurrentUptime() =>
        _stopwatch.Elapsed;

    public void StopCountdown()
    {
        _stopwatch.Stop();
        _timer?.Dispose();
    }

    private void UpdateTimer(object? source) =>
        UptimeUpdate?.Invoke(_stopwatch.Elapsed);
}