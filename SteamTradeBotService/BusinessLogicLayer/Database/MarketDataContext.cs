using Microsoft.EntityFrameworkCore;

namespace SteamTradeBotService.BusinessLogicLayer.Database;

public sealed class MarketDataContext : DbContext
{
    public DbSet<Item> Items { get; set; }

    public MarketDataContext(DbContextOptions<MarketDataContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
}