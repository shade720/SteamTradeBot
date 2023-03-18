using System.ComponentModel;

namespace TradeBotClient.Forms
{
    internal partial class LoginForm : Form
    {
        private readonly TradeBotAPIClient _client;
        private bool IsLogged = false;
        private bool IsNeedToSave = false;
        private static string SteamID;
        private readonly List<bool> Errors = new();

        public LoginForm(TradeBotAPIClient client)
        {
            _client = client;
            InitializeComponent();
            for (int i = 0; i < 3; i++) Errors.Add(false);
            KeyDown += LoginForm_KeyDown;
            LoginTextBox.Validating += LoginTextBox_Validating;
            PasswordTextBox.Validating += PasswordTextBox_Validating;
            SteamGuardTextBox.Validating += SteamGuardTextBox_Validating;
            PathTextBox.Validating += SteamGuardTextBox_Validating;
        }
        private async void LoginButton_Click(object sender, EventArgs e)
        {
            _client.LogIn(LoginTextBox.Text, PasswordTextBox.Text, SteamGuardTextBox.Text);
        }
        
        private void Observe_Click(object sender, EventArgs e)
        {
            OpenFileDialog.ShowDialog();
            PathTextBox.Text = OpenFileDialog.FileName;
            PathTextBox.Focus();
        }
        private void SaveLoginAndPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveLoginAndPasswordCheckBox.Checked == true) IsNeedToSave = true;
        }
        private void ClearSavedButton_Click(object sender, EventArgs e)
        {
            //File.Delete(@"Data\SavedLoginAndPassword.txt");
            //Close();
            //new LoginForm();
        }
        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoginButton.PerformClick();
            }
        }
        private void LoginTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTextBox.Text))
            {
                ErrorProvider.SetError(LoginTextBox, "Поле должно быть заполнено!");
                Errors[0] = true;
            } 
            else
            {
                ErrorProvider.SetError(LoginTextBox, string.Empty);
                Errors[0] = false;
            }
        }
        private void SteamGuardTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(SteamGuardTextBox.Text) && string.IsNullOrEmpty(PathTextBox.Text))
            {
                ErrorProvider.SetError(SteamGuardTextBox, "Введите Steam Guard или укажите путь к maFile!");
                Errors[1] = true;
            }
            else
            {
                ErrorProvider.SetError(SteamGuardTextBox, string.Empty);
                Errors[1] = false;
            }
        }
        private void PasswordTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                ErrorProvider.SetError(PasswordTextBox, "Поле должно быть заполнено!");
                Errors[2] = true;
            }
            else
            {
                ErrorProvider.SetError(PasswordTextBox, string.Empty);
                Errors[2] = false;
            }
        }
    }
}