using System.ComponentModel;
using System.Diagnostics;
using Interface;
using Interface.Forms;
using Newtonsoft.Json;
using TradeBotClient;
using TradeBotClient.Forms;

namespace SteamTradeBotClient.Forms
{
    public partial class MainForm : Form
    {
        private DateTime _startTime;
        private Configuration _configuration;
        private readonly ExtendedConsoleForm _extendedConsole = new();
        private readonly TradeBotAPIClient _client = new();

        public MainForm()
        {
            InitializeComponent();
        }

        private void TradeBotAPI_Load(object sender, EventArgs e)
        {
            _configuration = ReadConfiguration();
            InitializeSda();
            GetConfiguration();
            AddTrayMenuContext();
            ValidateConfiguration();
            SetConfiguration();
            //_client.MessageWriteEvent += message => EventConsole.AppendText(message);
        }

        #region Handlers

        private async void StartButton_Click(object sender, EventArgs e)
        {
            PrepareWindowToStart();
            await _client.StartBot(_configuration);
        }

        private async void StopButton_Click(object sender, EventArgs e)
        {
            EventConsole.AppendText("Останавливаем бота...", Color.White);
            PrepareWindowToStop();
            await _client.StopBot();
        }

        private void SighUpButton_Click(object sender, EventArgs e)
        {
            new LoginForm(_client) { Owner = this }.Show();
        }

        private async void ChangeAccountButton_Click(object sender, EventArgs e)
        {
            PrepareWindowForChangingAccount();
            await _client.LogOut();
            EventConsole.AppendText("Аккаунт отключен, бот недоступен");
        }

        private async void LoadItemListButton_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                MessageBox.Show(@"Поля минимальной/максимальной цены и количества предметов в списке не заполнены!", @"Заполните соответствующие поля");
                return;
            }
            StartButton.Enabled = false;
            ChangeAccountButton.Enabled = false;
            LoadItemListButton.Enabled = false;
            ClearBuyLotsButton.Enabled = false;

            //await _client.LoadItemsList();

