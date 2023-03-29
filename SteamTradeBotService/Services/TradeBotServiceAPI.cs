using System.Threading.Tasks;
using Grpc.Core;
using Serilog;
using SteamTradeBotService.BusinessLogicLayer;
using TradeBotService;

namespace SteamTradeBotService.Services;

public class TradeBotServiceAPI : SteamTradeBot.SteamTradeBotBase
{
    private readonly TradeBot _tradeBot;

    public TradeBotServiceAPI(TradeBot tradeBot)
    {
        _tradeBot = tradeBot;
    }

	public override async Task<StartResponse> StartBot(StartRequest request, ServerCallContext context)
    {
        _tradeBot.StartTrading();
        return new StartResponse();
	}

	public override async Task<StopResponse> StopBot(StopRequest request, ServerCallContext context)
	{
        _tradeBot.StopTrading();
        return new StopResponse();
	}

	public override async Task<LoadItemListResponse> LoadItemList(LoadItemListRequest request, ServerCallContext context)
    {
        _tradeBot.RefreshItemsList();
		return new LoadItemListResponse();
    }

	public override async Task<ClearMyLotsResponse> ClearMyLots(ClearMyLotsRequest request, ServerCallContext context)
    {
        _tradeBot.ClearBuyOrders();
		return new ClearMyLotsResponse();
	}
        
	public override async Task<SetConfigurationResponse> SetConfiguration(SetConfigurationRequest request, ServerCallContext context)
    {
        if (_tradeBot.SetConfiguration(request.ConfigurationJson))
            Log.Information("Configuration successfully updated!");
        else
            Log.Error("Configuration not updated due error!");
        return new SetConfigurationResponse();
	}

	public override async Task<LogInResponse> LogIn(LogInRequest request, ServerCallContext context)
    {
        _tradeBot.LogIn(request.Login, request.Password, request.Token);
		return new LogInResponse();
	}

	public override async Task<LogOutResponse> LogOut(LogOutRequest request, ServerCallContext context)
	{
		_tradeBot.LogOut();
        return new LogOutResponse();
    }

	public override async Task<WriteReport> Report(SubscribeReports request, IServerStreamWriter<WriteReport> responseStream, ServerCallContext context)
	{
		var task = new TaskCompletionSource<WriteReport>();
		//void Write<T>(T param)
		//{
		//	responseStream.WriteAsync(double.TryParse(param.ToString(), out var balance)
		//		? new WriteReport { Balance = balance }
		//		: new WriteReport { Message = param.ToString() });
		//}

		//try
		//{
		//	context.CancellationToken.Register(() => task.SetCanceled());
		//	_reporter.WriteBalanceEvent += Write;
		//	_reporter.WriteMessageEvent += Write;
		//	await task.Task;
		//}
		//catch (RpcException e)
		//{
		//	Log.Error("Error {@ExceptionMessage}, stack trace {@ExceptionStackTrace}", e.Message, e.StackTrace);
		//	task.SetCanceled();
		//}
		//finally
		//{
		//	_reporter.WriteBalanceEvent -= Write;
		//	_reporter.WriteMessageEvent -= Write;
		//}
		return await task.Task;
    }
}