using Newtonsoft.Json;
using SteamTradeBot.Desktop.Winforms.Forms;
using SteamTradeBot.Desktop.Winforms.Models;

namespace SteamTradeBot.Desktop.Winforms;

internal static class Program
{
    private static readonly string TempPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SteamTradeBotTemp");
    private const string SettingsFileName = "configuration.json";
    private static readonly string SettingsPath = Path.Combine(TempPath, SettingsFileName);

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        if (!Directory.Exists(TempPath))
            Directory.CreateDirectory(TempPath);
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }

    public static ApplicationSettings? LoadConfiguration()
    {
        if (!File.Exists(SettingsPath))
            return null;
        var configurationJson = File.ReadAllText(SettingsPath);
        try
        {
            return JsonConvert.DeserializeObject<ApplicationSettings>(configurationJson);
        }
        catch
        {
            return null;
        }
    }

    public static void SaveConfiguration(ApplicationSettings remoteConfiguration)
    {
        var configurationJson = JsonConvert.SerializeObject(remoteConfiguration);
        File.WriteAllText(SettingsPath, configurationJson);
    }

    public static void EraseConfiguration()
    {
        File.Delete(SettingsPath);
    }
}