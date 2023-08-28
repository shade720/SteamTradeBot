using System.Linq;
using Microsoft.AspNetCore.Http;
using SteamTradeBot.Backend.DataAccessLayer;
using System.Threading.Tasks;
using SteamTradeBot.Backend.Models.StateModel;

namespace SteamTradeBot.Backend.Middlewares;

public class ExclusiveAccessMiddleware
{
    private readonly RequestDelegate _next;
    private readonly StateDbAccess _db;

    public ExclusiveAccessMiddleware(RequestDelegate next, StateDbAccess db)
    {
        _next = next;
        _db = db;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Query["apiKey"].ToString();
        var states = await _db.GetStatesAsync();
        if (states.Any(s => s.WorkingState == ServiceWorkingState.Up && s.ApiKey != token))
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Application is already in work.");
        }
        else
        {
            await _next.Invoke(context);
        }
    }
}