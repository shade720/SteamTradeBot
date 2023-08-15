﻿using System.Threading.Tasks;
using SteamTradeBot.Backend.Models.ItemModel;

namespace SteamTradeBot.Backend.Models.Abstractions;

public interface IBuyRule
{
    public bool IsFollowed(ItemPage itemPage);
    public Task<bool> IsFollowedAsync(ItemPage itemPage);
}