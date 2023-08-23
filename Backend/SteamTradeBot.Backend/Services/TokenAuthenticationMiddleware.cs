using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using SteamTradeBot.Backend.DataAccessLayer;

namespace SteamTradeBot.Backend.Services;

public class TokenAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly TokenDbAccess _db;

    public TokenAuthenticationMiddleware(RequestDelegate next, TokenDbAccess db)
    {
        _next = next;
        _db = db;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Query["apiKey"].ToString();
        if (await _db.Contains(token))
        {
            await _next.Invoke(context);
        }
        else
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Invalid token!");
        }
    }
}