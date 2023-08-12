using System;

namespace SteamTradeBot.Backend.Models.StateModel;

public class TradingEvent
{
    public int Id { get; set; }
    public DateTime Time { get; set; }
    public InfoType Type { get; set; }
    public string Info { get; set; } = string.Empty;
    public double BuyPrice { get; set; }
    public double SellPrice { get; set; }
    public double Profit { get; set; }
    public double CurrentBalance { get; set; }
}