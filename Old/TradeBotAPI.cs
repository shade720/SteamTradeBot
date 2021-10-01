using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TradeBot.Data;
using static TradeBot.GeneralBrowser;

namespace TradeBot
{
    public partial class TradeBotAPI : Form
    {
        DateTime startTime;
        List<string> Configuration = new List<string>();
        CancellationTokenSource CancellationTokenSource;
        List<bool> Errors = new List<bool>();
        public static ExtendedConsole ExtendedConsole;

        public TradeBotAPI()
        {
            InitializeComponent();
        }

        private void TradeBotAPI_Load(object sender, EventArgs e)
        {
            ExtendedConsole = new ExtendedConsole();
            InitializeSDA();
            AddTrayMenuContext();
            CreateRequiredFiles();
            Configuration = ReadConfiguration();
            for (int i = 0; i < 14; i++) Errors.Add(false);
            InitializeConfigurationTextBoxesValidation();
            InitializeConfigurationTextBoxes();
        }

        private void InitializeSDA()
        {
            if (IsSDAEnabled())
            {
                IsSDADisabledLabel.Text = "SDA включен";
                IsSDADisabledLabel.ForeColor = Color.Chartreuse;
            }
            else
            {
                IsSDADisabledLabel.Text = "SDA выключен";
                IsSDADisabledLabel.ForeColor = Color.Red;
                DialogResult dialogResult = MessageBox.Show("SDA не включен! Включить его?", "SDA не включен", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Process.Start(Directory.GetCurrentDirectory() + "/../" + "/../" + "/../" + "/../" +"/SteamDesktopAuthenticator/Steam Desktop Authenticator.exe");
                    IsSDADisabledLabel.Text = "SDA включен";
                    IsSDADisabledLabel.ForeColor = Color.Chartreuse;
                }
            }
        }

        private void InitializeConfigurationTextBoxesValidation()
        {
            NumberOfSales.Validating += NumberOfSales_Validating;
            PlaceOnListing.Validating += PlaceOnListing_Validating;
            CoefficientOfSales.Validating += CoefficientOfSales_Validating;
            RequredProfit.Validating += RequredProfit_Validating;
            MinProfit.Validating += MinProfit_Validating;
            AvailibleBalance.Validating += AvailibleBalance_Validating;
            RangeOfPrice.Validating += RangeOfPrice_Validating;
            PeriodOfAnalysisComboBox.Validating += PeriodOfAnalysisComboBox_Validating;
            AveragePriceTextBox.Validating += AveragePriceTextBox_Validating;
            MinPriceTextBox.Validating += MinPriceTextBox_Validating;
            MaxPriceTextBox.Validating += MaxPriceTextBox_Validating;
            NumberOfItemsTextBox.Validating += NumberOfItemsTextBox_Validating;
            CountOfItemBox.Validating += CountOfItemBox_Validating;
            TrendTextBox.Validating += TrendTextBox_Validating;
        }

        private void InitializeConfigurationTextBoxes()
        {
            NumberOfSales.Text = Configuration[0];
            PlaceOnListing.Text = Configuration[1];
            CoefficientOfSales.Text = Configuration[2];
            RequredProfit.Text = Configuration[3];
            MinProfit.Text = Configuration[4];
            AvailibleBalance.Text = Configuration[5];
            RangeOfPrice.Text = Configuration[6];
            AveragePriceTextBox.Text = Configuration[7];
            PeriodOfAnalysisComboBox.SelectedItem = Configuration[8];
            MinPriceTextBox.Text = Configuration[9];
            MaxPriceTextBox.Text = Configuration[10];
            NumberOfItemsTextBox.Text = Configuration[11];
            CountOfItemBox.Text = Configuration[12];
            TrendTextBox.Text = Configuration[13];
        }

        public static bool IsSDAEnabled()
        {
            Process[] processes = Process.GetProcessesByName("Steam Desktop Authenticator");
            if (processes.Length != 0) return true;
            else return false;
        }

