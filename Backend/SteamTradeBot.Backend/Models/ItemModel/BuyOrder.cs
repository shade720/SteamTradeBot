﻿using System.ComponentModel.DataAnnotations;

namespace SteamTradeBot.Backend.Models.ItemModel;

public class BuyOrder
{
    [Key]
    public int Id { get; set; }
    public string EngItemName { get; set; }
    public string ApiKey { get; set; }

    [Required(AllowEmptyStrings = true)]
    public string RusItemName { get; set; } = string.Empty;
    public string ItemUrl { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}