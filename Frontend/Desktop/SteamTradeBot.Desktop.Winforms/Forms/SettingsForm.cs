using SteamTradeBot.Desktop.Winforms.BLL;

namespace SteamTradeBot.Desktop.Winforms.Forms;

public partial class SettingsForm : Form
{
    private readonly SteamTradeBotRestClient _steamTradeBotRestClient;

    public SettingsForm(SteamTradeBotRestClient steamTradeBotRestClient)
    {
        _steamTradeBotRestClient = steamTradeBotRestClient;
        InitializeComponent();
    }
}