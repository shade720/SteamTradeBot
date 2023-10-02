using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;

public interface IItemsTableApi
{
    public Task<List<string>> GetItemNamesListAsync(double fromPrice, double toPrice, double salesVolumeByWeek, int listSize);
}