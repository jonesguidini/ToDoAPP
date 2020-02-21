using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using APP.Domain.Contracts.FluentValidation;
using APP.Domain.Contracts.Managers;
using APP.Domain.Contracts.Repositories;
using APP.Domain.Contracts.Services;
using APP.Domain.Entities;

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
