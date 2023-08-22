using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.Models.ItemModel;
using SteamTradeBot.Backend.Models.StateModel;

namespace SteamTradeBot.Backend.DataAccessLayer;

public sealed class TradeBotDataContext : DbContext
{
    public DbSet<BuyOrder> BuyOrders { get; set; }
    public DbSet<SellOrder> SellOrders { get; set; }
    public DbSet<TradingEvent> History { get; set; }
    public DbSet<ServiceState> ServiceStates { get; set; }
    public DbSet<string> ApiKeys { get; set; }

    public TradeBotDataContext(DbContextOptions<TradeBotDataContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
}