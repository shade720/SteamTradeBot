using System;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Models;

public class StateInfo
{
    public int Id { get; set; }
    public DateTime Time { get; set; }
    public InfoType Type { get; set; }
    public string Info { get; set; }
    public double BuyPrice { get; set; }
    public double SellPrice { get; set; }
    public double Profit { get; set; }
    public double CurrentBalance { get; set; }
}

public enum InfoType
{
    ItemAnalyzed,
    ItemBought,
    ItemsSold,
    Error,
    Warning
}