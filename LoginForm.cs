using System;
using System.Drawing;
using System.Windows.Forms;
using OpenQA.Selenium;
using System.IO;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Support.UI;
using Keys = OpenQA.Selenium.Keys;
using System.Threading.Tasks;
using static TradeBot.GeneralBrowser;
using static TradeBot.Data;
using System.ComponentModel;
using System.Collections.Generic;
using System.Threading;

namespace TradeBot
{
    public partial class LoginForm : Form
    {
        private bool IsLogged = false;
        private bool IsNeedToSave = false;
        private static string SteamID;
        private readonly List<bool> Errors = new List<bool>();

        public LoginForm()
        {
            InitializeComponent();
            for (int i = 0; i < 3; i++) Errors.Add(false);
            KeyDown += new KeyEventHandler(LoginForm_KeyDown);
            LoginTextBox.Validating += LoginTextBox_Validating;
            PasswordTextBox.Validating += PasswordTextBox_Validating;
            SteamGuardTextBox.Validating += SteamGuardTextBox_Validating;
            PathTextBox.Validating += SteamGuardTextBox_Validating;
            LoginTextBox.Text = ReadLoginAndPassword(1);
            PasswordTextBox.Text = ReadLoginAndPassword(2);
            PathTextBox.Text = ReadLoginAndPassword(3);
        }
        private async void LoginButton_Click(object sender, EventArgs e)
        {
            if (!Errors.Contains(true))
            {
                TradeBotAPI main = Owner as TradeBotAPI;
                main.EventConsole.AppendText("Подождите, аккаунт загружается...");
                main.ProgressBar.Visible = true;
                Hide();

                Progress<string> p = new Progress<string>(m => main.ProgressBar.PerformStep());

                await Task.Run(() => LogIn(LoginTextBox.Text, PasswordTextBox.Text, PathTextBox.Text, SteamGuardTextBox.Text, p));

                if (IsLogged)
                {
                    while (true)
                    {
                        try
                        {
                            main.EntireBalanceLabel.Text = Logic.CheckEntireBalance() + " руб.";
                            main.BalanceLabel.Text = Browser.FindElement(By.Id("header_wallet_balance")).Text;
                            main.AccountNameLabel.Text = Browser.FindElement(By.Id("account_pulldown")).Text;
                            break;
                        }
                        catch
                        {
                            Thread.Sleep(10000);
                            Browser.Navigate().Refresh();
                        }
                    }
                    main.ProgressBar.PerformStep();
                    main.EntireLabel.Visible = true;
                    main.BalLabel.Visible = true;
                    main.EntireBalanceLabel.Visible = true;
                    main.BalanceLabel.Visible = true;
                    main.ProgressBar.Visible = false;
                    main.ProgressBar.Value = 0;
                    main.sighUp.Enabled = false;
                    main.sighUp.Visible = false;
                    main.ChangeAccountButton.Enabled = true;
                    main.ChangeAccountButton.Visible = true;
                    main.IsLoggedLabel.Text = "Вход выполнен";
                    main.IsLoggedLabel.ForeColor = Color.Chartreuse;
                    main.EventConsole.AppendText(" Готово!\r\n");
                    main.Show();
                }
                else
                {
                    main.ProgressBar.Visible = false;
                    main.ProgressBar.Value = 0;
                    main.EventConsole.AppendText(" Ошибка!\r\n");
                }
            }
            else
            {
                MessageBox.Show("Заполните поля!", "Ошибка авторизации");
                return;
            }

        }
        private void LogIn(string login, string password, string path, string SteamGuardTextBox, IProgress<string> progress)
        {
            new GeneralBrowser(HideBrowserCheckBox.Checked);
            TimeSpan maxTime = TimeSpan.FromSeconds(10);
            WebDriverWait wait = new WebDriverWait(Browser, maxTime);
            IJavaScriptExecutor js = Browser as IJavaScriptExecutor;
            Browser.Navigate().GoToUrl("https://store.steampowered.com/login/");
            Browser.FindElement(By.Id("input_username")).SendKeys(login + Keys.Enter);
            Browser.FindElement(By.Id("input_password")).SendKeys(password + Keys.Enter);

            progress.Report("");

            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("twofactorcode_entry")));
                IsLogged = true;
            }
            catch
            {
                IsLogged = false;
                Browser.Close();
                MessageBox.Show("Ошибка авторизации", "Ошибка авторизации");
                return;
            }

            progress.Report("");

            if (IsLogged)
            {
                if ((path == "" && SteamGuardTextBox != "") || (path != "" && SteamGuardTextBox != ""))
                {

                    Browser.FindElement(By.Id("twofactorcode_entry")).SendKeys(SteamGuardTextBox + Keys.Enter);

                    try
                    {
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("login_twofactorauth_message_incorrectcode")));
                        IsLogged = false;
                        Browser.Quit();
                        MessageBox.Show("Ошибка авторизации", "Ошибка авторизации");
                        return;
                    }
                    catch { }

                    try
                    {
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("error_display")));
                        IsLogged = false;
                        Browser.Quit();
                        MessageBox.Show("Ошибка авторизации", "Ошибка авторизации");
                        return;
                    }
                    catch { }

                    js.ExecuteScript("window.open('https://store.steampowered.com/')");


                    Browser.SwitchTo().Window(Browser.WindowHandles[1]);
                    progress.Report("");

                    while (true) {
                        try
                        {
                            Browser.FindElement(By.XPath("//*[@class='supernav_container']/a[3]")).Click();
                            Browser.FindElement(By.XPath("//*[@class='profile_link_block box']/div[7]/a")).Click();
                            Browser.FindElement(By.Id("inventory_link_730")).Click();
                            break;
                        }
                        catch
                        {
                            Thread.Sleep(10000);
                            Browser.Navigate().Refresh();
                        }
                    }
                    

                    progress.Report("");

                    Browser.SwitchTo().Window(Browser.WindowHandles[0]);

                }
                if (path != "" && SteamGuardTextBox == "")
                {
                    JObject myJObject;
                    try {
                        myJObject = JObject.Parse(ReadMaFile(path));
                    } 
                    catch {
                        IsLogged = false;
                        Browser.Quit();
                        MessageBox.Show("Неверный путь к maFile", "Ошибка авторизации");
                        return;
                    }
                    string SharedSecret = myJObject.SelectToken("shared_secret").Value<string>();
                    SteamID = myJObject.SelectToken("$.Session.SteamID").Value<string>();

                    js.ExecuteScript("window.open('https://www.chescos.me/js-steam-authcode-generator/?')");

                    Browser.SwitchTo().Window(Browser.WindowHandles[1]);

                    Browser.FindElement(By.Id("secret")).SendKeys(SharedSecret);

                    Browser.FindElement(By.Id("generate")).Click();

                    string Guard = Browser.FindElement(By.Id("result")).Text;

                    Browser.Close();

                    Browser.SwitchTo().Window(Browser.WindowHandles[0]);

                    Browser.FindElement(By.Id("twofactorcode_entry")).SendKeys(Guard + Keys.Enter);

                    try
                    {
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("error_display")));
                        IsLogged = false;
                        Browser.Quit();
                        MessageBox.Show("Ошибка авторизации", "Ошибка авторизации");
                        return;
                    }
                    catch { }

                    progress.Report("");

                    js.ExecuteScript("window.open('https://steamcommunity.com/profiles/" + SteamID + "/inventory/#730')");

                    Browser.SwitchTo().Window(Browser.WindowHandles[0]);

                    progress.Report("");
                }
            }
            if (IsNeedToSave && IsLogged) WriteLoginAndPassword(login, password, PathTextBox.Text);
        }
        private void Observe_Click(object sender, EventArgs e)
        {
            OpenFileDialog.ShowDialog();
            PathTextBox.Text = OpenFileDialog.FileName;
            PathTextBox.Focus();
        }
        private void SaveLoginAndPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveLoginAndPasswordCheckBox.Checked == true) IsNeedToSave = true;
        }
        private void ClearSavedButton_Click(object sender, EventArgs e)
        {
            File.Delete(@"Data\SavedLoginAndPassword.txt");
            Close();
            new LoginForm();
        }
        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                LoginButton.PerformClick();
            }
        }
        private void LoginTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTextBox.Text))
            {
                ErrorProvider.SetError(LoginTextBox, "Поле должно быть заполнено!");
                Errors[0] = true;
            } 
            else
            {
                ErrorProvider.SetError(LoginTextBox, string.Empty);
                Errors[0] = false;
            }
        }
        private void SteamGuardTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(SteamGuardTextBox.Text) && string.IsNullOrEmpty(PathTextBox.Text))
            {
                ErrorProvider.SetError(SteamGuardTextBox, "Введите Steam Guard или укажите путь к maFile!");
                Errors[1] = true;
            }
            else
            {
                ErrorProvider.SetError(SteamGuardTextBox, string.Empty);
                Errors[1] = false;
            }
        }
        private void PasswordTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                ErrorProvider.SetError(PasswordTextBox, "Поле должно быть заполнено!");
                Errors[2] = true;
            }
            else
            {
                ErrorProvider.SetError(PasswordTextBox, string.Empty);
                Errors[2] = false;
            }
        }
    }
}