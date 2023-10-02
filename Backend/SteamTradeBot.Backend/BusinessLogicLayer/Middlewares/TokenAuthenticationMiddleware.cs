using Microsoft.AspNetCore.Http;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.RepositoryAbstractions;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Middlewares;

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