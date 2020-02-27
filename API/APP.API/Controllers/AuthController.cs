
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using APP.Domain.Contracts.Managers;
using APP.Domain.Contracts.Repositories;
using APP.Domain.DTOs;
using APP.Domain.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APP.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthController : APIController
    {
        private readonly IAuthRepository _repo;

        public IConfiguration _config { get; }
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper, INotificationManager _gerenciadorNotificacoes) : base(_gerenciadorNotificacoes)
        {
            _mapper = mapper;
            _repo = repo;
            _config = config;
        }

        [Authorize]
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(UserForRegisterDTO userRegisterDTO)
        {

            userRegisterDTO.Username = userRegisterDTO.Username.ToLower();

            if (await _repo.UserExists(userRegisterDTO.Username))
            {
                NotificarError("Usuário", "Já existe um usuário cadastrado com esse login.");
                return CustomResponse();
            }

            // TODO: ajustar mapeamento
            var userToCreate = new User()
            {
                Username = userRegisterDTO.Username,
                Email = userRegisterDTO.Email
            };

            var createdUser = await _repo.Register(userToCreate, userRegisterDTO.Password);

            return CustomResponse(createdUser);
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(UserForLoginDTO userForLoginDTO)
        {
            try
            {
                var userFromRepo = await _repo.Login(userForLoginDTO.Username.ToLower(), userForLoginDTO.Password);

                if (userFromRepo == null)
                {
                    NotificarError("Login", "Usuário ou senha não conferem.");
                    return CustomResponse();
                    //return Unauthorized();
                }

                // cria dois claims
                var claims = new[]
                {
                new Claim("UserId", userFromRepo.Id.ToString()),
                new Claim("UserName", userFromRepo.Username)
                //new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                //new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

                // cria a key a ser usada na credencial em base do token informado no arquivo appsettings.json
                var key = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(_config.GetSection("AppSettings:Token").Value));

                // cria a credencial
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                // Cria estrutura do token
                var tokenDecriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims), // com base nas claims já criada anteriormente
                    Expires = DateTime.Now.AddDays(1), // expira em 1 dia   
                    SigningCredentials = credentials // com base na credencial criada anteiormente
                };

                // cria um handler de token a ser usando para gerar o token
                var tokenHandler = new JwtSecurityTokenHandler();

                // gera o token
                var token = tokenHandler.CreateToken(tokenDecriptor);

                // retorna o tolen gerado
                return CustomResponse(new
                {
                    token = tokenHandler.WriteToken(token)
                });
            }
            catch (Exception ex)
            {
                var message = ex.InnerException != null ? ex.InnerException.ToString() : ex.Message;
                return CustomResponse(message);
            }
            

        }
    }
}