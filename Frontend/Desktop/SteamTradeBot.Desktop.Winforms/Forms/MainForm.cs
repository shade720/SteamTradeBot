namespace SteamTradeBot.Desktop.Winforms.Forms;

public partial class MainForm : Form
{
    private readonly WorkerForm _workerForm;
    private readonly SettingsForm _settingsForm;

    public MainForm()
    {
        InitializeComponent();
        _workerForm = new WorkerForm();
        _settingsForm = new SettingsForm();
        _settingsForm.TopLevel = false;
        _workerForm.TopLevel = false;
        Frame.Controls.Add(_workerForm);
        Frame.Controls.Add(_settingsForm);
        _workerForm.Show();
    }

    private void WorkerNavButton_Click(object sender, EventArgs e)
    {
        _settingsForm.Hide();
        _workerForm.Show();
    }

    private void SettingsNavButton_Click(object sender, EventArgs e)
    {
        _settingsForm.Show();
        _workerForm.Hide();
    }
}