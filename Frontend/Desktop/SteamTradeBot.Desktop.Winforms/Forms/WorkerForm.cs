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
        if (StateRefresher.IsBusy != true)
            StateRefresher.RunWorkerAsync();
    }

    private async void StartButton_Click(object sender, EventArgs e)
    {
        try
        {
            OnServiceWorkingControlsVisibility();
            await _steamTradeBotServiceClient.Start();
        }
        catch (Exception exception)
        {
            OnServiceNotWorkingControlsVisibility();
            MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void StopButton_Click(object sender, EventArgs e)
    {
        try
        {
            OnServiceNotWorkingControlsVisibility();
            await _steamTradeBotServiceClient.Stop();
        }
        catch (Exception exception)
        {
            OnServiceWorkingControlsVisibility();
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
        ThreadHelperClass.SetText(this, ItemsAnalyzedLabel, state.ItemsAnalyzed.ToString());
        ThreadHelperClass.SetText(this, ItemsBoughtLabel, state.ItemsBought.ToString());
        ThreadHelperClass.SetText(this, ItemsSoldLabel, state.ItemsSold.ToString());
        ThreadHelperClass.SetText(this, ErrorsLabel, state.Errors.ToString());
        ThreadHelperClass.SetText(this, WarningsLabel, state.Warnings.ToString());
        ThreadHelperClass.SetText(this, UptimeLabel, state.Uptime.ToString(@"dd\.hh\:mm\:ss"));
        ThreadHelperClass.SetText(this, ConnectionStateLabel, state.Connection.ToString());
        ThreadHelperClass.SetText(this, ServiceStateLabel, state.WorkingState.ToString());
        foreach (var eventInfo in state.Events)
        {
            ThreadHelperClass.AddRow(this, HistoryDataGridView, eventInfo.Split('-'));
        }
    }

    private void OnServiceWorkingControlsVisibility()
    {
        StartButton.Visible = false;
        StopButton.Visible = true;
    }

    private void OnServiceNotWorkingControlsVisibility()
    {
        StartButton.Visible = true;
        StopButton.Visible = false;
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
}