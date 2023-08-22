using System;
using System.ComponentModel.DataAnnotations;

namespace SteamTradeBot.Backend.Models.StateModel;

public class TradingEvent
{
    [Key]
    public int Id { get; set; }
    public DateTime Time { get; set; }

    [Required(AllowEmptyStrings = true)]
    public string ApiKey { get; set; }
    public InfoType Type { get; set; }

    [Required(AllowEmptyStrings = true)]
    public string Info { get; set; } = string.Empty;

    public ServiceWorkingState State { get; set; }
    public LogInState LogIn { get; set; }
    public double BuyPrice { get; set; }
    public double SellPrice { get; set; }
    public double Profit { get; set; }
    public double CurrentBalance { get; set; }
}