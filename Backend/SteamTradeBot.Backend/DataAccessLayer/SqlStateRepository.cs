using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.BusinessLogicLayer.Models;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.StateModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.DataAccessLayer;

public sealed class SqlStateRepository : StateRepository
{
    public SqlStateRepository(IDbContextFactory<TradeBotDataContext> tradeBotDataContextFactory) : base(tradeBotDataContextFactory) { }

    public override async Task AddOrUpdateStateAsync(ServiceState state)
    {
        await using var context = await TradeBotDataContextFactory.CreateDbContextAsync();
        if (await context.ServiceStates.ContainsAsync(state))
            context.ServiceStates.Update(state);
        else
            await context.ServiceStates.AddAsync(state);
        await context.SaveChangesAsync();
    }

    public override async Task<ServiceState?> GetStateAsync(string apiKey)
    {
        await using var context = await TradeBotDataContextFactory.CreateDbContextAsync();
        return await context.ServiceStates.FirstOrDefaultAsync(x => x.ApiKey == apiKey);
    }

    public override async Task<List<ServiceState>> GetStatesAsync()
    {
        await using var context = await TradeBotDataContextFactory.CreateDbContextAsync();
        return await context.ServiceStates.ToListAsync();
    }
}