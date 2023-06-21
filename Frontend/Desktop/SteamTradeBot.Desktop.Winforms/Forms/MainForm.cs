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
        _settingsForm = new SettingsForm(_steamTradeBotRestClient);
        _logInForm = new LogInForm(_steamTradeBotRestClient);
        _logInForm.OnLoggedInEvent += OnLoggedInEvent;

        _settingsForm.TopLevel = false;
        _workerForm.TopLevel = false;
        _logInForm.TopLevel = false;

        Frame.Controls.Add(_workerForm);
        Frame.Controls.Add(_settingsForm);
        Frame.Controls.Add(_logInForm);
        _workerForm.Show();
    }

    private void OnLoggedInEvent()
    {
        LogInButton.Visible = false;
        LogOutButton.Visible = true;
        _settingsForm.Hide();
        _workerForm.Show();
        _logInForm.Hide();
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

    private void LogInButton_Click(object sender, EventArgs e)
    {
        _settingsForm.Hide();
        _workerForm.Hide();
        _logInForm.Show();
    }

    private async void LogOutButton_Click(object sender, EventArgs e)
    {
        await _steamTradeBotRestClient.LogOut();
        LogInButton.Visible = true;
        LogOutButton.Visible = false;
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        _logInForm.OnLoggedInEvent -= OnLoggedInEvent;
        _workerForm.Dispose();
        _settingsForm.Dispose();
        _logInForm.Dispose();
        _steamTradeBotRestClient.Dispose();
    }
}