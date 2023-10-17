﻿using SteamTradeBot.Backend.Domain.ItemModel;

namespace SteamTradeBot.Backend.Domain.Abstractions.Rules;

public interface ICancelRule
{
    public bool IsFollowed(ItemPage itemPage);
    public Task<bool> IsFollowedAsync(ItemPage itemPage);
}