using SteamTradeBot.Desktop.Winforms.Models;
using SteamTradeBot.Desktop.Winforms.ServiceAccess;

namespace SteamTradeBot.Desktop.Winforms.Forms;

public partial class MainForm : Form
{
    private readonly WorkerForm _workerForm;
    private readonly SettingsForm _settingsForm;
    private readonly LogInForm _logInForm;

    private readonly SteamTradeBotRestClient _steamTradeBotRestClient;
    public MainForm()
    {
        InitializeComponent();
        _steamTradeBotRestClient = new SteamTradeBotRestClient();

        _workerForm = new WorkerForm(_steamTradeBotRestClient);
        _workerForm.OnWorkingStateChangedEvent += OnWorkingStateChanged;

        _settingsForm = new SettingsForm(_steamTradeBotRestClient);
        _logInForm = new LogInForm(_steamTradeBotRestClient);
        _logInForm.OnAuthenticationStartEvent += OnAuthenticationStart;
        _logInForm.OnAuthenticationEndEvent += OnAuthenticationEnd;

        _settingsForm.TopLevel = false;
        _workerForm.TopLevel = false;
        _logInForm.TopLevel = false;

        Frame.Controls.Add(_workerForm);
        Frame.Controls.Add(_settingsForm);
        Frame.Controls.Add(_logInForm);
        _workerForm.Show();
    }

    #region LogIn

    private void LogInButton_Click(object sender, EventArgs e)
    {
        ShowLogInForm();
    }

    private void SignInLabel_Click(object sender, EventArgs e)
    {
        ShowLogInForm();
    }

    private async void LogOutButton_Click(object sender, EventArgs e)
    {
        await LogOutAndRecover();
    }

    private async void LogOutLabel_Click(object sender, EventArgs e)
    {
        await LogOutAndRecover();
    }

    private async Task LogOutAndRecover()
    {
        await _steamTradeBotRestClient.LogOut();
        LogInButton.Visible = true;
        LogOutButton.Visible = false;
        SignInLabel.Visible = true;
        LogOutLabel.Visible = false;
    }

    private void ShowLogInForm()
    {
        _settingsForm.Hide();
        _workerForm.Hide();
        _logInForm.Show();
    }

    private void OnAuthenticationStart(string message)
    {
        StartDisplayLoadingIcon(message);
    }

    private void OnAuthenticationEnd(string message)
    {
        StopDisplayLoadingIcon();
        LogInButton.Visible = false;
        LogOutButton.Visible = true;
        _settingsForm.Hide();
        _workerForm.Show();
        _logInForm.Hide();
        SignInLabel.Visible = false;
        LogOutLabel.Visible = true;
        LogOutLabel.Text = message;
    }

    #endregion

    private void OnWorkingStateChanged(StateInfo.ServiceWorkingState state)
    {
        ServiceStatePanel.BackColor = state switch
        {
            StateInfo.ServiceWorkingState.Up => Color.PaleGreen,
            StateInfo.ServiceWorkingState.Down => Color.Orange,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
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

    private void WorkerNavButton_Click(object sender, EventArgs e)
    {
        _settingsForm.Hide();
        _workerForm.Show();
        _logInForm.Hide();
    }

    private void SettingsNavButton_Click(object sender, EventArgs e)
    {
        _settingsForm.Show();
        _workerForm.Hide();
        _logInForm.Hide();
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        _logInForm.OnAuthenticationStartEvent -= OnAuthenticationStart;
        _logInForm.OnAuthenticationEndEvent -= OnAuthenticationEnd;
        _workerForm.Dispose();
        _settingsForm.Dispose();
        _logInForm.Dispose();
        _steamTradeBotRestClient.Dispose();
    }
}