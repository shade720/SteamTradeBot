using System.Collections.Generic;

namespace SteamTradeBot.Backend.Models.ItemModel;

public class ItemPage
{
    public string EngItemName { get; init; }
    public string RusItemName { get; set; }
    public string ItemUrl { get; set; }
    public BuyOrder? MyBuyOrder { get; set; }
    public List<SellOrder>? MySellOrders { get; set; }
    public Chart SalesChart { get; set; }
    public List<OrderBookItem> SellOrderBook { get; set; }
    public List<OrderBookItem> BuyOrderBook { get; set; }
    
    //Temp
    public double? EstimatedBuyPrice { get; set; }
    public double? EstimatedSellPrice { get; set; }
    public double CurrentBalance { get; set; }

    public override string ToString()
    {
        return EngItemName;
    }
}