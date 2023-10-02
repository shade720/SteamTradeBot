using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.StateModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.RepositoryAbstractions;

public abstract class HistoryRepository : Repository
{
    protected HistoryRepository(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory) : base(tradeBotDataContextFactory) { }

    public abstract Task AddNewEventAsync(TradingEvent tradingEvent);

    public abstract Task<List<TradingEvent>> GetHistoryAsync(string apiKey);

    public abstract Task ClearHistoryAsync();
}