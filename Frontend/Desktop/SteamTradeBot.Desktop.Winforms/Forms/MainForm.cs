using SteamTradeBot.Desktop.Winforms.BusinessLogicLayer;
using SteamTradeBot.Desktop.Winforms.BusinessLogicLayer.ServiceAccess;
using SteamTradeBot.Desktop.Winforms.Models;

namespace SteamTradeBot.Desktop.Winforms.Forms;

public partial class MainForm : Form
{
    private readonly WorkerForm _workerForm;
    private readonly SettingsForm _settingsForm;

    public MainForm()
    {
        InitializeComponent();

        var steamTradeBotRestClient = new SteamTradeBotRestClient(new HttpClientProvider(), new ApiKeyProvider());
        var steamTradeBotSignalRClient = new SteamTradeBotSignalRClient(new ApiKeyProvider());
        _workerForm = new WorkerForm(steamTradeBotRestClient, steamTradeBotSignalRClient);
        _settingsForm = new SettingsForm(steamTradeBotRestClient);

        _settingsForm.TopLevel = false;
        _settingsForm.Dock = DockStyle.Fill;
        _workerForm.TopLevel = false;
        _workerForm.Dock = DockStyle.Fill;

        Frame.Controls.Add(_workerForm);
        Frame.Controls.Add(_settingsForm);
        _workerForm.Show();
    }

    #region Worker

    private void WorkerNavButton_Click(object sender, EventArgs e)
    {
        _settingsForm.Hide();
        _workerForm.Show();
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
    }

    #endregion

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

    private void SettingsNavButton_Click(object sender, EventArgs e)
    {
        _settingsForm.Show();
        _workerForm.Hide();
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        _workerForm.Dispose();
        _settingsForm.Dispose();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        _workerForm.OnWorkingStateChangedEvent += OnStateChanged;
    }
}