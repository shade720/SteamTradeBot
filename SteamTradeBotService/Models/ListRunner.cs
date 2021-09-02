using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SteamTradeBotService.Models
{
    public class ListRunner : Base
    {
        private readonly Analyzer _analyzer;
        private List<string> _itemList;
        private readonly CancellationToken _token;

        public ListRunner(List<string> itemList, CancellationToken token)
        {
            _itemList = itemList;
            _analyzer = new Analyzer(_browser);
            _token = token;
        }

        public async Task Run()
        {
            await Task.Run(() =>
            {
                foreach (var item in _itemList)
                {
                    if (_analyzer.AnalyzeItem(item)) _core.Notify(this, "buy");
                    if (_token.IsCancellationRequested) break;
                }
            });
        }

        public void SetItemsList(List<string> newItemsList)
        {
            _itemList = new List<string>(newItemsList);
        }
    }
}
