using APP.Domain.Contracts.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace APP.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAcessor;

        public AuthService(IHttpContextAccessor httpContextAcessor)
        {
            _httpContextAcessor = httpContextAcessor;
        }

        public string GetClaims(string chave)
        {
            string listaClaimsValues = "";

            var filtroClaims = _httpContextAcessor.HttpContext.User.Claims
                .GroupBy(claim => claim.Type)
                .ToList().Where(x => x.Key.ToLower() == chave.ToLower())
                .Select(b => b)
                .ToDictionary(group => group.Key, group => group).Values.FirstOrDefault();

            if (chave.ToLower() == ClaimTypes.Role.ToLower())
            {
                listaClaimsValues = String.Join(", ", filtroClaims.Select(x => x.Value).ToList());
            }
            else
            {
                listaClaimsValues = filtroClaims?.FirstOrDefault()?.Value;
            }

            return listaClaimsValues;

        }
    }
}