        private void PrepareWindowToStop()
        {
            UptimeLabel.Visible = false;
            StopButton.Visible = false;
            StopButton.BackColor = Color.Transparent;
            StartButton.Enabled = true;
            StartButton.Visible = true;
            MyTimer.Enabled = false;
            TimeOfUptime.Visible = false;
            TimeOfUptime.Text = "";
            ChangeAccountButton.Enabled = true;
            LoadItemListButton.Enabled = true;
            ClearBuyLotsButton.Enabled = true;
            MyTimer.Stop();
        }

        private void PrepareWindowForLaunching()
        {
            ChangeAccountButton.Enabled = false;
            LoadItemListButton.Enabled = false;
            ClearBuyLotsButton.Enabled = false;
            TimeOfUptime.Visible = true;
            UptimeLabel.Visible = true;
            startTime = DateTime.Now;
            MyTimer.Enabled = true;
            MyTimer.Start();
            StopButton.Enabled = true;
            StopButton.Visible = true;
            StartButton.Enabled = false;
            StartButton.Visible = false;
        }

        private async void StartButton_Click(object sender, EventArgs e)
        {
            if (Browser == null)
            {
                MessageBox.Show("Вы не авторизированы!", "Вы не авторизированы");
                return;
            }
            InitializeSDA();
            Progress<string> Channel = new Progress<string>(m =>
            {
                switch (m)
                {
                    case string a when a.Contains("Stopped"):
                        {
                            PrepareWindowToStop();
                            EventConsole.AppendText("\r\nБот остановлен\r\n", Color.White);
                        }
                        break;
                    case string b when b.Contains("Error"):
                        {
                            PrepareWindowToStop();
                            EventConsole.AppendText("\r\nБот остановлен аварийно\r\n" + b[(b.IndexOf(' ') + 1)..], Color.White);
                        }
                        break;
                    case string c when c.Contains("balance"):
                        {
                            BalanceLabel.Text = c[(c.IndexOf(' ') + 1)..];
                        }
                        break;
                    case string d when d.Contains("entire"):
                        {
                            EntireBalanceLabel.Text = d[(d.IndexOf(' ') + 1)..] + " руб.";
                        }
                        break;
                    case string e when e.Contains("продан"):
                        {
                            EventConsole.AppendText(e, Color.Chartreuse);
                            ExtendedConsole.ExtendedEventConsole.AppendText(e + " " + '[' + DateTime.Now.ToString(@"hh\:mm\:ss") + ']', Color.Chartreuse);
                        }
                        break;
                    case string f when f.Contains("куплен"):
                        {
                            EventConsole.AppendText(f, Color.Orange);
                            ExtendedConsole.ExtendedEventConsole.AppendText(f + " " + '[' + DateTime.Now.ToString(@"hh\:mm\:ss") + ']', Color.Orange);
                        }
                        break;
                    case string g when g.Contains("SDADisabled"):
                        {
                            IsSDADisabledLabel.Text = "SDA выключен";
                            IsSDADisabledLabel.ForeColor = Color.Red;
                            DialogResult dialogResult = MessageBox.Show("SDA не включен! Включить его?", "SDA не включен", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                Process.Start(Directory.GetCurrentDirectory() + "/../" + "/../" + "/../" + "/SDA/Steam Desktop Authenticator.exe");
                                IsSDADisabledLabel.Text = "SDA включен";
                                IsSDADisabledLabel.ForeColor = Color.Chartreuse;
                            }
                        }
                        break;
                    case string h when h.Contains("отсеян"):
                        {
                            ExtendedConsole.SiftedItemsTextBox.AppendText(h + " " + '[' + DateTime.Now.ToString(@"hh\:mm\:ss") + ']');
                        }
                        break;
                    default:
                        {
                            EventConsole.AppendText(m, Color.White);
                        }
                        break;
                }
            });
            CancellationTokenSource = new CancellationTokenSource();
            PrepareWindowForLaunching();
            EventConsole.AppendText("\r\nБот запущен\r\n", Color.White);
            await Task.Run(() => Logic.LaunchBot(Channel, CancellationTokenSource.Token), CancellationTokenSource.Token);
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            CancellationTokenSource.Cancel();
            EventConsole.AppendText("\r\n\r\nОстанавливаем бота...", Color.White);
        }

