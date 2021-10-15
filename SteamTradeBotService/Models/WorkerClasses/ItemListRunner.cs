using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SteamTradeBotService.Models.WorkerClasses
{
    public class ItemListRunner : BaseComponent
    {
        private readonly ItemAnalyzer _analyzer;
        private List<string> _itemList;

        public ItemListRunner(List<string> itemList, Browser browser)
        {
            _itemList = itemList;
            _analyzer = new ItemAnalyzer(browser);
        }

        public async Task Run(CancellationToken token)
        {
            await Task.Run(() =>
            {
                foreach (var item in _itemList)
                {
                    if (token.IsCancellationRequested) break;
                    Thread.Sleep(5000);
                    if (_analyzer.AnalyzeItem(item)) _worker.Notify(this, "buy");
                }
            });
        }

        public void SetItemsList(List<string> newItemsList)
        {
            _itemList = new List<string>(newItemsList);
        }
    }
}
