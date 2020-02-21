using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using APP.Domain.Entities;

namespace APP.Domain.Contracts.Services
{
    public interface IAuthService
    {
        string GetClaims(string chave);
    }
}
