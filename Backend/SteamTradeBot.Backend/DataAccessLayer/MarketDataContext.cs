using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.BusinessLogicLayer;
using SteamTradeBot.Backend.Models;

namespace SteamTradeBot.Backend.DataAccessLayer;

public sealed class MarketDataContext : DbContext
{
    public DbSet<Item> Orders { get; set; }
    public DbSet<TradingEvent> History { get; set; }

    public MarketDataContext(DbContextOptions<MarketDataContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
}