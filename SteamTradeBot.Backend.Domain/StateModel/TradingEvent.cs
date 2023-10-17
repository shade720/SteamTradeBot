using System.ComponentModel.DataAnnotations;

namespace SteamTradeBot.Backend.Domain.StateModel;

public class TradingEvent
{
    [Key]
    public int Id { get; set; }
    public DateTime Time { get; set; }
    public string ApiKey { get; set; }
    public InfoType Type { get; set; }
    public string Info { get; set; }
    public double BuyPrice { get; set; }
    public double SellPrice { get; set; }
    public double Profit { get; set; }
    public double CurrentBalance { get; set; }
}