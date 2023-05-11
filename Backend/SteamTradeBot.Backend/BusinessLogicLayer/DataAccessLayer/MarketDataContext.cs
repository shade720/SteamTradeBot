using Microsoft.EntityFrameworkCore;
using SteamTradeBot.Backend.BusinessLogicLayer.Models;

namespace SteamTradeBot.Backend.BusinessLogicLayer.DataAccessLayer;

public sealed class MarketDataContext : DbContext
{
    public DbSet<Item> Items { get; set; }

    public MarketDataContext(DbContextOptions<MarketDataContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
}