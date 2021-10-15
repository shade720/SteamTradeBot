using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SteamTradeBotService.Clients;
using SteamTradeBotService.DatabaseModels;
using SteamTradeBotService.Protos;

namespace SteamTradeBotService.Models
{
    public class ItemListLoader
    {
        public List<string> ItemList { get; private set; }
        private readonly IWebDriver _browser;
        private readonly PostgresClient _database;

        public ItemListLoader(Browser browser, PostgresClient database) =>
            (_browser, _database) = (browser.ChromeBrowser, database);

        public async Task<DefaultResponse> Load()
        {
            await Task.Run(() =>
            {
                ItemList = _database.GetItemList();
            });
            return new DefaultResponse { Code = ReplyCode.Failure, Message = "Unreachable" };
        }
    }
}
