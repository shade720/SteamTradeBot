using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SteamTradeBotService.Models
{
    public class ListRunner : Base
    {
        private readonly Analyzer _analyzer;
        private List<string> _itemList;

        public ListRunner(List<string> itemList, Browser browser)
        {
            _itemList = itemList;
            _analyzer = new Analyzer(browser);
        }

        public async Task Run(CancellationToken token)
        {
            await Task.Run(() =>
            {
                foreach (var item in _itemList)
                {
                    if (_analyzer.AnalyzeItem(item)) _core.Notify(this, "buy");
                }
            }, token);
        }

        public void SetItemsList(List<string> newItemsList)
        {
            _itemList = new List<string>(newItemsList);
        }
    }
}
