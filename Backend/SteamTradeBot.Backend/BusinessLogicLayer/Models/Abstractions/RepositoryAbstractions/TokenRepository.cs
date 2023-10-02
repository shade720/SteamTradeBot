using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.RepositoryAbstractions;

public abstract class TokenRepository : Repository
{
    protected TokenRepository(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory) : base(tradeBotDataContextFactory) { }

    public abstract Task<bool> Contains(string token);
}