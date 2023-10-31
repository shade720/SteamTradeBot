namespace SteamTradeBot.Backend.Domain.TradingEventAggregate;

public enum InfoType
{
    ItemAnalyzed,
    ItemBought,
    ItemSold,
    ItemCanceled,
    Error,
    Warning
}