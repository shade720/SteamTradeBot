using System;
using Microsoft.EntityFrameworkCore;

namespace SteamTradeBotService.DatabaseModels
{
    public sealed class DataContext : DbContext
    {
        public DbSet<Item> ItemList { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DataContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING") ?? throw new InvalidOperationException());
        }
    }
}
