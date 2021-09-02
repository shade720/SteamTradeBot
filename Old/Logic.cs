using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using static TradeBot.Data;
using static TradeBot.GeneralBrowser;

namespace TradeBot
{
    public class Logic
    {
        private static List<Data> MainList = new List<Data>();
        private static List<string> Items = new List<string>();
        private static List<string> OldBoughtItems = new List<string>();
        private static List<string> Configuration = new List<string>();
        
        private static IProgress<string> progress;
        private static CancellationToken token;

        private static double
            PriceBuy,
            PriceSell,
            Sales,
            AvgPrice,
            Slope,
            Balance,
            EntireBalance = 0;
        private static List<double> Listing;
        private static int RecursionDeep = 0;
        private static bool NeedAnUpdate = false;


        public static void LaunchBot(IProgress<string> Progress, CancellationToken Token)
        {
            try
            {
                progress = Progress;
                token = Token;
                progress.Report("\r\nЗагружаем данные...");
                Configuration = ReadConfiguration();
                ReadList(Items, "Items");
                ReadList(OldBoughtItems, "OldBoughtItems");
                LoadData(MainList);
                progress.Report("\r\nПроверяем инвентарь...");
                ItemSell();
                progress.Report("\r\nПроверяем заказы...");
                FitPrices();
                Balance = CheckBalance();
                EntireBalance = CheckEntireBalance();
                progress.Report("balance " + Browser.FindElement(By.Id("header_wallet_balance")).Text);
                progress.Report("entire " + EntireBalance.ToString());
                progress.Report("\r\n\r\nОсновной цикл:");
                for (int i = 0; i < Items.Count && !token.IsCancellationRequested; i++)
                {
                    if (ItemАnalysis(i)) ItemBuy(i);
                    if (Balance < double.Parse(Configuration[9]) * 75.8) Waiter(false);
                    if (i == Items.Count - 1)
                    {
                        Waiter(true);
                        i = 0;
                    }
                    if (NeedAnUpdate)
                    {
                        Configuration = ReadConfiguration();
                        NeedAnUpdate = false;
                    }
                }
            }
            catch (Exception e)
            {
                progress.Report("Error " + e.Message);
                return;
            }
            progress.Report("Stopped");
        }
        private static void Waiter(bool IsEndOfList)
        {
            int WaitTime = Convert.ToInt32(double.Parse(Configuration[11])) * 20 * 1000;
            int counter = WaitTime;
            try
            {
                while (true)
                {
                    while (counter > 0 && !token.IsCancellationRequested)
                    {
                        ItemSell();
                        Balance = CheckBalance();
                        if (Balance > double.Parse(Configuration[9]) * 75.8 && !IsEndOfList) break;
                        Thread.Sleep(40000);
                        counter -= 40000;
                        if ((WaitTime - counter) % 480000 == 0)
                        {
                            FitPrices();
                            Balance = CheckBalance();
                            EntireBalance = CheckEntireBalance();
                            progress.Report("balance " + Browser.FindElement(By.Id("header_wallet_balance")).Text);
                            progress.Report("entire " + EntireBalance.ToString());
                            if (Balance > double.Parse(Configuration[9]) * 75.8 && !IsEndOfList) return;
                        }
                    }
                    if (token.IsCancellationRequested) return;
                    if (Balance < double.Parse(Configuration[9]) * 75.8 && !IsEndOfList)
                    {
                        counter = WaitTime;
                        continue;
                    }
                    FitPrices();
                    EntireBalance = CheckEntireBalance();
                    progress.Report("balance " + Browser.FindElement(By.Id("header_wallet_balance")).Text);
                    progress.Report("entire " + EntireBalance.ToString());
                    break;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private static void ItemSell()
        {
            if (!TradeBotAPI.IsSDAEnabled()) progress.Report("SDADisabled");
            if (Browser.CurrentWindowHandle == Browser.WindowHandles[0]) Browser.SwitchTo().Window(Browser.WindowHandles[1]);

            string ItemName = "";
            double FactProfit = 0;
            bool Error, WasLast;
            while (true)
            {
                WasLast = true;
                Error = false;
                Browser.Navigate().Refresh();
                try
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("iteminfo1_item_name")));
                }
                catch
                {
                    continue;
                }
                ItemName = Browser.FindElement(By.Id("iteminfo1_item_name")).Text + " (" + Browser.FindElement(By.XPath("//*[@id='iteminfo1_item_descriptors']/div[1]")).Text[(Browser.FindElement(By.XPath("//*[@id='iteminfo1_item_descriptors']/div[1]")).Text.IndexOf(' ') + 1)..] + ")";
                for (int j = 0; j < MainList.Count; j++)
                {
                    if (MainList[j].ItemNameRu == ItemName)
                    {
                        try
                        {
                            Browser.FindElement(By.XPath("//*[@class='inventory_page_right']/div[2]/div[3]/div/a")).Click();
                            Browser.FindElement(By.Id("market_sell_buyercurrency_input")).SendKeys(MainList[j].PriceSell);
                            Browser.FindElement(By.Id("market_sell_dialog_accept_ssa")).Click();
                            Browser.FindElement(By.Id("market_sell_dialog_accept")).Click();
                            Thread.Sleep(2000);
                            Browser.FindElement(By.Id("market_sell_dialog_ok")).Click();
                        }
                        catch
                        {
                            Error = true;
                            break;
                        }
                        try
                        {
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("market_sell_dialog_error")));
                            Error = true;
                            break;
                        }
                        catch { }

                        Thread.Sleep(20000);

                        WasLast = false;
                        FactProfit = Math.Round(double.Parse(MainList[j].PriceSell) - double.Parse(MainList[j].PriceSell) * 0.13 - double.Parse(MainList[j].PricePurchase), 3);

                        progress.Report("\r\nПредмет " + MainList[j].ItemNameEng + " продан." + " Профит: " + FactProfit.ToString());
                        if (OldBoughtItems.Contains(ItemName))
                        {
                            Items.Remove(MainList[j].ItemNameEng);
                            OldBoughtItems.Remove(MainList[j].ItemNameEng);
                            WriteList(Items, "Items");
                            WriteList(OldBoughtItems, "OldBoughtItems");
                        }
                        MainList.RemoveAt(j);
                        UploadData(MainList);
                        Balance = CheckBalance();
                        break;
                    }
                }
                if (WasLast && !Error) break;
            }
        }
        private static void ItemBuy(int i)
        {
            if (Browser.CurrentWindowHandle == Browser.WindowHandles[1]) Browser.SwitchTo().Window(Browser.WindowHandles[0]);
            int NumberOfItems = 1;

            while (NumberOfItems < Convert.ToInt32(double.Parse(Configuration[12])) && PriceBuy * NumberOfItems < Balance) NumberOfItems++;

            string quality, itemName = "";
            while (true)
            {
                Thread.Sleep(3000);
                Browser.Navigate().GoToUrl("https://steamcommunity.com/market/listings/730/" + Items[i]);
                Thread.Sleep(2000);
                try
                {
                    quality = Browser.FindElement(By.XPath("//*[@id='largeiteminfo_item_descriptors']/div[1]")).Text;
                    itemName = Browser.FindElement(By.Id("largeiteminfo_item_name")).Text + " (" + quality[(quality.IndexOf(' ') + 1)..] + ")";
                    Browser.FindElement(By.XPath("//*[@id='market_buyorder_info']/div/div/a")).Click();
                    Browser.FindElement(By.Id("market_buy_commodity_input_price")).SendKeys("\b\b\b\b\b\b\b\b\b\b\b" + Math.Round(PriceBuy + 0.01, 2, MidpointRounding.AwayFromZero).ToString());
                    Browser.FindElement(By.Name("input_quantity")).SendKeys("\b" + NumberOfItems.ToString());
                    Browser.FindElement(By.Id("market_buyorder_dialog_accept_ssa")).Click();
                    Browser.FindElement(By.Id("market_buyorder_dialog_purchase")).Click();
                    RecursionDeep = 0;
                    break;
                }
                catch (Exception e)
                {
                    if (token.IsCancellationRequested) return;
                    if (RecursionDeep > 20)
                        throw e;
                    RecursionDeep++;
                    Thread.Sleep(10000);
                }
            }
            for (int j = 1; j <= NumberOfItems; j++)
                MainList.Add(new Data()
                {
                    ItemNameEng = Items[i],
                    ItemNameRu = itemName,
                    PricePurchase = PriceBuy.ToString(),
                    PriceSell = PriceSell.ToString()
                });
            UploadData(MainList);
            Balance = CheckBalance();
            progress.Report("\r\nПредмет " + Items[i] + "(x" + NumberOfItems.ToString() + ") куплен. ");
        }
        private static void FitPrices()
        {
            if (Browser.CurrentWindowHandle == Browser.WindowHandles[1]) Browser.SwitchTo().Window(Browser.WindowHandles[0]);
            string NewPriceStr = "";
            double NewPrice = 0;
            double OldPrice = 0;

            for (int k = 0; k < MainList.Count && k >= 0; k++)
            {
                if (k > 0)
                    if (MainList[k].ItemNameEng == MainList[k - 1].ItemNameEng) continue;
                Browser.Navigate().GoToUrl("https://steamcommunity.com/market/listings/730/" + MainList[k].ItemNameEng);
                Thread.Sleep(2000);
                try
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='market_commodity_buyrequests']/span[2]")));
                    NewPriceStr = Browser.FindElement(By.XPath("//*[@id='market_commodity_buyrequests']/span[2]")).Text;
                    NewPrice = double.Parse(NewPriceStr.Substring(0, NewPriceStr.IndexOf(" ")));
                }
                catch
                {
                    k--;
                    continue;
                }

                OldPrice = double.Parse(MainList[k].PricePurchase);

                if (OldPrice - NewPrice <= -double.Parse(Configuration[6]))
                {
                    string itemNameEng = MainList[k].ItemNameEng;
                    try
                    {
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='tabContentsMyListings']/div/div[2]/div[5]/div/a/span[2]")));
                        Browser.FindElement(By.XPath("//*[@id='tabContentsMyListings']/div/div[2]/div[5]/div/a/span[2]")).Click();
                    }
                    catch
                    {
                        continue;
                    }
                    progress.Report("\r\nПредмет " + MainList[k].ItemNameEng + " снят с покупки ");
                    if (OldBoughtItems.Contains(MainList[k].ItemNameRu))
                    {
                        Items.RemoveAll(x => x == itemNameEng);
                        OldBoughtItems.RemoveAll(x => x == itemNameEng);
                        WriteList(Items, "Items");
                        WriteList(OldBoughtItems, "OldBoughtItems");
                    }

                    MainList.RemoveAll(x => x.ItemNameEng == itemNameEng);
                    UploadData(MainList);
                    Balance = CheckBalance();
                    if (MainList.Count == 0) return;
                    k--;
                }
            }
        }
        private static bool ItemАnalysis(int i)
        {
            if (IsAlreadyThere(i)) return false;

            if (Browser.CurrentWindowHandle == Browser.WindowHandles[1]) Browser.SwitchTo().Window(Browser.WindowHandles[0]);
            while (true)
            {
                try
                {
                    Browser.Navigate().GoToUrl("https://steamcommunity.com/market/listings/730/" + Items[i]);
                    Thread.Sleep(5000);

                    if (Configuration[8].Equals("Неделя")) GraphAnalyzer(7);
                    if (Configuration[8].Equals("Месяц")) GraphAnalyzer(30);

                    if (Slope < double.Parse(Configuration[13]))
                    {
                        progress.Report("\r\nПредмет " + Items[i] + " отсеян по тренду");
                        return false;
                    }

                    if (Sales < double.Parse(Configuration[0]))
                    {
                        progress.Report("\r\nПредмет " + Items[i] + " отсеян по количеству продаж");
                        return false;
                    }

                    GetPrices();

                    if (Listing == null)
                    {
                        progress.Report("\r\nПредмет " + Items[i] + " отсеян по сломанному листингу");
                        return false;
                    }

                    if (AvgPrice * (2 - double.Parse(Configuration[7])) <= PriceBuy)
                    {
                        progress.Report("\r\nПредмет " + Items[i] + " отсеян по средней цене");
                        return false;
                    }
                    if (!IsPricesRight())
                    {
                        progress.Report("\r\nПредмет " + Items[i] + " отсеян по цене");
                        return false;
                    }
                    RecursionDeep = 0;
                    break;
                }
                catch (Exception e)
                {
                    if (token.IsCancellationRequested) break;
                    if (RecursionDeep > 20)
                        throw e;
                    RecursionDeep++;
                    Thread.Sleep(10000);
                }
            }
            return true;
        }
        private static void GraphAnalyzer(int Period)
        {
            string htmlCode = Browser.PageSource;
            string line = htmlCode[htmlCode.IndexOf("[[")..(htmlCode.IndexOf("]]") + 2)];
            string[] blocks = line.Split('"');
            DateTime now = DateTime.Now;
            string[] EndTimes = new string[24];
            for (int i = 0; i < 23; i++) EndTimes[i] = now.AddDays(-Period).AddHours(-i).ToString("MMM dd yyyy HH: +0", CultureInfo.GetCultureInfo("en-US"));

            Sales = SalesPerDay(blocks, Period, EndTimes);

            AvgPrice = AveragePrice(blocks, EndTimes);

            Slope = TrendOfPrice(blocks, EndTimes);
        }
        private static double AveragePrice(string[] Blocks, string[] EndTimes)
        {
            int i, j;
            double
                Summand,
                Sum = 0;
            double temp = double.Parse(Blocks[^3].Replace(",", "").Replace(".", ","));
            for (i = Blocks.Length - 3; ; i -= 4)
            {
                Summand = double.Parse(Blocks[i].Replace(",", "").Replace(".", ","));
                if (Summand < temp * 2 && Summand > temp / 2)
                {
                    Sum += Summand;
                    temp = Summand;
                }
                for (j = 0; j < 23; j++)
                    if (EndTimes[j] == Blocks[i - 1])
                        break;
                if (j < 23)
                    break;
            }
            return Sum / ((Blocks.Length - 3 - i) / 4);
        }
        private static double SalesPerDay(string[] Blocks, int Period, string[] EndTimes)
        {
            int i, j;
            double Sum = 0;
            for (i = Blocks.Length - 2; ; i -= 4)
            {
                Sum += int.Parse(Blocks[i]);
                for (j = 0; j < 23; j++)
                    if (EndTimes[j] == Blocks[i - 2]) break;
                if (j < 23)
                    break;
            }
            return Sum / Period;
        }
        private static double TrendOfPrice(string[] Blocks, string[] EndTimes)
        {
            int i, j;
            var ValuesByY = new List<double>();
            var ValuesByX = new List<int>();
            double
              Summand,
              AvgX,
              AvgY,
              dividend = 0,
              divisor = 0;

            //m = ∑ (x - AVG(x)(y - AVG(y)) / ∑ (x - AVG(x))²

            double temp = double.Parse(Blocks[^3].Replace(",", "").Replace(".", ","));
            for (i = Blocks.Length - 3; ; i -= 4)
            {
                Summand = double.Parse(Blocks[i].Replace(",", "").Replace(".", ","));
                if (Summand < temp * 2 && Summand > temp / 2)
                {
                    ValuesByY.Add(Summand);
                    temp = Summand;
                }
                for (j = 0; j < 23; j++)
                    if (EndTimes[j] == Blocks[i - 1])
                        break;
                if (j < 23)
                    break;
            }
            for (j = ValuesByY.Count; j > 0; j--) ValuesByX.Add(j);

            AvgX = ValuesByX.Average();
            AvgY = ValuesByY.Average();


            for (i = 0; i < ValuesByX.Count; i++) dividend += (ValuesByX[i] - AvgX) * (ValuesByY[i] - AvgY);
            for (i = 0; i < ValuesByX.Count; i++) divisor += Math.Pow(ValuesByX[i] - AvgX, 2);

            return dividend / divisor;
        }
        private static double ParsePrice(string PriceText)
        {
            try 
            {
                return double.Parse(PriceText.Substring(0, PriceText.IndexOf(" ")));
            } 
            catch 
            {
                return 0;
            }
        }
        private static List<double> GetSortedListing() 
        {
            List<double> rawListing = new List<double>();
            List<double> whiteListing;

            double ConstPrice;
            int CountOfEqual = 0, 
                NumberOfSold, 
                temp = 0, 
                PositionOnListing;

            while (true)
            {
                try
                {
                    rawListing.Add(ParsePrice(Browser.FindElement(By.XPath($"//*[@id='searchResultsRows']/div[2]/div[2]/div[2]/span[1]/span[1]")).Text));
                    for (NumberOfSold = 0; rawListing[NumberOfSold] == 0; NumberOfSold++)
                        rawListing.Add(ParsePrice(Browser.FindElement(By.XPath($"//*[@id='searchResultsRows']/div[{NumberOfSold + 3}]/div[2]/div[2]/span[1]/span[1]")).Text));

                    ConstPrice = rawListing[NumberOfSold];

                    for (int PageNumber = 1; PageNumber <= 3; PageNumber++)
                    {
                        if (PageNumber > 1)
                        {
                            while (true)
                            {
                                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"//*[@id='searchResults_links']/span[{PageNumber}]")));
                                Browser.FindElement(By.XPath($"//*[@id='searchResults_links']/span[{PageNumber}]")).Click();
                                Thread.Sleep(5000);
                                if (int.Parse(Browser.FindElement(By.CssSelector("#searchResults_links > span.market_paging_pagelink.active")).Text) == PageNumber)
                                {
                                    RecursionDeep = 0;
                                    break;
                                }
                                else
                                {
                                    if (RecursionDeep > 5)
                                    {
                                        RecursionDeep = 0;
                                        return null;
                                    }
                                    RecursionDeep++;
                                }
                            }
                        }
                        for (PositionOnListing = 0; PositionOnListing <= 8; PositionOnListing++)
                        {
                            rawListing.Add(ParsePrice(Browser.FindElement(By.XPath($"//*[@id='searchResultsRows']/div[{PositionOnListing + 3}]/div[2]/div[2]/span[1]/span[1]")).Text));
                            if (rawListing[PositionOnListing + temp + 1] == 0) continue;
                            if (rawListing[PositionOnListing + temp + 1] == ConstPrice) CountOfEqual++;
                            else
                            {
                                CountOfEqual = 0;
                                ConstPrice = rawListing[PositionOnListing + temp + 1];
                            }
                            if (CountOfEqual == 15) return null;
                        }
                        temp += PositionOnListing;
                    }
                    Browser.FindElement(By.XPath($"//*[@id='searchResults_links']/span[1]")).Click();
                    break;
                }
                catch (Exception e)
                {
                    if (RecursionDeep > 10)
                        throw e;
                    RecursionDeep++;
                    Thread.Sleep(10000);
                    Browser.Navigate().Refresh();
                }
            }
            rawListing.RemoveAll(x => x == 0);
            rawListing.Sort();
            whiteListing = rawListing.Distinct().ToList();

            return whiteListing;
        }
        private static void GetPrices()
        {
            string PriceBuyStr = "";
            while (true)
            {
                try
                {
                    Listing = GetSortedListing();
                    if (Listing == null) return;

                    Browser.FindElement(By.CssSelector("#market_buyorder_info_show_details > span")).Click();
                    double Sum = 0;
                    for (int j = 2; j <= 7; j++)
                    {
                        Sum += double.Parse(Browser.FindElement(By.CssSelector($"#market_commodity_buyreqeusts_table > table > tbody > tr:nth-child({j}) > td:nth-child(2)")).Text);
                        if (Sum > Sales * double.Parse(Configuration[2]))
                        {
                            PriceBuyStr = Browser.FindElement(By.CssSelector($"#market_commodity_buyreqeusts_table > table > tbody > tr:nth-child({j}) > td:nth-child(1)")).Text;
                            break;
                        }
                    }
                    PriceBuy = ParsePrice(PriceBuyStr) + 0.01;

                    RecursionDeep = 0;
                    break;
                }
                catch (Exception e)
                {
                    if (RecursionDeep > 20)
                        throw e;
                    RecursionDeep++;
                    Thread.Sleep(10000);
                    Browser.Navigate().Refresh();
                }
            }
        }
        private static bool IsAlreadyThere(int i)
        {
            int j;
            for (j = 0; j < MainList.Count; j++)
                if (Items[i] == MainList[j].ItemNameEng) return true;
            return false;
        }
        private static bool IsPricesRight()
        {
            bool IsPricesRight = false;
            for (int Position = 0; Position < double.Parse(Configuration[1]) && !IsPricesRight; Position++)
            {
                PriceSell = Listing[Position] - 0.01;
                if (PriceBuy < (PriceSell - PriceSell * 0.13 - double.Parse(Configuration[3])) && Balance >= PriceBuy) IsPricesRight = true;
                else IsPricesRight = false;
            }
            return IsPricesRight;
        }
        private static double CheckBalance()
        {
            while (true)
            {
                try
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("header_wallet_balance")));
                    RecursionDeep = 0;
                    break;
                }
                catch (Exception e)
                {
                    if (RecursionDeep > 10)
                        throw e;
                    Browser.Navigate().Refresh();
                    RecursionDeep++;
                }
            }
            string balanceStr = Browser.FindElement(By.Id("header_wallet_balance")).Text;
            double bal = double.Parse(balanceStr.Substring(0, balanceStr.IndexOf(" "))) * double.Parse(Configuration[5]);
            for (int i = 0; i < MainList.Count; i++) bal -= double.Parse(MainList[i].PricePurchase);
            return bal;
        }
        public static double CheckEntireBalance()
        {
            string NotParsedPrice;

            if (Browser.CurrentWindowHandle == Browser.WindowHandles[1]) Browser.SwitchTo().Window(Browser.WindowHandles[0]);
            while (true)
            {
                Browser.Navigate().GoToUrl("https://steamcommunity.com/market/");
                Thread.Sleep(2000);
                try
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='my_market_selllistings_number']")));
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("header_wallet_balance")));
                    RecursionDeep = 0;
                    break;
                }
                catch (Exception e)
                {
                    if (RecursionDeep > 10)
                        throw e;
                    RecursionDeep++;
                }
            }
            double Sum = Math.Round(double.Parse(Browser.FindElement(By.Id("header_wallet_balance")).Text.Substring(0, Browser.FindElement(By.Id("header_wallet_balance")).Text.IndexOf(" "))), 3);
            int NumberOfItems = int.Parse(Browser.FindElement(By.XPath("//*[@id='my_market_selllistings_number']")).Text);

            try
            {
                for (int i = 1; i <= NumberOfItems; i++)
                {
                    NotParsedPrice = Browser.FindElement(By.XPath($"//*[@id='tabContentsMyActiveMarketListingsRows']/div[{i}]/div[2]/span/span/span/span[2]")).Text;
                    NotParsedPrice = NotParsedPrice.Substring(1, NotParsedPrice.IndexOf(" "));
                    Sum += double.Parse(NotParsedPrice);
                    if (i % 10 == 0)
                    {
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id($"tabContentsMyActiveMarketListings_btn_next")));
                        Browser.FindElement(By.Id($"tabContentsMyActiveMarketListings_btn_next")).Click();
                        NumberOfItems -= i;
                        Thread.Sleep(3000);
                        i = 0;
                    }
                }
            }
            catch { }
            Browser.Navigate().Back();
            return Math.Round(Sum, 3);
        }
        public static void LoadItemList(IProgress<string> progress)
        {
            if (Browser.CurrentWindowHandle == Browser.WindowHandles[1]) Browser.SwitchTo().Window(Browser.WindowHandles[0]);
            progress.Report("\r\nСоставляется список предметов...");
            Configuration = ReadConfiguration();
            LoadData(MainList);

            Items.Clear();
            OldBoughtItems.Clear();

            for (int i = 0; i < MainList.Count; i++)
            {
                Items.Add(MainList[i].ItemNameEng);
                OldBoughtItems.Add(MainList[i].ItemNameRu);
            }
            progress.Report("");
            Browser.Navigate().GoToUrl("https://skins-table.xyz/");
            try
            {
                Browser.FindElement(By.XPath("/html/body/div[1]/div[4]/div/div/center/div/div/div[2]/form/div[2]/a")).Click();
                Browser.FindElement(By.Id("imageLogin")).Click();
            }
            catch { }
            try
            {
                progress.Report("");
                Browser.Navigate().GoToUrl("https://skins-table.xyz/table/");
                Browser.FindElement(By.CssSelector("#scroll > div > div.sites.first > div:nth-child(30)")).Click();
                Browser.FindElement(By.CssSelector("#scroll > div > div.sites.second > div:nth-child(29)")).Click();
                Browser.FindElement(By.Id("price1_from")).SendKeys("\b\b\b\b\b\b\b" + Configuration[9]);
                Browser.FindElement(By.Id("price1_to")).SendKeys("\b\b\b\b\b\b\b" + Configuration[10]);
                Browser.FindElement(By.Id("price2_from")).SendKeys("\b\b\b\b\b\b\b" + Configuration[9]);
                Browser.FindElement(By.Id("price2_to")).SendKeys("\b\b\b\b\b\b\b" + Configuration[10]);
                Browser.FindElement(By.Id("sc1")).SendKeys("\b\b\b\b" + double.Parse(Configuration[0]) * 7);
                Browser.FindElement(By.Id("change1")).Click();
                progress.Report("");
            }
            catch (Exception e)
            {
                progress.Report("Error " + e.Message);
                return;
            }

            IJavaScriptExecutor js = Browser as IJavaScriptExecutor;
            Thread.Sleep(15000);
            for (int i = 1; i < 10; i++)
            {
                js.ExecuteScript("scroll(0, 20000000);");
            }
            string htmlCode = Browser.PageSource;
            Regex regex = new Regex(@"data-clipboard-text=\p{P}[^\p{P}]*\p{P}?[^\p{P}]*\p{P}?[^\p{P}]*\p{P}?[^\p{P}]*\p{P}[^\p{P}]*\p{P}?[^\p{P}]*\p{P}\p{P}");
            MatchCollection matches = regex.Matches(htmlCode);
            progress.Report("");
            if (matches.Count > 0)
            {
                string line, line_end;
                int i = 0;
                while (true)
                {
                    line = matches[i].ToString();
                    line_end = line[(line.IndexOf("\"") + 1)..line.LastIndexOf("\"")];
                    if (!line_end.Contains("Sealed Graffiti") &&
                        !line_end.Contains("Sticker") &&
                        !line_end.Contains("Case") &&
                        !MainList.Exists(x => x.ItemNameEng == line_end)
                        ) Items.Add(line_end);
                    i++;
                    if (Items.Count == (double.Parse(Configuration[11]) + OldBoughtItems.Count) || i == matches.Count)
                        break;
                }
            }
            WriteList(Items, "Items");
            WriteList(OldBoughtItems, "OldBoughtItems");
            progress.Report("");
            Browser.Navigate().GoToUrl("https://store.steampowered.com/");
            progress.Report("\r\nСоставлен список: " + (Items.Count - OldBoughtItems.Count).ToString() + " из " + Convert.ToInt32(double.Parse(Configuration[11])).ToString() + " предметов!");
        }
        public static void ClearBuyLots(IProgress<string> progress)
        {
            if (Browser.CurrentWindowHandle == Browser.WindowHandles[1]) Browser.SwitchTo().Window(Browser.WindowHandles[0]);
            while (true)
            {
                try
                {
                    Browser.Navigate().GoToUrl("https://steamcommunity.com/market/");
                    Thread.Sleep(3000);
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='my_market_buylistings_number']")));
                    int Count = int.Parse(Browser.FindElement(By.XPath("//*[@id='my_market_buylistings_number']")).Text);
                    for (int k = 2; k <= Count + 1; k++)
                    {
                        Browser.FindElement(By.XPath($"//*[@id='tabContentsMyListings']/div[2]/div[2]/div[5]/div/a")).Click();
                        Thread.Sleep(2000);
                    }
                    RecursionDeep = 0;
                    break;
                }
                catch (Exception e)
                {
                    Thread.Sleep(10000);
                    if (RecursionDeep > 10)
                    {
                        progress.Report("Error " + e.Message);
                        return;
                    }
                    RecursionDeep++;
                    continue;
                }
            }
            File.Delete(@"Data\MainList.txt");
            File.Create(@"Data\MainList.txt").Close();
            Browser.Navigate().Back();
        }
        public static void UpdateConfiguration()
        {
            NeedAnUpdate = true;
        }
    }
}