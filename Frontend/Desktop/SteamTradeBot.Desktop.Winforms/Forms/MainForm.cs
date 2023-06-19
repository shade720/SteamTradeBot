namespace SteamTradeBot.Desktop.Winforms.Forms;

public partial class MainForm : Form
{
    private WorkerForm _workerForm;
    public MainForm()
    {
        InitializeComponent();
    }

    private void WorkerNavButton_Click(object sender, EventArgs e)
    {
        _workerForm = new WorkerForm();
        _workerForm.TopLevel = false;
        Frame.Controls.Add(_workerForm);
        _workerForm.Show();
    }
}