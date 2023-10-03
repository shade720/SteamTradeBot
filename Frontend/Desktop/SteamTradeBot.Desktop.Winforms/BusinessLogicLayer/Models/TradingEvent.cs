namespace SteamTradeBot.Desktop.Winforms.BusinessLogicLayer.Models;

public class TradingEvent
{
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
    ItemSold,
    ItemCanceled,
    Error,
    Warning
}