using OpenQA.Selenium;
using Serilog;
using SteamTradeBot.Backend.Domain.Abstractions;

namespace SteamTradeBot.Backend.Infrastructure.SteamConnectors.Selenium;

internal class SkinsTableApi : IItemsTableApi, IDisposable
{
    private readonly SeleniumWebDriver _webDriver;

    private const int MaxPageRefreshPeriodSeconds = 10000;
    private const int MinPageRefreshPeriodSeconds = 5;
    private const int ItemsNamesPerScreen = 20;
    private const char CorrectSortingSign = '↓';

    public SkinsTableApi(SeleniumWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    public async Task<List<string>> GetItemNamesListAsync(double fromPrice, double toPrice, double salesVolumeByWeek, int listSize)
    {
        return await _webDriver.SafeConnect(() =>
        {
            var result = new List<string>();
            _webDriver.SetPage("https://skins-table.xyz/table/");
            try
            {
                Log.Logger.Information("Check if we are already logged in...");
                //_webDriver.ClickOnElement(By.XPath("/html/body/div[1]/div[4]/div/div/center/div/div/div[2]/form/div[2]/a"), true);
                _webDriver.ClickOnElement(By.Id("imageLogin"));
                Log.Logger.Information("Logged in successfully on skins-table.xyz");
            }
            catch
            {
                Log.Logger.Information("Already logged in on skins-table.xyz");
            }

            _webDriver.SetPage("https://skins-table.xyz/table/");

            // Выставляем большой период обновления страницы, чтобы успеть скачать нужное количество предметов до того
            // как страница обновится.
            _webDriver.Clear(By.Id("refresh"));
            _webDriver.WriteToElement(By.Id("refresh"), MinPageRefreshPeriodSeconds.ToString());

            _webDriver.ClickOnElement(By.CssSelector("#scroll > div > div.sites.first > div:nth-child(30)"));
            _webDriver.ClickOnElement(By.CssSelector("#scroll > div > div.sites.second > div:nth-child(29)"));

            _webDriver.Clear(By.Id("price1_from"));
            _webDriver.SendKey(By.Id("price1_from"), fromPrice.ToString());

            _webDriver.Clear(By.Id("price1_to"));
            _webDriver.SendKey(By.Id("price1_to"), toPrice.ToString());

            _webDriver.Clear(By.Id("price2_from"));
            _webDriver.SendKey(By.Id("price2_from"), fromPrice.ToString());

            _webDriver.Clear(By.Id("price2_to"));
            _webDriver.SendKey(By.Id("price2_to"), toPrice.ToString());

            _webDriver.Clear(By.Id("sc1"));
            _webDriver.SendKey(By.Id("sc1"), salesVolumeByWeek.ToString());
            // Нужна проверка, правильно ли установлена сортировка.
            var sortingStateStr = _webDriver.ReadFromElement(By.Id("change1"));
            if (!sortingStateStr.Contains(CorrectSortingSign))
                _webDriver.ClickOnElement(By.Id("change1"));

            Thread.Sleep(MinPageRefreshPeriodSeconds * 1000);
            _webDriver.Clear(By.Id("refresh"));
            _webDriver.WriteToElement(By.Id("refresh"), MaxPageRefreshPeriodSeconds.ToString());

            _webDriver.ExecuteJs("scroll(0, 500);");

            for (var itemsTableIndex = 1; itemsTableIndex <= listSize; itemsTableIndex++)
            {
                var itemName = _webDriver.ReadFromElement(By.XPath($"//*[@id='data-table']/tbody/tr[{itemsTableIndex}]/td"));
                if (itemName.Contains("Sealed Graffiti") || itemName.Contains("Sticker") || itemName.Contains("Case"))
                    continue;
                Log.Logger.Information("Got {0} item name", 
                    itemName);
                result.Add(itemName);

                if (itemsTableIndex % ItemsNamesPerScreen == 0)
                    _webDriver.ExecuteJs("scroll(0, 1350);");
            }

            return result;
        });
    }

    public void Dispose()
    {
        _webDriver.Dispose();
    }
}