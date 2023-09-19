using SteamTradeBot.Backend.Models.ItemModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.Models.Abstractions;

public interface ISteamApi
{
    public Task<bool> LogIn(string login, string password, string secret);
    public void LogOut();
    public Task<string> GetItemUrlAsync(string itemName);
    public Task<string> GetRusItemNameAsync(string itemUrl);
    public Task<List<OrderBookItem>> GetBuyOrdersBookAsync(string itemUrl, int buyListingFindRange);
    public Task<List<OrderBookItem>> GetSellOrdersBookAsync(string itemUrl, int sellListingFindRange);
    public Task<int?> GetBuyOrderQuantityAsync(string itemUrl);
    public Task<double?> GetBuyOrderPriceAsync(string itemUrl);
    public Task<Chart> GetChartAsync(string itemUrl, DateTime fromDate);
    public Task<double?> GetSellOrderPriceAsync(string itemUrl);
    public Task<bool> PlaceBuyOrderAsync(string itemUrl, double price, int quantity);
    public Task<bool> PlaceSellOrderAsync(string itemName, double price, string userId, int inventoryFindRange = 10);
    public Task<bool> CancelBuyOrderAsync(string itemUrl);
    public Task<bool> CancelSellOrderAsync(string itemUrl);
    public Task<double> GetBalanceAsync();
}