        private void SighUpButton_Click(object sender, EventArgs e)
        {
            new LoginForm { Owner = this }.Show();
        }

        private void PrepareWindowForChangingAccount()
        {
            sighUp.Enabled = true;
            sighUp.Visible = true;
            ChangeAccountButton.Enabled = false;
            ChangeAccountButton.Visible = false;
            BalanceLabel.Text = "";
            EntireBalanceLabel.Text = "";
            EntireLabel.Visible = false;
            BalLabel.Visible = false;
            EntireBalanceLabel.Visible = false;
            BalanceLabel.Visible = false;
            AccountNameLabel.Text = "none";
            BalanceLabel.Text = "";
            IsLoggedLabel.Text = "Вход не выполнен";
            IsLoggedLabel.ForeColor = Color.Red;
        }

        private void ChangeAccountButton_Click(object sender, EventArgs e)
        {
            Browser.Quit();
            Browser = null;
            PrepareWindowForChangingAccount();
            EventConsole.AppendText("\r\nАккаунт отключен, бот недоступен\r\n\r\n");
        }

        private async void SaveConfigurationButton_Click(object sender, EventArgs e)
        {
            if (!Errors.Contains(true))
            {
                MessageBox.Show("Исправьте значения полей конфигурации!", "Значение полей неверно");
                return;
            }
            UpdateConfigurationList();
            WriteConfiguration(Configuration);
            MessageBox.Show("Конфигурация успешно применена!", "Конфигурация успешно применена");
            await Task.Run(() => Logic.UpdateConfiguration());
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            NumberOfSales.Text = "";
            PlaceOnListing.Text = "";
            CoefficientOfSales.Text = "";
            RequredProfit.Text = "";
            MinProfit.Text = "";
            AvailibleBalance.Text = "";
            RangeOfPrice.Text = "";
            AveragePriceTextBox.Text = "";
            PeriodOfAnalysisComboBox.SelectedItem = " ";
            MinPriceTextBox.Text = "";
            MaxPriceTextBox.Text = "";
            NumberOfItemsTextBox.Text = "";
            CountOfItemBox.Text = "";
            TrendTextBox.Text = "";
        }

