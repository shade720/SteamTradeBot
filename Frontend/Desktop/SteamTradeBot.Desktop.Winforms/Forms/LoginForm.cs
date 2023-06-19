using SteamTradeBot.Desktop.Winforms.BLL;

namespace SteamTradeBot.Desktop.Winforms.Forms;

public partial class LogInForm : Form
{
    private readonly SteamTradeBotRestClient _steamTradeBotRestClient;

    public delegate void OnLoggedIn();
    public event OnLoggedIn? OnLoggedInEvent;

    public LogInForm(SteamTradeBotRestClient steamTradeBotRestClient)
    {
        _steamTradeBotRestClient = steamTradeBotRestClient;
        InitializeComponent();
    }

    private async void LogInButton_Click(object sender, EventArgs e)
    {
        var credentials = new Credentials
        {
            Login = LogInTextBox.Text,
            Password = PasswordTextBox.Text,
            Token = TokenTextBox.Text
        };
        await _steamTradeBotRestClient.LogIn(credentials);
        OnLoggedInEvent?.Invoke();
    }
}