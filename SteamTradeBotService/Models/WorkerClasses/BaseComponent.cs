namespace SteamTradeBotService.Models.WorkerClasses
{
    public class BaseComponent
    {
        protected IWorker _worker;

        protected BaseComponent(IWorker worker = null)
        {
            _worker = worker;
        }

        public void SetCore(IWorker core)
        {
            _worker = core;
        }
    }
}
