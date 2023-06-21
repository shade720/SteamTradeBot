using System;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Models;

public class StateChangingEvent
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

public enum InfoType
{
    ItemAnalyzed,
    ItemBought,
    ItemSold,
    ItemCanceled,
    Error,
    Warning
}