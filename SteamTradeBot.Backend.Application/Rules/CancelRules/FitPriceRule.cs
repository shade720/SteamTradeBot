using Serilog;
using SteamTradeBot.Backend.Domain.Abstractions;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.Abstractions.Rules;
using SteamTradeBot.Backend.Domain.ItemModel;

namespace SteamTradeBot.Backend.Application.Rules.CancelRules;

public sealed class FitPriceRule : ICancelRule
{
    private readonly OrdersRepository _ordersRepository;
    private readonly IConfigurationService _configurationService;

    public FitPriceRule(IConfigurationService configurationService, OrdersRepository ordersRepository)
    {
        _configurationService = configurationService;
        _ordersRepository = ordersRepository;
    }

    public bool IsFollowed(ItemPage itemPage)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsFollowedAsync(ItemPage itemPage)
    {
        if (itemPage.MyBuyOrder is null)
        {
            Log.Information("Can't check if order obsolete. Buy order does not exist.");
            return false;
        }
        if (itemPage.BuyOrderBook.Count <= 0)
        {
            Log.Information("Can't check if order obsolete. Buy order book is empty.");
            return false;
        }
        var existedBuyOrder = await _ordersRepository.GetOrderAsync(itemPage.EngItemName, _configurationService.ApiKey, OrderType.BuyOrder);

        var isOrderObsolete = existedBuyOrder is not null && 
                              itemPage.BuyOrderBook.All(x => Math.Abs(x.Price - existedBuyOrder.BuyPrice) > _configurationService.FitPriceRange);
        if (!isOrderObsolete)
        {
            Log.Information("Order is still relevant.");
            return false;
        }
        Log.Information("Order is obsolete. Cancelling...");
        return true;
    }
}