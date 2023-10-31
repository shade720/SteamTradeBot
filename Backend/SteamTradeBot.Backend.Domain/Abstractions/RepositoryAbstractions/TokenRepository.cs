namespace SteamTradeBot.Backend.Domain.Abstractions.RepositoryAbstractions;

public interface ITokenRepository
{
    public Task<bool> Contains(string token);
}