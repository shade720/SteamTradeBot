using System.Threading.Tasks;
using OpenQA.Selenium;
using SteamTradeBotService.Clients;

namespace SteamTradeBotService.Models.WorkerClasses
{
    public class Worker : IWorker
    {
        private PostgresClient _database;
        private IWebDriver _browser;
        public bool WorkingState { get; private set; }

        public Worker( Browser browser, PostgresClient database) => (_browser, _database) = (browser.ChromeBrowser, database);
        

        public async Task StartWork()
        {
            WorkingState = true;
        }

        public async Task StopWork()
        {
            WorkingState = false;
        }

        public void Notify(object sender, string ev)
        {
            throw new System.NotImplementedException();
        }
    }
}
