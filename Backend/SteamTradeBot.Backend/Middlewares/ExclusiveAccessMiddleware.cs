using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.Domain.StateAggregate;

namespace SteamTradeBot.Backend.UI.Middlewares;

public class ExclusiveAccessMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IStateRepository _stateRepository;

    public ExclusiveAccessMiddleware(
        RequestDelegate next, 
        IStateRepository stateRepository)
    {
        _next = next;
        _stateRepository = stateRepository;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Query["apiKey"].ToString();
        var states = await _stateRepository.GetStatesAsync();
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