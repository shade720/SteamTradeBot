using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Threading.Tasks;
using System.Threading;
using System;
using Serilog;

namespace SteamTradeBot.Backend.BusinessLogicLayer.SteamConnectors.Selenium;

public class SeleniumWebDriver : IDisposable
{
    private readonly IWebDriver _chromeBrowser;

    private const int DefaultImplicitWaitTime = 5;
    private const int RequestDelayMs = 5000;
    private const int RetryWaitTimeMs = 2000;
    private const int RetriesCount = 5;

    public SeleniumWebDriver(Func<IWebDriver> webDriver)
    {
        Log.Logger.Information("Creating selenium steam API...");
        _chromeBrowser = webDriver.Invoke();
        Log.Logger.Information("Selenium steam API created!");
    }

    public bool IsVisible(By by, bool explicitly = false, int waitTime = 5)
    {
        return GetElementWithWait(by, explicitly, waitTime).Displayed;
    }

    public void WriteToElement(By by, string message, bool explicitly = false, int waitTime = 5)
    {
        GetElementWithWait(by, explicitly, waitTime).SendKeys(message);
    }

    public string ReadFromElement(By by, bool explicitly = false, int waitTime = 5)
    {
        return GetElementWithWait(by, explicitly, waitTime).Text;
    }

    public void ClickOnElement(By by, bool explicitly = false, int waitTime = 5)
    {
        GetElementWithWait(by, explicitly, waitTime).Click();
    }

    public void SendKey(By by, string key, bool explicitly = false, int waitTime = 5)
    {
        GetElementWithWait(by, explicitly, waitTime).SendKeys(key);
    }

    public void SetCheckBox(By by, bool checkBoxValue, bool explicitly = false, int waitTime = 5)
    {
        var element = GetElementWithWait(by, explicitly, waitTime);
        if (element.Selected != checkBoxValue)
           element.Click();
    }

    public void Clear(By by, bool explicitly = false, int waitTime = 5)
    {
        GetElementWithWait(by, explicitly, waitTime).Clear();
    }

    public string GetPageSource()
    {
        return _chromeBrowser.PageSource;
    }

    public string GetCurrentPageUrl()
    {
        return _chromeBrowser.Url;
    }

    public void SetPage(string url)
    {
        if (_chromeBrowser.Url != url)
            _chromeBrowser.Navigate().GoToUrl(url);
    }

    public void ExecuteJs(string script)
    {
        ((IJavaScriptExecutor)_chromeBrowser).ExecuteScript(script);
    }

    public async Task<T> SafeConnect<T>(Func<T> unsafeFunc, bool isDelayNeeded = false)
    {
        var retryWaitTimeMs = isDelayNeeded ? RequestDelayMs : 0;
        for (var attempt = 0; attempt < RetriesCount; attempt++)
        {
            try
            {
                Thread.Sleep(retryWaitTimeMs);
                return await Task.Run(unsafeFunc);
            }
            catch (Exception e)
            {
                Log.Error("Connection error! Attempt: {0}/{1}\r\nMessage: {2}\r\nStack trace: {3}", attempt + 1, RetriesCount, e.Message, e.StackTrace);
                retryWaitTimeMs = RetryWaitTimeMs;
                _chromeBrowser.Navigate().Refresh();
                if (attempt == RetriesCount - 1)
                    throw;
            }
        }
        return default;
    }

    public void Dispose()
    {
        Log.Logger.Information("Disposing web browser...");
        _chromeBrowser.Quit();
        _chromeBrowser.Dispose();
        Log.Logger.Information("Web browser disposed!");
    }

    private IWebElement ExplicitWait(By by, int waitTime)
    {
        _chromeBrowser.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
        return new WebDriverWait(_chromeBrowser, TimeSpan.FromSeconds(waitTime)).Until(ExpectedConditions.ElementIsVisible(by));
    }

    private IWebElement ImplicitWait(By by, int waitTime)
    {
        _chromeBrowser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(waitTime);
        return _chromeBrowser.FindElement(by);
    }

    private IWebElement GetElementWithWait(By by, bool explicitly = false, int waitTime = 5)
    {
        return explicitly ? ExplicitWait(by, waitTime) : ImplicitWait(by, DefaultImplicitWaitTime);
    }
}