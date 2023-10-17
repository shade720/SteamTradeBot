namespace SteamTradeBot.Backend.Domain.Abstractions;

public interface IItemsTableApi
{
    public Task<List<string>> GetItemNamesListAsync(double fromPrice, double toPrice, double salesVolumeByWeek, int listSize);
}