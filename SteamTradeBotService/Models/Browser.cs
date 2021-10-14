using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SteamTradeBotService.Models
{
    public class Browser
    {
        public IWebDriver ChromeBrowser { get; }

        public Browser() {
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            var options = new ChromeOptions();
            //options.AddArgument("--disable-gpu");
            //options.AddArgument("--headless");
            //options.AddArgument("--window-size=1920,1080");
            ChromeBrowser = new ChromeDriver(driverService, options);
            ChromeBrowser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
    }
}
