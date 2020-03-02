namespace APP.Domain.Contracts.Services
{
    public interface IAuthService
    {
        string GetClaims(string chave);
    }
}
