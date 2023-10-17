﻿using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.Domain.StateModel;

namespace SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;

public abstract class HistoryRepository : Repository
{
    protected HistoryRepository(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory) : base(tradeBotDataContextFactory) { }

    public abstract Task AddNewEventAsync(TradingEvent tradingEvent);

    public abstract Task<List<TradingEvent>> GetHistoryAsync(string apiKey);

    public abstract Task ClearHistoryAsync(string apiKey);
}