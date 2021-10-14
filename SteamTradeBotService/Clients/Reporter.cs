using System.Threading.Tasks;
using Grpc.Core;
using Serilog;
using SteamTradeBotService.Protos;

namespace SteamTradeBotService.Clients
{
    public class Reporter : ReportService.ReportServiceBase
    {
        public delegate void BalanceWriter (double balance);
        public delegate void MessageWriter (string message);

        public static BalanceWriter BalanceWriteEvent;
        public static MessageWriter MessageWriteEvent;

        public override async Task<WriteBackResponse> WriteBack(WriteBackRequest request, IServerStreamWriter<WriteBackResponse> responseStream, ServerCallContext context)
        {
            var task = new TaskCompletionSource<WriteBackResponse>();
            void WriteMessage<T>(T param)
            {
                WriteEvent(param, responseStream);
            }

            try
            {
                context.CancellationToken.Register(() => task.SetCanceled());
                BalanceWriteEvent += WriteMessage;
                MessageWriteEvent += WriteMessage;
                await task.Task;
            }
            catch (RpcException e)
            {
                Log.Error("Error {@ExceptionMessage}, stack trace {@ExceptionStackTrace}", e.Message, e.StackTrace);
                task.SetCanceled();
            }
            finally
            {
                BalanceWriteEvent -= WriteMessage;
                MessageWriteEvent -= WriteMessage;
            }

            return await task.Task;
        }

        private static void WriteEvent<T>(T param, IAsyncStreamWriter<WriteBackResponse> responseStream)
        {
            responseStream.WriteAsync(double.TryParse(param.ToString(), out var balance)
                ? new WriteBackResponse { Balance = balance }
                : new WriteBackResponse { Message = param.ToString() });
        }
    }
}
