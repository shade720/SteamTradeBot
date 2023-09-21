using SteamTradeBot.Desktop.Winforms.Models;
using SteamTradeBot.Desktop.Winforms.BusinessLogicLayer.ServiceAccess;
using SteamTradeBot.Desktop.Winforms.BusinessLogicLayer;
using SteamTradeBot.Desktop.Winforms.Models.DTOs;

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
        _signalRClient.OnStateRefreshEvent += RefreshServiceStatePanel;
        _signalRClient.OnHistoryRefreshEvent += RefreshHistoryTable;
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
        StopButton.Enabled = true;
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
                eventInfo.Time,
                eventInfo.Info,
                type,
                eventInfo.BuyPrice == 0 ? string.Empty : eventInfo.BuyPrice,
                eventInfo.SellPrice == 0 ? string.Empty : eventInfo.SellPrice,
                eventInfo.Profit == 0 ? string.Empty : eventInfo.Profit);
            HistoryDataGridView.Rows[rowIndex].DefaultCellStyle.BackColor = eventInfo.Type switch
            {
                InfoType.ItemAnalyzed => Color.White,
                InfoType.ItemBought => Color.Orange,
                InfoType.ItemSold => Color.Chartreuse,
                InfoType.ItemCanceled => Color.Gray,
                InfoType.Error => Color.Red,
                InfoType.Warning => Color.Yellow,
                _ => throw new ArgumentOutOfRangeException()
            };
        });
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
        await _signalRClient.Connect();
        var initState = await _restClient.GetInitState();
        RefreshServiceStatePanel(initState);
        var initTradingHistory = await _restClient.GetInitHistory();
        foreach (var tradingEvent in initTradingHistory)
        {
            RefreshHistoryTable(tradingEvent);
        }
    }

    private async void WorkerForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        await _signalRClient.Disconnect();
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