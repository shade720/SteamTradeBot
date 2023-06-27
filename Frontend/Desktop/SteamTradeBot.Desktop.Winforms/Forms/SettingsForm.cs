using System.Globalization;
using Newtonsoft.Json;
using SteamTradeBot.Desktop.Winforms.Models;
using SteamTradeBot.Desktop.Winforms.ServiceAccess;

namespace SteamTradeBot.Desktop.Winforms.Forms;

public partial class SettingsForm : Form
{
    private readonly SteamTradeBotRestClient _steamTradeBotRestClient;
    public Configuration? Configuration { get; set; }

    public SettingsForm(SteamTradeBotRestClient steamTradeBotRestClient)
    {
        InitializeComponent();
        _steamTradeBotRestClient = steamTradeBotRestClient;
        Configuration = Program.LoadConfiguration() ?? new Configuration();
        AvailibleBalanceTextBox.Text = Configuration.AvailableBalance.ToString(CultureInfo.InvariantCulture);
        AnalysisIntervalComboBox.SelectedItem = Configuration.AnalysisIntervalDays.ToString();
        AveragePriceTextBox.Text = Configuration.AveragePrice.ToString(CultureInfo.InvariantCulture);
        OrderQuantityTextBox.Text = Configuration.OrderQuantity.ToString(CultureInfo.InvariantCulture);
        FitRangePriceTextBox.Text = Configuration.FitPriceRange.ToString(CultureInfo.InvariantCulture);
        ItemListSizeTextBox.Text = Configuration.ItemListSize.ToString(CultureInfo.InvariantCulture);
        ListingFindRangeTextBox.Text = Configuration.ListingFindRange.ToString(CultureInfo.InvariantCulture);
        MaxPriceTextBox.Text = Configuration.MaxPrice.ToString(CultureInfo.InvariantCulture);
        MinPriceTextBox.Text = Configuration.MinPrice.ToString(CultureInfo.InvariantCulture);
        RequiredProfitTextBox.Text = Configuration.RequiredProfit.ToString(CultureInfo.InvariantCulture);
        TrendTextBox.Text = Configuration.Trend.ToString(CultureInfo.InvariantCulture);
        SalesPerWeekTextBox.Text = Configuration.SalesPerWeek.ToString(CultureInfo.InvariantCulture);
        SteamUserIdTextBox.Text = Configuration.SteamUserId;
        SteamCommissionTextBox.Text = Configuration.SteamCommission.ToString(CultureInfo.InvariantCulture);
    }

    private async void UploadSettingsButton_Click(object sender, EventArgs e)
    {
        try
        {
            var configuration = GetCurrentConfiguration();
            await _steamTradeBotRestClient.UploadSettings(JsonConvert.SerializeObject(configuration));
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
            AveragePrice = double.Parse(AveragePriceTextBox.Text, CultureInfo.InvariantCulture),
            OrderQuantity = int.Parse(OrderQuantityTextBox.Text),
            FitPriceRange = double.Parse(FitRangePriceTextBox.Text, CultureInfo.InvariantCulture),
            ItemListSize = int.Parse(ItemListSizeTextBox.Text),
            ListingFindRange = int.Parse(ListingFindRangeTextBox.Text),
            MaxPrice = double.Parse(MaxPriceTextBox.Text, CultureInfo.InvariantCulture),
            MinPrice = double.Parse(MinPriceTextBox.Text, CultureInfo.InvariantCulture),
            RequiredProfit = double.Parse(RequiredProfitTextBox.Text, CultureInfo.InvariantCulture),
            Trend = double.Parse(TrendTextBox.Text, CultureInfo.InvariantCulture),
            SalesPerWeek = int.Parse(SalesPerWeekTextBox.Text),
            SteamUserId = SteamUserIdTextBox.Text,
            SteamCommission = double.Parse(SteamCommissionTextBox.Text, CultureInfo.InvariantCulture)
        };
    }

    private void ResetSettingsButton_Click(object sender, EventArgs e)
    {
        MessageBox.Show(@"Erase all settings?", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        AvailibleBalanceTextBox.Text = string.Empty;
        AnalysisIntervalComboBox.SelectedText = string.Empty;
        AveragePriceTextBox.Text = string.Empty;
        OrderQuantityTextBox.Text = string.Empty;
        FitRangePriceTextBox.Text = string.Empty;
        ItemListSizeTextBox.Text = string.Empty;
        ListingFindRangeTextBox.Text = string.Empty;
        MaxPriceTextBox.Text = string.Empty;
        MinPriceTextBox.Text = string.Empty;
        RequiredProfitTextBox.Text = string.Empty;
        TrendTextBox.Text = string.Empty;
        SalesPerWeekTextBox.Text = string.Empty;
        SteamCommissionTextBox.Text = string.Empty;
        SteamCommissionTextBox.Text = string.Empty;
        Configuration = null;
        Program.EraseConfiguration();
        MessageBox.Show(@"Settings was erased!", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void SaveSettingsButton_Click(object sender, EventArgs e)
    {
        try
        {
            var currentConfiguration = GetCurrentConfiguration();
            Program.SaveConfiguration(currentConfiguration);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        MessageBox.Show(@"Configuration was saved!", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}