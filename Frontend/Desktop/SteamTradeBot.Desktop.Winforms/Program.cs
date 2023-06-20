using Newtonsoft.Json;
using SteamTradeBot.Desktop.Winforms.Forms;

namespace SteamTradeBot.Desktop.Winforms;

internal static class Program
{
    public static readonly string TempPath = Path.Combine(Environment.SpecialFolder.ApplicationData.ToString(), "SteamTradeBotTemp");
    private const string SettingsFileName = "configuration.json";
    private const string CredentialsFileName = "credentials.json";
    private const string MaFileFileName = "maFile.json";
    private static readonly string SettingsPath = Path.Combine(TempPath, SettingsFileName);
    private static readonly string CredentialsPath = Path.Combine(TempPath, CredentialsFileName);
    private static readonly string MaFilePath = Path.Combine(TempPath, MaFileFileName);

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

    public static Configuration? LoadConfiguration()
    {
        if (!File.Exists(SettingsPath))
            return null;
        var configurationJson = File.ReadAllText(SettingsPath);
        return JsonConvert.DeserializeObject<Configuration>(configurationJson);
    }

    public static void SaveConfiguration(Configuration configuration)
    {
        var configurationJson = JsonConvert.SerializeObject(configuration);
        File.WriteAllText(SettingsPath, configurationJson);
    }

    public static Credentials? LoadCredentials()
    {
        if (!File.Exists(CredentialsPath))
            return null;
        var configurationJson = File.ReadAllText(CredentialsPath);
        return JsonConvert.DeserializeObject<Credentials>(configurationJson);
    }

    public static void SaveCredentials(Credentials configuration)
    {
        var configurationJson = JsonConvert.SerializeObject(configuration);
        File.WriteAllText(CredentialsPath, configurationJson);
    }

    public static void EraseCredentials()
    {
        File.Delete(CredentialsPath);
    }
}