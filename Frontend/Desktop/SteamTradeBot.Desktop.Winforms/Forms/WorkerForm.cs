﻿using SteamTradeBot.Desktop.Winforms.ServiceAccess;

namespace SteamTradeBot.Desktop.Winforms.Forms;

public partial class WorkerForm : Form
{
    private readonly SteamTradeBotRestClient _steamTradeBotServiceClient;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private const int PollingDelayMs = 1000;

    public WorkerForm(SteamTradeBotRestClient steamTradeBotServiceClient)
    {
        InitializeComponent();
        _steamTradeBotServiceClient = steamTradeBotServiceClient;
        _cancellationTokenSource = new CancellationTokenSource();
        Task.Run(async () => await RefreshServiceStateLoop(), _cancellationTokenSource.Token);
    }

    private async void StartButton_Click(object sender, EventArgs e)
    {
        try
        {
            StartButton.Visible = false;
            StopButton.Visible = true;
            await _steamTradeBotServiceClient.Start();
        }
        catch (Exception exception)
        {
            StartButton.Visible = true;
            StopButton.Visible = false;
            MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void StopButton_Click(object sender, EventArgs e)
    {
        try
        {
            StartButton.Visible = true;
            StopButton.Visible = false;
            await _steamTradeBotServiceClient.Stop();
        }
        catch (Exception exception)
        {
            StartButton.Visible = false;
            StopButton.Visible = true;
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

    private async Task RefreshServiceStateLoop()
    {
        if (_cancellationTokenSource is null)
            throw new ApplicationException("CancellationToken was null!");
        while (!_cancellationTokenSource.IsCancellationRequested)
        {
            await RefreshServiceStatePanel();
            await Task.Delay(PollingDelayMs);
        }
    }

    private async void CheckConnectionButton_Click(object sender, EventArgs e)
    {
        await RefreshServiceStatePanel();
    }

    private async Task RefreshServiceStatePanel()
    {
        var state = await _steamTradeBotServiceClient.CheckState();
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

    private void WorkerForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        _cancellationTokenSource.Cancel();
    }

    private void ViewLogsButton_Click(object sender, EventArgs e)
    {

    }
}