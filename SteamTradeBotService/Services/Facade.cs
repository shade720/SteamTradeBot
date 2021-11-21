using System.Threading.Tasks;
using Grpc.Core;
using Serilog;
using SteamTradeBotService.Clients;
using SteamTradeBotService.Models;
using SteamTradeBotService.Models.Worker;
using SteamTradeBotService.Protos;
using Configuration = SteamTradeBotService.Models.Configuration;

namespace SteamTradeBotService.Services;

public class Facade : InterfaceService.InterfaceServiceBase
{
	private readonly Browser _browser;
	private readonly AccountLogger _account;
	private readonly ItemListLoader _itemListLoader;
	private readonly PostgresClient _database;
	private readonly Worker _worker;
	private readonly Reporter _reporter;
	private readonly Configuration _configuration;

	public Facade()
	{
		_configuration = new Configuration();
		_browser = new Browser();
		_database = new PostgresClient();
		_reporter = new Reporter();
		_account = new AccountLogger(_browser, _reporter);
		_itemListLoader = new ItemListLoader(_browser, _database);
		_worker = new Worker(_browser, _database, _reporter);
	}

	public override async Task<StartResponse> StartBot(StartRequest request, ServerCallContext context)
	{
		if (!Worker.WorkingState)
		{
			_worker.SetConfiguration(_configuration);
			await _worker.StartWork();
		}
		return new StartResponse();
	}

	public override async Task<StopResponse> StopBot(StopRequest request, ServerCallContext context)
	{
		if (Worker.WorkingState) 
		{
			await _worker.StopWork();
		}
		return new StopResponse();
	}

	public override async Task<LoadItemListResponse> LoadItemList(LoadItemListRequest request, ServerCallContext context)
	{
		return new LoadItemListResponse 
		{
			Response = await _itemListLoader.Load()
		};
	}

	public override async Task<ClearMyLotsResponse> ClearMyLots(ClearMyLotsRequest request, ServerCallContext context)
	{
		if (!Worker.WorkingState)
		{
			var marketOperationExecutor = new MarketOperationExecutor(_browser, _reporter);
			await marketOperationExecutor.CancelAll();
			_database.ClearItemList();
		}
		return new ClearMyLotsResponse();
	}
        
	public override async Task<SetConfigurationResponse> SetConfiguration(SetConfigurationRequest request, ServerCallContext context)
	{
		await _configuration.SetConfiguration(request.Configuration);
		return new SetConfigurationResponse();
	}

	public override async Task<LogInResponse> LogIn(LogInRequest request, ServerCallContext context)
	{
		return _account.LogState
			? new LogInResponse
				{ Response = new DefaultResponse { Code = ReplyCode.Failure, Message = "AlreadyLogged" } }
			: new LogInResponse { Response = await _account.LogIn() };
	}

	public override async Task<LogOutResponse> LogOut(LogOutRequest request, ServerCallContext context)
	{
		return _account.LogState
			? new LogOutResponse
				{ Response = new DefaultResponse { Code = ReplyCode.Failure, Message = "AlreadyLogged" } }
			: new LogOutResponse { Response = await _account.LogOut() };
	}

	public override async Task<WriteReport> Report(SubscribeReports request, IServerStreamWriter<WriteReport> responseStream, ServerCallContext context)
	{
		var task = new TaskCompletionSource<WriteReport>();
		void Write<T>(T param)
		{
			responseStream.WriteAsync(double.TryParse(param.ToString(), out var balance)
				? new WriteReport { Balance = balance }
				: new WriteReport { Message = param.ToString() });
		}

		try
		{
			context.CancellationToken.Register(() => task.SetCanceled());
			_reporter.WriteBalanceEvent += Write;
			_reporter.WriteMessageEvent += Write;
			await task.Task;
		}
		catch (RpcException e)
		{
			Log.Error("Error {@ExceptionMessage}, stack trace {@ExceptionStackTrace}", e.Message, e.StackTrace);
			task.SetCanceled();
		}
		finally
		{
			_reporter.WriteBalanceEvent -= Write;
			_reporter.WriteMessageEvent -= Write;
		}
		return await task.Task;
	}
}