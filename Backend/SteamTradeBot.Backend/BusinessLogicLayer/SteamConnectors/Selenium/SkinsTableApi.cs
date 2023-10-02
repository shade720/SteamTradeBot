using OpenQA.Selenium;
using Serilog;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.SteamConnectors.Selenium;

public class SkinsTableApi : IItemsTableApi, IDisposable
{
    private readonly SeleniumWebDriver _webDriver;

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

            _webDriver.ClickOnElement(By.CssSelector("#scroll > div > div.sites.first > div:nth-child(30)"));
            _webDriver.ClickOnElement(By.CssSelector("#scroll > div > div.sites.second > div:nth-child(29)"));
            _webDriver.SendKey(By.Id("price1_from"), $"\b\b\b\b\b\b\b{fromPrice}");
            _webDriver.SendKey(By.Id("price1_to"), $"\b\b\b\b\b\b\b{toPrice}");
            _webDriver.SendKey(By.Id("price2_from"), $"\b\b\b\b\b\b\b{fromPrice}");
            _webDriver.SendKey(By.Id("price2_to"), $"\b\b\b\b\b\b\b{toPrice}");
            _webDriver.SendKey(By.Id("sc1"), $"\b\b\b\b{salesVolumeByWeek}");
            _webDriver.ClickOnElement(By.Id("change1"));

            Thread.Sleep(15000);
            for (var i = 1; i < 10; i++)
            {
                _webDriver.ExecuteJs("scroll(0, 20000000);");
            }

            var htmlCode = _webDriver.GetPageSource();
            var regex = new Regex(@"data-clipboard-text=\p{P}[^\p{P}]*\p{P}?[^\p{P}]*\p{P}?[^\p{P}]*\p{P}?[^\p{P}]*\p{P}[^\p{P}]*\p{P}?[^\p{P}]*\p{P}\p{P}");
            var matches = regex.Matches(htmlCode);

            if (matches.Count <= 0)
                return new List<string>();

            for (var i = 0; i < listSize; i++)
            {
                var itemName = string.Join("", matches[i].ToString().SkipWhile(x => x != '=').Skip(2).SkipLast(1));
                if (itemName.Contains("Sealed Graffiti") || itemName.Contains("Sticker") || itemName.Contains("Case"))
                    continue;
                Log.Logger.Information("Got {0} item name", itemName);
                result.Add(itemName);
            }
            return result;
        });
    }

    public void Dispose()
    {
        _webDriver.Dispose();
    }
}