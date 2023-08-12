using System;
using System.Collections;
using System.Collections.Generic;
using static SteamTradeBot.Backend.Models.Chart;

namespace SteamTradeBot.Backend.Models;

public class Chart : IEnumerable<PointInfo>
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