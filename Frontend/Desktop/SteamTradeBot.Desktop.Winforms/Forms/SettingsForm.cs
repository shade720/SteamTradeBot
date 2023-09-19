using System.Dynamic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SteamTradeBot.Desktop.Winforms.BusinessLogicLayer.ServiceAccess;
using SteamTradeBot.Desktop.Winforms.Models;
using SteamTradeBot.Desktop.Winforms.Models.DTOs;

namespace SteamTradeBot.Desktop.Winforms.Forms;

public partial class SettingsForm : Form
{
    private readonly SteamTradeBotRestClient _steamTradeBotRestClient;
    private readonly string _secret;

    public SettingsForm(SteamTradeBotRestClient steamTradeBotRestClient)
    {
        InitializeComponent();
        _steamTradeBotRestClient = steamTradeBotRestClient;

        var settings = Program.LoadConfiguration() ?? new ApplicationSettings();
        AvailibleBalanceTextBox.Text = settings.AvailableBalance.ToString(CultureInfo.InvariantCulture);
        AnalysisIntervalComboBox.SelectedItem = settings.AnalysisIntervalDays.ToString();
        AveragePriceRatioTextBox.Text = settings.AveragePriceRatio.ToString(CultureInfo.InvariantCulture);
        OrderQuantityTextBox.Text = settings.OrderQuantity.ToString();
        FitRangePriceTextBox.Text = settings.FitPriceRange.ToString(CultureInfo.InvariantCulture);
        ItemListSizeTextBox.Text = settings.ItemListSize.ToString(CultureInfo.InvariantCulture);
        SellListingFindRangeTextBox.Text = settings.SellListingFindRange.ToString();
        MaxPriceTextBox.Text = settings.MaxPrice.ToString(CultureInfo.InvariantCulture);
        MinPriceTextBox.Text = settings.MinPrice.ToString(CultureInfo.InvariantCulture);
        RequiredProfitTextBox.Text = settings.RequiredProfit.ToString(CultureInfo.InvariantCulture);
        TrendTextBox.Text = settings.Trend.ToString(CultureInfo.InvariantCulture);
        SalesPerDayTextBox.Text = settings.SalesPerDay.ToString(CultureInfo.InvariantCulture);
        SteamUserIdTextBox.Text = settings.SteamUserId;
        SteamCommissionTextBox.Text = settings.SteamCommission.ToString(CultureInfo.InvariantCulture);
        SalesRatio.Text = settings.SalesRatio.ToString(CultureInfo.InvariantCulture);
        ConnectionAddressTextBox.Text = settings.ServerAddress;
        LogInTextBox.Text = settings.Login;
        PasswordTextBox.Text = settings.Password;
        ApiKeyTextBox.Text = settings.ApiKey;

        if (string.IsNullOrEmpty(settings.Secret))
        {
            MaFilePathTextBox.Text = string.Empty;
            MaFilePathTextBox.Enabled = true;
            _secret = string.Empty;
        }
        else
        {
            MaFilePathTextBox.Text = @"The secret is loaded";
            MaFilePathTextBox.Enabled = false;
            _secret = settings.Secret;
        }
    }

    private void ChooseMaFileButton_Click(object sender, EventArgs e)
    {
        if (OpenFileDialog.ShowDialog() != DialogResult.OK) return;
        MaFilePathTextBox.Text = OpenFileDialog.FileName;
        MaFilePathTextBox.Enabled = true;
    }

    private static string? GetSecret(string maFilePath)
    {
        if (!File.Exists(maFilePath))
            return null;
        var maFileJson = File.ReadAllText(maFilePath);
        var jsonSettings = new JsonSerializerSettings();
        jsonSettings.Converters.Add(new ExpandoObjectConverter());
        jsonSettings.Converters.Add(new StringEnumConverter());
        dynamic? maFile = JsonConvert.DeserializeObject<ExpandoObject>(maFileJson, jsonSettings);
        return maFile is null
            ? null
            : ((IDictionary<string, object>)maFile)["shared_secret"].ToString();
    }

    private ApplicationSettings GetCurrentConfiguration()
    {
        return new ApplicationSettings
        {
            ApiKey = ApiKeyTextBox.Text,
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
            SalesRatio = double.Parse(SalesRatio.Text, CultureInfo.InvariantCulture),
            Login = LogInTextBox.Text,
            Password = PasswordTextBox.Text,
            Secret = _secret,
            ServerAddress = ConnectionAddressTextBox.Text
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
        ConnectionAddressTextBox.Text = string.Empty;
        LogInTextBox.Text = string.Empty;
        PasswordTextBox.Text = string.Empty;
        MaFilePathTextBox.Enabled = true;
        MaFilePathTextBox.Text = string.Empty;
        ApiKeyTextBox.Text = string.Empty;

        Program.EraseConfiguration();

        MessageBox.Show(@"Settings was erased!", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private async void SaveSettingsButton_Click(object sender, EventArgs e)
    {
        var currentConfiguration = GetCurrentConfiguration();

        EnsureThatSecretIsLoaded(currentConfiguration);
        Program.SaveConfiguration(currentConfiguration);

        MessageBox.Show(@"Configuration was saved locally!", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        try
        {
            await _steamTradeBotRestClient.UploadSettings(new RemoteSettings(currentConfiguration));
            MessageBox.Show(@"Configuration was saved remotely!", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void EnsureThatSecretIsLoaded(ApplicationSettings settings)
    {
        if (!string.IsNullOrEmpty(settings.Secret))
            return;
        if (!string.IsNullOrEmpty(MaFilePathTextBox.Text))
        {
            var secret = GetSecret(MaFilePathTextBox.Text);
            if (secret is null)
            {
                MessageBox.Show(@"Can't extract the secret from this maFile", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            settings.Secret = secret;
        }
        else
        {
            MessageBox.Show(@"Wrong maFile", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}