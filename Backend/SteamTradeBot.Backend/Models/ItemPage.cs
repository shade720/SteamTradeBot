using System.Collections.Generic;
using SteamTradeBot.Backend.BusinessLogicLayer;

namespace SteamTradeBot.Backend.Models;

public class ItemPage
{
    public string EngItemName { get; set; }
    public string RusItemName { get; set; }
    public string ItemUrl { get; set; }
    public double BuyPrice { get; set; }
    public BuyOrder? MyBuyOrder { get; set; }
    public List<SellOrder>? MySellOrders { get; set; }
    public double AvgPrice { get; set; }
    public double Trend { get; set; }
    public double Sales { get; set; }
    public List<SteamAPI.PointInfo> Graph { get; set; }
    public List<SteamAPI.OrderBookItem> SellOrderBook { get; set; }
    public List<SteamAPI.OrderBookItem> BuyOrderBook { get; set; }
    public double Balance { get; set; }

    public override string ToString()
    {
        return EngItemName;
    }
}