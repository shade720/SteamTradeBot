using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SteamTradeBot.Desktop.Winforms.Models;
using System.Dynamic;
using SteamTradeBot.Desktop.Winforms.ServiceAccess;

namespace SteamTradeBot.Desktop.Winforms.Forms;

public partial class LogInForm : Form
{
    private readonly SteamTradeBotRestClient _steamTradeBotRestClient;
    private readonly Credentials _credentials;
    public StateInfo.LogInState State { get; set; }

    public LogInForm(SteamTradeBotRestClient steamTradeBotRestClient)
    {
        InitializeComponent();
        _steamTradeBotRestClient = steamTradeBotRestClient;
        var savedCredentials = Program.LoadCredentials() ?? new Credentials();
        _credentials = savedCredentials;
        LogInTextBox.Text = savedCredentials.Login;
        PasswordTextBox.Text = savedCredentials.Password;
        if (string.IsNullOrEmpty(_credentials.Secret)) return;
        MaFilePathTextBox.Enabled = false;
        MaFilePathTextBox.Text = @"MaFile is loaded";
    }

    private async void LogInButton_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(LogInTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Text))
        {
            MessageBox.Show(@"Enter the login and the password!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        _credentials.Login = LogInTextBox.Text;
        _credentials.Password = PasswordTextBox.Text;

        if (string.IsNullOrEmpty(_credentials.Secret))
        {
            if (!string.IsNullOrEmpty(TokenTextBox.Text))
            {
                _credentials.Token = TokenTextBox.Text;
            }
            else if (!string.IsNullOrEmpty(MaFilePathTextBox.Text))
            {
                var secret = GetSecret(MaFilePathTextBox.Text);
                if (secret is null)
                {
                    MessageBox.Show(@"Can't extract the secret from this maFile", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _credentials.Secret = secret;
            }
            else
            {
                MessageBox.Show(@"Enter the token or maFilePath!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        try
        {
            await _steamTradeBotRestClient.LogIn(_credentials);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (RememberMeCheckBox.Checked)
            Program.SaveCredentials(_credentials);
    }

    private void ChooseMaFileButton_Click(object sender, EventArgs e)
    {
        if (OpenFileDialog.ShowDialog() == DialogResult.OK)
        {
            MaFilePathTextBox.Text = OpenFileDialog.FileName;
        }
    }
    private static string? GetSecret(string maFilePath)
    {
        var maFileJson = File.ReadAllText(maFilePath);
        var jsonSettings = new JsonSerializerSettings();
        jsonSettings.Converters.Add(new ExpandoObjectConverter());
        jsonSettings.Converters.Add(new StringEnumConverter());
        dynamic? maFile = JsonConvert.DeserializeObject<ExpandoObject>(maFileJson, jsonSettings);
        return maFile is null ? throw new JsonException() : ((IDictionary<string, object>)maFile)["shared_secret"].ToString();
    }

    private void ResetButton_Click(object sender, EventArgs e)
    {
        LogInTextBox.Text = string.Empty;
        PasswordTextBox.Text = string.Empty;
        MaFilePathTextBox.Enabled = true;
        MaFilePathTextBox.Text = string.Empty;
        TokenTextBox.Text = string.Empty;
        Program.EraseCredentials();
        MessageBox.Show(@"Credentials was erased", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}