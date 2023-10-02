using Microsoft.AspNetCore.Http;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.Abstractions.RepositoryAbstractions;
using SteamTradeBot.Backend.BusinessLogicLayer.Models.StateModel;
using System.Linq;
using System.Threading.Tasks;

namespace SteamTradeBot.Backend.BusinessLogicLayer.Middlewares;

public class ExclusiveAccessMiddleware
{
    private readonly RequestDelegate _next;
    private readonly StateRepository _stateRepository;

    public ExclusiveAccessMiddleware(RequestDelegate next, StateRepository stateRepository)
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