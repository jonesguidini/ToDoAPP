using APP.Domain.Contracts.Managers;
using APP.Domain.Contracts.Repositories;
using APP.Domain.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APP.API.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UsersController : APIController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper, INotificationManager _gerenciadorNotificacoes) : base(_gerenciadorNotificacoes)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAll();
            var usersDTO = _mapper.Map<IEnumerable<UserForRegisterDTO>>(users);
            //return CustomResponse(usersDTO);
            return CustomResponse(usersDTO);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userRepository.GetById(id);

            if (user == null)
            {
                NotificarError("Usuário", "O usuário informado não existe.");
                return CustomResponse();
            }

            var userDTO = _mapper.Map<UserForRegisterDTO>(user);
            //return CustomResponse(userDTO);
            return CustomResponse(userDTO);
        }

        [HttpPut]
        [Route("update/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(int id, UserForRegisterDTO userDTO)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var userBanco = await _userRepository.GetById(id);

            if(userBanco == null)
            {
                NotificarError("Usuário", "Não existe um usuário para o ID informado.");
                return CustomResponse();
            }

            // mapper
            _mapper.Map(userDTO, userBanco);

            //_userRepository.Update(userBanco);
            await _userRepository.SaveChanges();

            return CustomResponse(userBanco);
        }
    }
}