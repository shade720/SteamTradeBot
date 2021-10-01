using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SteamTradeBotService.DatabaseModels;
using SteamTradeBotService.Models;

namespace SteamTradeBotService.Clients
{
    public class PostgresClient
    {
        public void WriteItemList(List<Item> itemList)
        {
            using var database = new DataContext();
            foreach (var item in itemList)
            {
                database.Add(item);
            }
            database.SaveChanges();
        }

        public void WriteOrder(Order order)
        {
            using var database = new DataContext();
            database.Add(order);
            database.SaveChanges();
        }

        public void ClearItemList()
        {
            using var database = new DataContext();
            database.Database.ExecuteSqlRaw("TRUNCATE TABLE [ItemList]");
        }

        public List<string> GetItemList()
        {
            return new List<string>();
        }

        public List<string> GetOrdersList()
        {
            return new List<string>();
        }
    }
}
