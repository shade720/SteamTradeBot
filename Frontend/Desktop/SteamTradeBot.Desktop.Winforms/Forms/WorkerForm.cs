using SteamTradeBot.Desktop.Winforms.ServiceAccess;
using System.ComponentModel;
using SteamTradeBot.Desktop.Winforms.Models;

namespace SteamTradeBot.Desktop.Winforms.Forms;

public partial class WorkerForm : Form
{
    private readonly SteamTradeBotRestClient _steamTradeBotServiceClient;
    private const int PollingDelayMs = 1000;

    public delegate void OnWorkingStateChanged(StateInfo state);
    public event OnWorkingStateChanged? OnWorkingStateChangedEvent;

    public WorkerForm(SteamTradeBotRestClient steamTradeBotServiceClient)
    {
        InitializeComponent();
        _steamTradeBotServiceClient = steamTradeBotServiceClient;
    }

    private async void StartButton_Click(object sender, EventArgs e)
    {
        try
        {
            await _steamTradeBotServiceClient.Start();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void StopButton_Click(object sender, EventArgs e)
    {
        try
        {
            await _steamTradeBotServiceClient.Stop();
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
            await _steamTradeBotServiceClient.CancelOrders();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void StateRefresher_DoWork(object sender, DoWorkEventArgs e)
    {
        while (!StateRefresher.CancellationPending)
        {
            await RefreshServiceStatePanel();
            await Task.Delay(PollingDelayMs);
        }
        e.Cancel = true;
    }

    private async void CheckConnectionButton_Click(object sender, EventArgs e)
    {
        await RefreshServiceStatePanel();
    }

    private async Task RefreshServiceStatePanel()
    {
        var state = await _steamTradeBotServiceClient.CheckState();
        OnWorkingStateChangedEvent?.Invoke(state);
        ThreadHelperClass.ExecOnForm(this, () => ItemsAnalyzedLabel.Text = state.ItemsAnalyzed.ToString());
        ThreadHelperClass.ExecOnForm(this, () => ItemsBoughtLabel.Text = state.ItemsBought.ToString());
        ThreadHelperClass.ExecOnForm(this, () => ItemsSoldLabel.Text = state.ItemsSold.ToString());
        ThreadHelperClass.ExecOnForm(this, () => ErrorsLabel.Text = state.Errors.ToString());
        ThreadHelperClass.ExecOnForm(this, () => WarningsLabel.Text = state.Warnings.ToString());
        ThreadHelperClass.ExecOnForm(this, () => UptimeLabel.Text = state.Uptime.ToString(@"dd\.hh\:mm\:ss"));
        ThreadHelperClass.ExecOnForm(this, () => ConnectionStateLabel.Text = state.Connection.ToString());
        ThreadHelperClass.ExecOnForm(this, () => ServiceStateLabel.Text = state.WorkingState.ToString());
        ThreadHelperClass.ExecOnForm(this, () =>
        {
            foreach (var eventInfo in state.Events)
            {
                HistoryDataGridView.Rows.Add(eventInfo.Split('-'));
            }
        });
        ThreadHelperClass.ExecOnForm(this, () => WorkingStateControlsVisibility(state.WorkingState == StateInfo.ServiceWorkingState.Up));
    }

    private void WorkingStateControlsVisibility(bool isWorking)
    {
        StartButton.Visible = !isWorking;
        StopButton.Visible = isWorking;
    }


    private void WorkerForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        StateRefresher.CancelAsync();
    }

    private async void ViewLogsButton_Click(object sender, EventArgs e)
    {
        var logs = await _steamTradeBotServiceClient.GetLogFile();
        var logsForm = new LogForm(logs);
        logsForm.Show();
    }

    private void WorkerForm_Load(object sender, EventArgs e)
    {
        if (StateRefresher.IsBusy != true)
            StateRefresher.RunWorkerAsync();
    }
}