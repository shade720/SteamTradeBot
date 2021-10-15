namespace SteamTradeBotService.Models
{
    public interface IWorker
    {
        void Notify(object sender, string ev);
    }
}
