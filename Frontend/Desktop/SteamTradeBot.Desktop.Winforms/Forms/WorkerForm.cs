using SteamTradeBot.Desktop.Winforms.Models;
using SteamTradeBot.Desktop.Winforms.BusinessLogicLayer.ServiceAccess;
using SteamTradeBot.Desktop.Winforms.BusinessLogicLayer;
using SteamTradeBot.Desktop.Winforms.Models.DTOs;
using System.Data;
using System.Windows.Forms;

namespace SteamTradeBot.Desktop.Winforms.Forms;

public partial class WorkerForm : Form
{
    private readonly SteamTradeBotRestClient _restClient;
    private readonly SteamTradeBotSignalRClient _signalRClient;

    public delegate void OnWorkingStateChanged(StateInfo state);
    public event OnWorkingStateChanged? OnWorkingStateChangedEvent;

    public WorkerForm(SteamTradeBotRestClient restClient, SteamTradeBotSignalRClient signalRClient)
    {
        InitializeComponent();
        _restClient = restClient;
        _signalRClient = signalRClient;
    }

    private async void StartButton_Click(object sender, EventArgs e)
    {
        try
        {
            var settings = Program.LoadConfiguration();
            if (settings is null)
            {
                MessageBox.Show(@"Incorrect credentials!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            StartButton.Enabled = false;
            await _restClient.Start(new Credentials
            {
                Login = settings.Login,
                Password = settings.Password,
                Secret = settings.Secret
            });
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        StartButton.Enabled = true;
    }

    private async void StopButton_Click(object sender, EventArgs e)
    {
        try
        {
            StopButton.Enabled = false;
            await _restClient.Stop();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void CancelOrdersButtons_Click(object sender, EventArgs e)
    {
        try
        {
            CancelOrdersButtons.Enabled = false;
            await _restClient.CancelOrders();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        CancelOrdersButtons.Enabled = true;
    }

    private async void CheckConnectionButton_Click(object sender, EventArgs e)
    {
        var currentState = await _restClient.GetInitState();
        RefreshServiceStatePanel(currentState);
    }

    private void RefreshServiceStatePanel(StateInfo state)
    {
        OnWorkingStateChangedEvent?.Invoke(state);
        ThreadHelperClass.ExecOnForm(this, () => ItemsAnalyzedLabel.Text = state.ItemsAnalyzed.ToString());
        ThreadHelperClass.ExecOnForm(this, () => ItemsBoughtLabel.Text = state.ItemsBought.ToString());
        ThreadHelperClass.ExecOnForm(this, () => ItemsSoldLabel.Text = state.ItemsSold.ToString());
        ThreadHelperClass.ExecOnForm(this, () => ErrorsLabel.Text = state.Errors.ToString());
        ThreadHelperClass.ExecOnForm(this, () => WarningsLabel.Text = state.Warnings.ToString());
        ThreadHelperClass.ExecOnForm(this, () => UptimeLabel.Text = state.Uptime.ToString(@"dd\.hh\:mm\:ss"));
        ThreadHelperClass.ExecOnForm(this, () => ConnectionStateLabel.Text = state.Connection.ToString());
        ThreadHelperClass.ExecOnForm(this, () => ServiceStateLabel.Text = state.WorkingState.ToString());
        ThreadHelperClass.ExecOnForm(this, () => SetWorkingStateControlsVisibility(state.WorkingState == StateInfo.ServiceWorkingState.Up));
    }

    private void RefreshHistoryTable(TradingEvent eventInfo)
    {
        if (eventInfo.Type == InfoType.ItemAnalyzed)
            return;
        ThreadHelperClass.ExecOnForm(this, () =>
        {
            var type = eventInfo.Type switch
            {
                InfoType.ItemAnalyzed => "Analyzed",
                InfoType.ItemBought => "Bought",
                InfoType.ItemSold => "Sold",
                InfoType.ItemCanceled => "Canceled",
                InfoType.Error => "Error",
                InfoType.Warning => "Warning",
                _ => throw new ArgumentOutOfRangeException()
            };
            var rowIndex = HistoryDataGridView.Rows.Add(
                eventInfo.Time.ToLocalTime(),
                eventInfo.Info,
                type,
                eventInfo.BuyPrice == 0 ? string.Empty : eventInfo.BuyPrice,
                eventInfo.SellPrice == 0 ? string.Empty : eventInfo.SellPrice,
                eventInfo.Profit == 0 ? string.Empty : eventInfo.Profit);
            HistoryDataGridView.Rows[rowIndex].DefaultCellStyle.ForeColor = eventInfo.Type switch
            {
                InfoType.ItemAnalyzed => Color.White,
                InfoType.ItemBought => Color.DarkOrange,
                InfoType.ItemSold => Color.LimeGreen,
                InfoType.ItemCanceled => Color.Gray,
                InfoType.Error => Color.Red,
                InfoType.Warning => Color.Yellow,
                _ => throw new ArgumentOutOfRangeException()
            };
            var filter = HistoryFilterComboBox.SelectedItem.ToString();
            HistoryDataGridView.Rows[rowIndex].Visible = filter == "All" || filter == type;
            HistoryDataGridView.FirstDisplayedScrollingRowIndex = HistoryDataGridView.RowCount - 1;
        });
    }

    private void HistoryFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (HistoryDataGridView.RowCount <= 0)
            return;
        var filter = HistoryFilterComboBox.SelectedItem.ToString();
        foreach (var row in HistoryDataGridView.Rows.OfType<DataGridViewRow>())
        {
            row.Visible = filter == "All" ||
                          filter == row.Cells[2].Value.ToString();
        }
    }

    private void SetWorkingStateControlsVisibility(bool isWorking)
    {
        StartButton.Visible = !isWorking;
        StopButton.Visible = isWorking;
    }

    private async void ViewLogsButton_Click(object sender, EventArgs e)
    {
        try
        {
            var logs = await _restClient.GetLogFile();
            var logsForm = new LogForm(logs);
            logsForm.Show();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void WorkerForm_Load(object sender, EventArgs e)
    {
        _signalRClient.OnStateRefreshEvent += RefreshServiceStatePanel;
        _signalRClient.OnHistoryRefreshEvent += RefreshHistoryTable;
        HistoryFilterComboBox.SelectedIndex = 0;
        var initState = await _restClient.GetInitState();
        RefreshServiceStatePanel(initState);
        var initTradingHistory = await _restClient.GetInitHistory();
        foreach (var tradingEvent in initTradingHistory.OrderBy(x => x.Time))
        {
            RefreshHistoryTable(tradingEvent);
        }
    }

    private async void WorkerForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        _signalRClient.OnStateRefreshEvent -= RefreshServiceStatePanel;
        _signalRClient.OnHistoryRefreshEvent -= RefreshHistoryTable;
    }

    private async void ResetStateButton_Click(object sender, EventArgs e)
    {
        try
        {
            await _restClient.ResetState();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}