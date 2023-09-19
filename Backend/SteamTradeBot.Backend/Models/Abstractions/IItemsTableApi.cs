﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.Models.Abstractions;

public interface IItemsTableApi
{
    public Task<List<string>> GetItemNamesListAsync(double fromPrice, double toPrice, double salesVolumeByWeek, int listSize);
}