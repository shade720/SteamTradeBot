using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading.Tasks;

namespace SteamTradeBotService.Models
{
    public class AccountLogger : BaseComponent
    {
        private readonly string _login;
        private readonly string _password;
        private readonly string _code;

        public AccountLogger(string login, string password, string code) =>
            (_login, _password, _code) = (login, password, code);

        public async Task LogIn(Browser browserWindow)
        {
            await Task.Run(() =>
            {
                var wait = new WebDriverWait(browserWindow.ChromeBrowser, TimeSpan.FromSeconds(10));

                browserWindow.ChromeBrowser.Navigate().GoToUrl("https://store.steampowered.com/login/");
                browserWindow.ChromeBrowser.FindElement(By.Id("input_username")).SendKeys(_login + Keys.Enter);
                browserWindow.ChromeBrowser.FindElement(By.Id("input_password")).SendKeys(_password + Keys.Enter);
                try
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                        .ElementIsVisible(By.Id("twofactorcode_entry")));
                    browserWindow.ChromeBrowser.FindElement(By.Id("twofactorcode_entry")).SendKeys(_code + Keys.Enter);
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                        By.Id("login_twofactorauth_message_incorrectcode")));
                }
                catch { }
            });
        }

        public async Task LogOut(Browser browserWindow)
        {


        }
    }
}
    