        private void Restart_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + "\\TradeBot.exe");
                Process.GetCurrentProcess().Kill();
            }
            catch { }
        }

        private void ClearLogButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Очистить консоль событий?", "Очищение консоли событий", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) EventConsole.Text = "";
        }

        private void Tray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            EventConsole.Focus();
            EventConsole.ScrollToCaret();
        }

        private void ToTrayButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
        }

        private void AddTrayMenuContext()
        {
            Tray.ContextMenuStrip = new ContextMenuStrip();
            Tray.ContextMenuStrip.Items.Add("Запустить бота", null, MenuLaunch_Click);
            Tray.ContextMenuStrip.Items.Add("Остановить бота", null, MenuStop_Click);
            Tray.ContextMenuStrip.Items.Add("Выход", null, MenuExit_Click);
        }

        private void MenuLaunch_Click(object sender, EventArgs e)
        {
            StartButton_Click(sender, e);
        }

        private void MenuStop_Click(object sender, EventArgs e)
        {
            StopButton_Click(sender, e);
        }

        private void MenuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            TimeOfUptime.Text = DateTime.Now.Subtract(startTime).ToString(@"dd\:hh\:mm\:ss");
        }

        private void UpdateConfigurationList()
        {
            Configuration.Clear();
            Configuration.Add(NumberOfSales.Text);
            Configuration.Add(PlaceOnListing.Text);
            Configuration.Add(CoefficientOfSales.Text);
            Configuration.Add(RequredProfit.Text);
            Configuration.Add(MinProfit.Text);
            Configuration.Add(AvailibleBalance.Text);
            Configuration.Add(RangeOfPrice.Text);
            Configuration.Add(AveragePriceTextBox.Text);
            Configuration.Add(PeriodOfAnalysisComboBox.SelectedItem.ToString());
            Configuration.Add(MinPriceTextBox.Text);
            Configuration.Add(MaxPriceTextBox.Text);
            Configuration.Add(NumberOfItemsTextBox.Text);
            Configuration.Add(CountOfItemBox.Text);
            Configuration.Add(TrendTextBox.Text);
        }

        private async void LoadItemListButton_Click(object sender, EventArgs e)
        {
            if (Errors[9] || Errors[10] || Errors[11] || Browser == null)
            {
                if (Browser == null)
                {
                    MessageBox.Show("Необходимо войти в аккаунт!", "Вы не авторизированы");
                    return;
                }
                MessageBox.Show("Поля минимальной/максимальной цены и количества предметов в списке не заполнены!", "Заполните соответствующие поля");
                return;
            }
            StartButton.Enabled = false;
            ChangeAccountButton.Enabled = false;
            LoadItemListButton.Enabled = false;
            ClearBuyLotsButton.Enabled = false;
            ProgressBar.Visible = true;
            Progress<string> p = new Progress<string>(m =>
            {
                if (m == "") ProgressBar.PerformStep();
                else if (m != "")
                    if (m == "stop1")
                    {
                        EventConsole.AppendText(" Ошибка!");
                        ProgressBar.Visible = false;
                        ProgressBar.Value = 0;
                        StartButton.Enabled = true;
                        ChangeAccountButton.Enabled = true;
                        LoadItemListButton.Enabled = true;
                        ClearBuyLotsButton.Enabled = true;
                    }
                    else EventConsole.AppendText(m);
            });
            await Task.Run(() => Logic.LoadItemList(p));
            ProgressBar.Visible = false;
            ProgressBar.Value = 0;
            StartButton.Enabled = true;
            ChangeAccountButton.Enabled = true;
            LoadItemListButton.Enabled = true;
            ClearBuyLotsButton.Enabled = true;
        }

        private async void ClearBuyLotsButton_ClickAsync(object sender, EventArgs e)
        {
            if (Browser == null)
            {
                MessageBox.Show("Необходимо войти в аккаунт!", "Вы не авторизированы");
                return;
            }
            EventConsole.AppendText("\r\nВыполняется очистка заказов...");
            StartButton.Enabled = false;
            ChangeAccountButton.Enabled = false;
            LoadItemListButton.Enabled = false;
            ClearBuyLotsButton.Enabled = false;
            Progress<string> p = new Progress<string>(m =>
            {
                EventConsole.AppendText(" Ошибка!");
                StartButton.Enabled = true;
                ChangeAccountButton.Enabled = true;
                LoadItemListButton.Enabled = true;
                ClearBuyLotsButton.Enabled = true;
            });
            await Task.Run(() => Logic.ClearBuyLots(p));
            StartButton.Enabled = true;
            ChangeAccountButton.Enabled = true;
            LoadItemListButton.Enabled = true;
            ClearBuyLotsButton.Enabled = true;
            EventConsole.AppendText(" Готово!");
            MessageBox.Show("Очистка заказов выполнена!", "Очистка заказов выполнена");
        }

        private void ExtendedConsoleButton_Click(object sender, EventArgs e)
        {
            ExtendedConsole.Show();
        }

        private void EventConsole_TextChanged(object sender, EventArgs e)
        {
            EventConsole.SelectionStart = EventConsole.Text.Length;
            EventConsole.ScrollToCaret();
        }

        private void NumberOfSales_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NumberOfSales.Text))
            {

                ErrorProvider.SetError(NumberOfSales, "Поля настроек должны быть не пустыми!");
                Errors[0] = true;
            }
            else if (!double.TryParse(NumberOfSales.Text, out _))
            {
                ErrorProvider.SetError(NumberOfSales, "Поля настроек должны быть числовыми!");
                Errors[0] = true;
            }
            else
            {
                ErrorProvider.SetError(NumberOfSales, string.Empty);
                Errors[0] = false;
            }
        }
        private void PlaceOnListing_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PlaceOnListing.Text))
            {

                ErrorProvider.SetError(PlaceOnListing, "Поля настроек должны быть не пустыми!");
                Errors[1] = true;
            }
            else if (!double.TryParse(PlaceOnListing.Text, out _))
            {
                ErrorProvider.SetError(PlaceOnListing, "Поля настроек должны быть числовыми!");
                Errors[1] = true;
            }
            else
            {
                ErrorProvider.SetError(PlaceOnListing, string.Empty);
                Errors[1] = false;
            }
        }
        private void CoefficientOfSales_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CoefficientOfSales.Text))
            {

                ErrorProvider.SetError(CoefficientOfSales, "Поля настроек должны быть не пустыми!");
                Errors[2] = true;
            }
            else if (!double.TryParse(CoefficientOfSales.Text, out _))
            {
                ErrorProvider.SetError(CoefficientOfSales, "Поля настроек должны быть числовыми!");
                Errors[2] = true;
            }
            else
            {
                ErrorProvider.SetError(CoefficientOfSales, string.Empty);
                Errors[2] = false;
            }
        }
        private void RequredProfit_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RequredProfit.Text))
            {

                ErrorProvider.SetError(RequredProfit, "Поля настроек должны быть не пустыми!");
                Errors[3] = true;
            }
            else if (!double.TryParse(RequredProfit.Text, out _))
            {
                ErrorProvider.SetError(RequredProfit, "Поля настроек должны быть числовыми!");
                Errors[3] = true;
            }
            else
            {
                ErrorProvider.SetError(RequredProfit, string.Empty);
                Errors[3] = false;
            }
        }
        private void MinProfit_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MinProfit.Text))
            {

                ErrorProvider.SetError(MinProfit, "Поля настроек должны быть не пустыми!");
                Errors[4] = true;
            }
            else if (!double.TryParse(MinProfit.Text, out _))
            {
                ErrorProvider.SetError(MinProfit, "Поля настроек должны быть числовыми!");
                Errors[4] = true;
            }
            else
            {
                ErrorProvider.SetError(MinProfit, string.Empty);
                Errors[4] = false;
            }
        }
        private void AvailibleBalance_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AvailibleBalance.Text))
            {

                ErrorProvider.SetError(AvailibleBalance, "Поля настроек должны быть не пустыми!");
                Errors[5] = true;
            }
            else if (!double.TryParse(AvailibleBalance.Text, out _))
            {
                ErrorProvider.SetError(AvailibleBalance, "Поля настроек должны быть числовыми!");
                Errors[5] = true;
            }
            else
            {
                ErrorProvider.SetError(AvailibleBalance, string.Empty);
                Errors[5] = false;
            }
        }
        private void AveragePriceTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AveragePriceTextBox.Text))
            {

                ErrorProvider.SetError(AveragePriceTextBox, "Поля настроек должны быть не пустыми!");
                Errors[6] = true;
            }
            else if (!double.TryParse(AveragePriceTextBox.Text, out _))
            {
                ErrorProvider.SetError(AveragePriceTextBox, "Поля настроек должны быть числовыми!");
                Errors[6] = true;
            }
            else
            {
                ErrorProvider.SetError(AveragePriceTextBox, string.Empty);
                Errors[6] = false;
            }
        }
        private void RangeOfPrice_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RangeOfPrice.Text))
            {

                ErrorProvider.SetError(RangeOfPrice, "Поля настроек должны быть не пустыми!");
                Errors[7] = true;
            }
            else if (!double.TryParse(RangeOfPrice.Text, out _))
            {
                ErrorProvider.SetError(RangeOfPrice, "Поля настроек должны быть числовыми!");
                Errors[7] = true;
            }
            else
            {
                ErrorProvider.SetError(RangeOfPrice, string.Empty);
                Errors[7] = false;
            }
        }
        private void PeriodOfAnalysisComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (PeriodOfAnalysisComboBox.SelectedItem.ToString() == " ")
            {
                ErrorProvider.SetError(PeriodOfAnalysisComboBox, "Поля настроек должны быть не пустыми!");
                Errors[8] = true;
            }
            else
            {
                ErrorProvider.SetError(PeriodOfAnalysisComboBox, string.Empty);
                Errors[8] = false;
            }
        }
        private void MinPriceTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MinPriceTextBox.Text))
            {

                ErrorProvider.SetError(MinPriceTextBox, "Поля настроек должны быть не пустыми!");
                Errors[9] = true;
            }
            else if (!double.TryParse(MinPriceTextBox.Text, out _))
            {
                ErrorProvider.SetError(MinPriceTextBox, "Поля настроек должны быть числовыми!");
                Errors[9] = true;
            }
            else
            {
                ErrorProvider.SetError(MinPriceTextBox, string.Empty);
                Errors[9] = false;
            }
        }
        private void MaxPriceTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MaxPriceTextBox.Text))
            {

                ErrorProvider.SetError(MaxPriceTextBox, "Поля настроек должны быть не пустыми!");
                Errors[10] = true;
            }
            else if (!double.TryParse(MaxPriceTextBox.Text, out _))
            {
                ErrorProvider.SetError(MaxPriceTextBox, "Поля настроек должны быть числовыми!");
                Errors[10] = true;
            }
            else
            {
                ErrorProvider.SetError(MaxPriceTextBox, string.Empty);
                Errors[10] = false;
            }
        }
        private void NumberOfItemsTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NumberOfItemsTextBox.Text))
            {

                ErrorProvider.SetError(NumberOfItemsTextBox, "Поля настроек должны быть не пустыми!");
                Errors[11] = true;
            }
            else if (!double.TryParse(NumberOfItemsTextBox.Text, out _))
            {
                ErrorProvider.SetError(NumberOfItemsTextBox, "Поля настроек должны быть числовыми!");
                Errors[11] = true;
            }
            else
            {
                ErrorProvider.SetError(NumberOfItemsTextBox, string.Empty);
                Errors[11] = false;
            }
        }
        private void CountOfItemBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CountOfItemBox.Text))
            {
                ErrorProvider.SetError(CountOfItemBox, "Поля настроек должны быть не пустыми!");
                Errors[12] = true;
            }
            else if (!double.TryParse(CountOfItemBox.Text, out _))
            {
                ErrorProvider.SetError(CountOfItemBox, "Поля настроек должны быть числовыми!");
                Errors[12] = true;
            }
            else
            {
                ErrorProvider.SetError(CountOfItemBox, string.Empty);
                Errors[12] = false;
            }
        }
        private void TrendTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TrendTextBox.Text))
            {
                ErrorProvider.SetError(TrendTextBox, "Поля настроек должны быть не пустыми!");
                Errors[13] = true;
            }
            else if (!double.TryParse(TrendTextBox.Text, out _))
            {
                ErrorProvider.SetError(TrendTextBox, "Поля настроек должны быть числовыми!");
                Errors[13] = true;
            }
            else
            {
                ErrorProvider.SetError(TrendTextBox, string.Empty);
                Errors[13] = false;
            }
        }
    }
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;
            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}