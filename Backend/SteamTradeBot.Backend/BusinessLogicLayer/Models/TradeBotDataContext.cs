using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.ItemModel;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.StateModel;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Models;

public sealed class TradeBotDataContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<TradingEvent> History { get; set; }
    public DbSet<ServiceState> ServiceStates { get; set; }
    public DbSet<ApiKey> ApiKeys { get; set; }

    public TradeBotDataContext(DbContextOptions<TradeBotDataContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasKey(o => new { o.EngItemName, o.ApiKey, o.OrderType });
    }
}