using System.Globalization;
using Newtonsoft.Json;
using SteamTradeBot.Desktop.Winforms.Models;
using SteamTradeBot.Desktop.Winforms.ServiceAccess;

namespace SteamTradeBot.Desktop.Winforms.Forms;

public partial class SettingsForm : Form
{
    private readonly SteamTradeBotRestClient _steamTradeBotRestClient;
    public Configuration? Configuration { get; set; }
    public ConnectionInfo? ConnectionInfo { get; set; }

    public SettingsForm(SteamTradeBotRestClient steamTradeBotRestClient)
    {
        InitializeComponent();
        _steamTradeBotRestClient = steamTradeBotRestClient;
        Configuration = Program.LoadConfiguration() ?? new Configuration();
        AvailibleBalanceTextBox.Text = Configuration.AvailableBalance.ToString(CultureInfo.InvariantCulture);
        AnalysisIntervalComboBox.SelectedItem = Configuration.AnalysisIntervalDays.ToString();
        AveragePriceRatioTextBox.Text = Configuration.AveragePriceRatio.ToString(CultureInfo.InvariantCulture);
        OrderQuantityTextBox.Text = Configuration.OrderQuantity.ToString();
        FitRangePriceTextBox.Text = Configuration.FitPriceRange.ToString(CultureInfo.InvariantCulture);
        ItemListSizeTextBox.Text = Configuration.ItemListSize.ToString(CultureInfo.InvariantCulture);
        SellListingFindRangeTextBox.Text = Configuration.SellListingFindRange.ToString();
        MaxPriceTextBox.Text = Configuration.MaxPrice.ToString(CultureInfo.InvariantCulture);
        MinPriceTextBox.Text = Configuration.MinPrice.ToString(CultureInfo.InvariantCulture);
        RequiredProfitTextBox.Text = Configuration.RequiredProfit.ToString(CultureInfo.InvariantCulture);
        TrendTextBox.Text = Configuration.Trend.ToString(CultureInfo.InvariantCulture);
        SalesPerDayTextBox.Text = Configuration.SalesPerDay.ToString(CultureInfo.InvariantCulture);
        SteamUserIdTextBox.Text = Configuration.SteamUserId;
        SteamCommissionTextBox.Text = Configuration.SteamCommission.ToString(CultureInfo.InvariantCulture);
        SalesRatio.Text = Configuration.SalesRatio.ToString(CultureInfo.InvariantCulture);

        ConnectionInfo = Program.LoadConnectionInfo() ?? new ConnectionInfo();
        ConnectionAddressTextBox.Text = ConnectionInfo.ServerAddress;
    }

    private async void UploadSettingsButton_Click(object sender, EventArgs e)
    {
        try
        {
            var configuration = GetCurrentConfiguration();
            var credentials = Program.LoadCredentials();
            configuration.ApiKey = credentials.ApiKey;
            await _steamTradeBotRestClient.UploadSettings(JsonConvert.SerializeObject(configuration));
            MessageBox.Show(@"Configuration was uploaded!", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private Configuration GetCurrentConfiguration()
    {
        return new Configuration
        {
            AvailableBalance = double.Parse(AvailibleBalanceTextBox.Text, CultureInfo.InvariantCulture),
            AnalysisIntervalDays = int.Parse(AnalysisIntervalComboBox.SelectedItem.ToString()),
            AveragePriceRatio = double.Parse(AveragePriceRatioTextBox.Text, CultureInfo.InvariantCulture),
            OrderQuantity = int.Parse(OrderQuantityTextBox.Text),
            FitPriceRange = double.Parse(FitRangePriceTextBox.Text, CultureInfo.InvariantCulture),
            ItemListSize = int.Parse(ItemListSizeTextBox.Text),
            SellListingFindRange = int.Parse(SellListingFindRangeTextBox.Text),
            MaxPrice = double.Parse(MaxPriceTextBox.Text, CultureInfo.InvariantCulture),
            MinPrice = double.Parse(MinPriceTextBox.Text, CultureInfo.InvariantCulture),
            RequiredProfit = double.Parse(RequiredProfitTextBox.Text, CultureInfo.InvariantCulture),
            Trend = double.Parse(TrendTextBox.Text, CultureInfo.InvariantCulture),
            SalesPerDay = int.Parse(SalesPerDayTextBox.Text),
            SteamUserId = SteamUserIdTextBox.Text,
            SteamCommission = double.Parse(SteamCommissionTextBox.Text, CultureInfo.InvariantCulture),
            SalesRatio = double.Parse(SalesRatio.Text, CultureInfo.InvariantCulture)
        };
    }

    private void ResetSettingsButton_Click(object sender, EventArgs e)
    {
        MessageBox.Show(@"Erase all settings?", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        AvailibleBalanceTextBox.Text = string.Empty;
        AnalysisIntervalComboBox.SelectedText = string.Empty;
        AveragePriceRatioTextBox.Text = string.Empty;
        OrderQuantityTextBox.Text = string.Empty;
        FitRangePriceTextBox.Text = string.Empty;
        ItemListSizeTextBox.Text = string.Empty;
        SellListingFindRangeTextBox.Text = string.Empty;
        MaxPriceTextBox.Text = string.Empty;
        MinPriceTextBox.Text = string.Empty;
        RequiredProfitTextBox.Text = string.Empty;
        TrendTextBox.Text = string.Empty;
        SalesPerDayTextBox.Text = string.Empty;
        SteamCommissionTextBox.Text = string.Empty;
        SalesRatio.Text = string.Empty;
        Configuration = null;

        ConnectionAddressTextBox.Text = string.Empty;
        ConnectionInfo = null;

        Program.EraseConnectionInfo();
        Program.EraseConfiguration();

        MessageBox.Show(@"Settings was erased!", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void SaveSettingsButton_Click(object sender, EventArgs e)
    {
        try
        {
            var currentConfiguration = GetCurrentConfiguration();
            Program.SaveConfiguration(currentConfiguration);

            Program.SaveConnectionInfo(new ConnectionInfo { ServerAddress = ConnectionAddressTextBox.Text });
            MessageBox.Show(@"Configuration was saved!", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}