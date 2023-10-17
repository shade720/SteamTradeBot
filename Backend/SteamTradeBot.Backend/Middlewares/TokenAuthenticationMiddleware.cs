using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;

namespace SteamTradeBot.Backend.UI.Middlewares;

public class TokenAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly TokenRepository _db;

    public TokenAuthenticationMiddleware(RequestDelegate next, TokenRepository db)
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