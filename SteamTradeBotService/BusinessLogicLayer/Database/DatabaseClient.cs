using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SteamTradeBotService.BusinessLogicLayer.Database;

public class DatabaseClient
{
    private readonly IDbContextFactory<MarketDataContext> _contextFactory;

    public DatabaseClient(IDbContextFactory<MarketDataContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public void AddItem(Item item)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Items.Add(item);
        context.SaveChanges();
    }

    public void UpdateItem(Item item)
    {
        using var context = _contextFactory.CreateDbContext();
        if (context.Items.Contains(item))
            context.Items.Update(item);
        context.SaveChanges();
    }

    public List<Item> GetItems()
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Items.ToList();
    }

    public void ClearItems()
    {
        using var context = _contextFactory.CreateDbContext();
        context.Items.ExecuteDelete();
        context.SaveChanges();
    }
}

