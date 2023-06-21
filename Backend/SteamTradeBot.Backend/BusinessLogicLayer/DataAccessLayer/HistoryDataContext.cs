using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.BusinessLogicLayer.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer.DataAccessLayer;

public class HistoryDataContext : DbContext
{
    public DbSet<StateInfo> States { get; set; }

    public HistoryDataContext(DbContextOptions<HistoryDataContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
}