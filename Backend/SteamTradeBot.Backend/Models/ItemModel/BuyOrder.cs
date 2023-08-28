﻿using System.ComponentModel.DataAnnotations;

namespace SteamTradeBot.Backend.Models.ItemModel;

public class BuyOrder
{
    [Key]
    public int Id { get; set; }
    public string ApiKey { get; set; }
    public string EngItemName { get; set; }

    [Required(AllowEmptyStrings = true)]
    public string RusItemName { get; set; } = string.Empty;
    public string ItemUrl { get; set; }
    public double BuyPrice { get; set; }
    public double EstimatedSellPrice { get; set; }
    public int Quantity { get; set; }
}