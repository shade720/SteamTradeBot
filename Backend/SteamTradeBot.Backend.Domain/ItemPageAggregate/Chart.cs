using System.Collections;

namespace SteamTradeBot.Backend.Domain.ItemPageAggregate;

public class Chart : IEnumerable<Chart.PointInfo>
{
    private readonly List<PointInfo> _graphPoints;

    public PointInfo this[int index]
    {
        get => _graphPoints[index];
        set => _graphPoints.Insert(index, value);
    }
    public Chart() { }
    public Chart(IEnumerable<PointInfo> points)
    {
        _graphPoints = new List<PointInfo>(points);
    }

    public IEnumerator<PointInfo> GetEnumerator()
    {
        return _graphPoints.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public class PointInfo
    {
        public DateTime Date { get; init; }
        public double Price { get; init; }
        public int Quantity { get; init; }
    }
}