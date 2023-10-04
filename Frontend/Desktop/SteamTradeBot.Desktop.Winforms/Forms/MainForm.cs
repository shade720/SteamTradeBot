using SteamTradeBot.Desktop.Winforms.BusinessLogicLayer;
using SteamTradeBot.Desktop.Winforms.BusinessLogicLayer.Models;
using SteamTradeBot.Desktop.Winforms.BusinessLogicLayer.ServiceAccess;

namespace SteamTradeBot.Desktop.Winforms.Forms;

public partial class MainForm : Form
{
    private readonly WorkerForm _workerForm;
    private readonly SettingsForm _settingsForm;
    private readonly StatsForm _statsForm;
    private readonly SteamTradeBotSignalRClient _steamTradeBotSignalRClient;

    public MainForm()
    {
        InitializeComponent();

        var steamTradeBotRestClient = new SteamTradeBotRestClient(new HttpClientProvider(), new ApiKeyProvider());
        _steamTradeBotSignalRClient = new SteamTradeBotSignalRClient(new ApiKeyProvider());
        _workerForm = new WorkerForm(steamTradeBotRestClient, _steamTradeBotSignalRClient);
        _settingsForm = new SettingsForm(steamTradeBotRestClient);
        _statsForm = new StatsForm(steamTradeBotRestClient, _steamTradeBotSignalRClient);

        _settingsForm.TopLevel = false;
        _settingsForm.Dock = DockStyle.Fill;
        _workerForm.TopLevel = false;
        _workerForm.Dock = DockStyle.Fill;
        _statsForm.TopLevel = false;
        _statsForm.Dock = DockStyle.Fill;

        Frame.Controls.Add(_workerForm);
        Frame.Controls.Add(_settingsForm);
        Frame.Controls.Add(_statsForm);
        _workerForm.Show();
    }

    private async void MainForm_Load(object sender, EventArgs e)
    {
        await _steamTradeBotSignalRClient.Connect();
        _workerForm.OnWorkingStateChangedEvent += OnStateChanged;
    }

    private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        await _steamTradeBotSignalRClient.Disconnect();
        _workerForm.Dispose();
        _settingsForm.Dispose();
    }

    #region Worker

    private void WorkerNavButton_Click(object sender, EventArgs e)
    {
        _workerForm.Show();
        _settingsForm.Hide();
        _statsForm.Hide();
    }

    private void OnStateChanged(StateInfo state)
    {
        if (state is null)
            return;
        ServiceStatePanel.BackColor = state.WorkingState switch
        {
            StateInfo.ServiceWorkingState.Up => Color.PaleGreen,
            StateInfo.ServiceWorkingState.Down => Color.Orange,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };

        switch (state.IsLoggedIn)
        {
            case StateInfo.LogInState.Pending:
                ThreadHelperClass.ExecOnForm(this, () =>
                {
                    StartDisplayLoadingIcon("Connecting to steam API...");
                });
                break;
            case StateInfo.LogInState.LoggedIn:
                ThreadHelperClass.ExecOnForm(this, StopDisplayLoadingIcon);
                break;
            case StateInfo.LogInState.NotLoggedIn:
                ThreadHelperClass.ExecOnForm(this, StopDisplayLoadingIcon);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return;
    }

    private void StartDisplayLoadingIcon(string startActionText)
    {
        LoadingPictureBox.Visible = true;
        CurrentWorkLabel.Text = startActionText;
        CurrentWorkLabel.Visible = true;
    }

    private void StopDisplayLoadingIcon()
    {
        LoadingPictureBox.Visible = false;
        CurrentWorkLabel.Text = string.Empty;
        CurrentWorkLabel.Visible = false;
    }

    #endregion

    private void SettingsNavButton_Click(object sender, EventArgs e)
    {
        _settingsForm.Show();
        _workerForm.Hide();
        _statsForm.Hide();
    }

    private void StatsNavButton_Click(object sender, EventArgs e)
    {
        _statsForm.Show();
        _settingsForm.Hide();
        _workerForm.Hide();
    }
}