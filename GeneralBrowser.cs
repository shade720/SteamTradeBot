using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TradeBot
{
    public class GeneralBrowser
    {
        public static IWebDriver Browser;
        private readonly ChromeDriverService driverService;
        private readonly ChromeOptions options;
        public static WebDriverWait wait;

        public GeneralBrowser(bool IfHeadless) {
            driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            options = new ChromeOptions();
            options.AddArgument("--disable-gpu");
            if (IfHeadless) {
                options.AddArgument("--headless");
                options.AddArgument("--window-size=1920,1080");
            }
            Browser = new ChromeDriver(driverService, options);
            Browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            TimeSpan maxTime = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(Browser, maxTime);
        }
    }
}
