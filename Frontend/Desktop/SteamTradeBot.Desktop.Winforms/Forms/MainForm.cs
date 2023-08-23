using SteamTradeBot.Desktop.Winforms.Models;
using SteamTradeBot.Desktop.Winforms.ServiceAccess;

namespace SteamTradeBot.Desktop.Winforms.Forms;

public partial class MainForm : Form
{
    private readonly WorkerForm _workerForm;
    private readonly SettingsForm _settingsForm;
    private readonly LogInForm _logInForm;

    public MainForm()
    {
        InitializeComponent();

        var steamTradeBotRestClient = new SteamTradeBotRestClient(new HttpClientProvider(), new ApiKeyProvider());
        _workerForm = new WorkerForm(steamTradeBotRestClient);

        _settingsForm = new SettingsForm(steamTradeBotRestClient);
        _logInForm = new LogInForm();

        _settingsForm.TopLevel = false;
        _settingsForm.Dock = DockStyle.Fill;
        _workerForm.TopLevel = false;
        _workerForm.Dock = DockStyle.Fill;
        _logInForm.TopLevel = false;
        _logInForm.Dock = DockStyle.Fill;

        Frame.Controls.Add(_workerForm);
        Frame.Controls.Add(_settingsForm);
        Frame.Controls.Add(_logInForm);
        _workerForm.Show();
    }

    #region LogIn

    private void LogInButton_Click(object sender, EventArgs e) => ShowLogInForm();
    private void LogInLabel_Click(object sender, EventArgs e) => ShowLogInForm();
    private void ShowLogInForm()
    {
        _settingsForm.Hide();
        _workerForm.Hide();
        _logInForm.Show();
    }

    #endregion

    #region Worker

    private void WorkerNavButton_Click(object sender, EventArgs e)
    {
        _settingsForm.Hide();
        _workerForm.Show();
        _logInForm.Hide();
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

        if (_logInForm.State == state.IsLoggedIn)
            return;
        switch (state.IsLoggedIn)
        {
            case StateInfo.LogInState.Pending:
                ThreadHelperClass.ExecOnForm(this, () =>
                {
                    _logInForm.SaveCredentialsButton.Enabled = false;
                    _logInForm.State = StateInfo.LogInState.Pending;
                    StartDisplayLoadingIcon("Connecting to steam API...");
                });
                break;
            case StateInfo.LogInState.LoggedIn:
                ThreadHelperClass.ExecOnForm(this, () =>
                {
                    if (LogOutLabel.Text == state.CurrentUser)
                        return;
                    StopDisplayLoadingIcon();
                    LogInButton.Visible = false;
                    LogOutButton.Visible = true;
                    LogInLabel.Visible = false;
                    LogOutLabel.Visible = true;
                    LogOutLabel.Text = state.CurrentUser;
                    _logInForm.SaveCredentialsButton.Enabled = false;
                    _logInForm.Hide();
                    _workerForm.Show();
                    _settingsForm.Hide();
                    _logInForm.State = StateInfo.LogInState.LoggedIn;
                });
                break;
            case StateInfo.LogInState.NotLoggedIn:
                ThreadHelperClass.ExecOnForm(this, () =>
                {
                    StopDisplayLoadingIcon();
                    LogInButton.Visible = true;
                    LogOutButton.Visible = false;
                    LogInLabel.Visible = true;
                    LogOutLabel.Visible = false;
                    LogOutLabel.Text = string.Empty;
                    _logInForm.SaveCredentialsButton.Enabled = true;
                    _logInForm.State = StateInfo.LogInState.NotLoggedIn;
                });
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
        _logInForm.Hide();
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        _workerForm.Dispose();
        _settingsForm.Dispose();
        _logInForm.Dispose();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        _workerForm.OnWorkingStateChangedEvent += OnStateChanged;
    }
}