using System.ComponentModel.DataAnnotations;

namespace SteamTradeBot.Backend.Models.ItemModel;

public class SellOrder
{
    [Key]
    public int Id { get; set; }
    public string EngItemName { get; set; }
    [Required(AllowEmptyStrings = true)]
    public string RusItemName { get; set; } = string.Empty;
    [Required(AllowEmptyStrings = true)]
    public string ItemUrl { get; set; }
    public string ApiKey { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}