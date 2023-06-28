using System.ComponentModel.DataAnnotations;

namespace SteamTradeBot.Backend.Models;

public class SellOrder
{
    [Key]
    public int Id { get; set; }
    public string EngItemName { get; set; } = string.Empty;
    [Required(AllowEmptyStrings = true)]
    public string RusItemName { get; set; } = string.Empty;
    [Required(AllowEmptyStrings = true)]
    private string ItemUrl { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Quantity { get; set; }
}