            StartButton.Enabled = true;
            ChangeAccountButton.Enabled = true;
            LoadItemListButton.Enabled = true;
            ClearBuyLotsButton.Enabled = true;
        }

        private async void ClearBuyLotsButton_Click(object sender, EventArgs e)
        {
            EventConsole.AppendText("Выполняется очистка заказов...");
            StartButton.Enabled = false;
            ChangeAccountButton.Enabled = false;
            LoadItemListButton.Enabled = false;
            ClearBuyLotsButton.Enabled = false;

            //await _client.ClearItems();

            StartButton.Enabled = true;
            ChangeAccountButton.Enabled = true;
            LoadItemListButton.Enabled = true;
            ClearBuyLotsButton.Enabled = true;
            EventConsole.AppendText("Готово!");
            MessageBox.Show(@"Очистка заказов выполнена!", @"Очистка заказов выполнена");
        }

        private void ExtendedConsoleButton_Click(object sender, EventArgs e)
        {
            _extendedConsole.Show();
        }

        private async void SaveConfigurationButton_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                MessageBox.Show(@"Исправьте значения полей конфигурации!", @"Значение полей неверно");
                return;
            }
            WriteConfiguration(_configuration);
            //await _client.SetConfiguration(_configuration);
            MessageBox.Show(@"Конфигурация успешно применена!", @"Конфигурация успешно применена");
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ClearConfiguration();
        }

        private void ClearLogButton_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show(@"Очистить консоль событий?", @"Очищение консоли событий", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) EventConsole.Text = "";
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            TimeOfUptime.Text = DateTime.Now.Subtract(_startTime).ToString(@"dd\:hh\:mm\:ss");
        }
        #endregion
        #region Configuration

        private void ValidateConfiguration()
        {
            foreach (var control in Controls)
                if (control.GetType().FullName == "System.Windows.Forms.TextBox")
                    ((TextBox)control).Validating += TextBox_Validating;
        }

        private void SetConfiguration()
        {
            if (_configuration is null) return;
            SalesPerWeekTxb.Text = _configuration.SalesPerWeek.ToString();
            PlaceOnListingTxb.Text = _configuration.PlaceOnListing.ToString();
            CoefficientOfSalesTxb.Text = _configuration.CoefficientOfSales.ToString();
            RequredProfitTxb.Text = _configuration.RequiredProfit.ToString();
            MinProfitTxb.Text = _configuration.MinProfit.ToString();
            AvailableBalanceTxb.Text = _configuration.AvailableBalance.ToString();
            FitPriceIntervalTxb.Text = _configuration.FitPriceInterval.ToString();
            AveragePriceTxb.Text = _configuration.AveragePrice.ToString();
            AnalysisInterval.SelectedItem = _configuration.AnalysisInterval;
            MinPriceTxb.Text = _configuration.MinPrice.ToString();
            MaxPriceTxb.Text = _configuration.MaxPrice.ToString();
            ItemListCountTxb.Text = _configuration.ItemListCount.ToString();
            OrderVolumeTxb.Text = _configuration.OrderVolume.ToString();
            TrendTxb.Text = _configuration.Trend.ToString();
        }

        private void GetConfiguration()
        {
            _configuration = new Configuration
            {
                SalesPerWeek = double.Parse(SalesPerWeekTxb.Text),
                PlaceOnListing = double.Parse(PlaceOnListingTxb.Text),
                CoefficientOfSales = double.Parse(CoefficientOfSalesTxb.Text),
                RequiredProfit = double.Parse(RequredProfitTxb.Text),
                MinProfit = double.Parse(MinProfitTxb.Text),
                AnalysisInterval = AnalysisInterval.Text,
                MinPrice = double.Parse(MinPriceTxb.Text),
                MaxPrice = double.Parse(MaxPriceTxb.Text),
                AvailableBalance = double.Parse(AvailableBalanceTxb.Text),
                AveragePrice = double.Parse(AveragePriceTxb.Text),
                FitPriceInterval = double.Parse(FitPriceIntervalTxb.Text),
                ItemListCount = double.Parse(OrderVolumeTxb.Text),
                Trend = double.Parse(TrendTxb.Text),
                OrderVolume = double.Parse(ItemListCountTxb.Text),
            };
        }

        private Configuration ReadConfiguration()
        {
            if (!File.Exists("configuration.save")) return null;
            using var sr = new StreamReader("configuration.save");
            var json = sr.ReadToEnd();
            return JsonConvert.DeserializeObject<Configuration>(json);
        }

        private void WriteConfiguration(Configuration configuration)
        {
            using var sw = new StreamWriter("configuration.save");
            sw.Write(JsonConvert.SerializeObject(configuration));
        }

        #endregion
        #region Prepare

        private void ClearConfiguration()
        {
            SalesPerWeekTxb.Text = "";
            PlaceOnListingTxb.Text = "";
            CoefficientOfSalesTxb.Text = "";
            RequredProfitTxb.Text = "";
            MinProfitTxb.Text = "";
            AvailableBalanceTxb.Text = "";
            FitPriceIntervalTxb.Text = "";
            AveragePriceTxb.Text = "";
            AnalysisInterval.SelectedItem = " ";
            MinPriceTxb.Text = "";
            MaxPriceTxb.Text = "";
            ItemListCountTxb.Text = "";
            OrderVolumeTxb.Text = "";
            TrendTxb.Text = "";
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

        private void PrepareWindowToStart()
        {
            ChangeAccountButton.Enabled = false;
            LoadItemListButton.Enabled = false;
            ClearBuyLotsButton.Enabled = false;
            TimeOfUptime.Visible = true;
            UptimeLabel.Visible = true;
            _startTime = DateTime.Now;
            MyTimer.Enabled = true;
            MyTimer.Start();
            StopButton.Enabled = true;
            StopButton.Visible = true;
            StartButton.Enabled = false;
            StartButton.Visible = false;
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

        #endregion
        #region Tray

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

        #endregion
        #region Other

        private void InitializeSda()
        {
            if (IsSdaEnabled())
            {
                IsSDADisabledLabel.Text = @"SDA включен";
                IsSDADisabledLabel.ForeColor = Color.Chartreuse;
            }
            else
            {
                IsSDADisabledLabel.Text = @"SDA выключен";
                IsSDADisabledLabel.ForeColor = Color.Red;
                var dialogResult = MessageBox.Show(@"SDA не включен! Включить его?", @"SDA не включен", MessageBoxButtons.YesNo);
                if (dialogResult != DialogResult.Yes) return;
                Process.Start(Directory.GetCurrentDirectory() + "/../" + "/../" + "/../" + "/../" + "SteamDesktopAuthenticator/Steam Desktop Authenticator.exe");
                IsSDADisabledLabel.Text = @"SDA включен";
                IsSDADisabledLabel.ForeColor = Color.Chartreuse;
            }

        }
        private void TextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((TextBox)sender).Text))
                ErrorProvider.SetError(((TextBox)sender), "Поля настроек должны быть не пустыми!");
            else if (!double.TryParse(((TextBox)sender).Text, out _))
                ErrorProvider.SetError(((TextBox)sender), "Поля настроек должны быть числовыми!");
            else
                ErrorProvider.SetError(((TextBox)sender), string.Empty);

        }

        public static bool IsSdaEnabled()
        {
            return Process.GetProcessesByName("Steam Desktop Authenticator").Length != 0;
        }

        #endregion

    }
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;
            box.SelectionColor = color;
            box.AppendText("\r\n" + text);
            box.SelectionColor = box.ForeColor;
        }
    }
}