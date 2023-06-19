using SteamTradeBot.Desktop.Winforms.BLL;

namespace SteamTradeBot.Desktop.Winforms.Forms;

public partial class WorkerForm : Form
{
    private readonly SteamTradeBotRestClient _steamTradeBotServiceClient;
    public WorkerForm(SteamTradeBotRestClient steamTradeBotServiceClient)
    {
        InitializeComponent();
        _steamTradeBotServiceClient = steamTradeBotServiceClient;
    }

    private async void StartButton_Click(object sender, EventArgs e)
    {
        await _steamTradeBotServiceClient.Start();
    }

    private async void StopButton_Click(object sender, EventArgs e)
    {
        await _steamTradeBotServiceClient.Stop();
    }
}