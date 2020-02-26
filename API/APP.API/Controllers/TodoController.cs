using System.Collections.Generic;
using System.Threading.Tasks;
using APP.Domain.Contracts.Managers;
using APP.Domain.Contracts.Services;
using APP.Domain.DTOs;
using APP.Domain.Entities;
using APP.Domain.VMs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APP.API.Controllers
{
    [Authorize]
    [Route("api/todos")]
    [ApiController]
    public class TodoController : APIController
    {
        private readonly ITodoService _todoService;
        private readonly IMapper _mapper;
        public TodoController(ITodoService todoService, IMapper mapper, INotificationManager _gerenciadorNotificacoes) : base(_gerenciadorNotificacoes)
        {
            _mapper = mapper;
            _todoService = todoService;
        }

        /// <summary>
        /// Retornar todas categorias de despesas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TodoVM>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            List<string> includes = new List<string> { "TodoCategory" };

            var todoCategories = await _todoService.GetAll(includes);
            var listTodoVM = _mapper.Map<IEnumerable<TodoVM>>(todoCategories);
            return CustomResponse(listTodoVM);
        }

        /// <summary>
        /// Retornar categoria de despesa filtrado pelo parametro 'id'
        /// </summary>
        /// <param name="id">Parâmetro para filtro por ID </param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(TodoVM), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            List<string> includes = new List<string> { "TodoCategory" };
            var todoCategory = await _todoService.GetById(id, includes);

            TodoVM todoCategoryVM = _mapper.Map<TodoVM>(todoCategory);
            return CustomResponse(todoCategoryVM);
        }

        /// <summary>
        /// Criar nova categoria de despesa
        /// </summary>
        /// <param name="todoDTO">Objeto informado para cadastro do registro</param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(TodoDto todoDTO)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var todo = _mapper.Map<Todo>(todoDTO);
            await _todoService.Add(todo);
            return CustomResponse("Categoria de despesa cadastrada com sucesso");
        }

        /// <summary>
        /// Atualizar categoria de despesa
        /// </summary>
        /// <param name="id">Parâmetro para filtro da categoria a ser alterada</param>
        /// <param name="todoDTO">Objeto da categoria a ser alterada </param>
        /// <returns></returns>
        [HttpPut]
        [Route("update/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(int id, TodoDto todoDTO)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var todoCategoryBanco = await _todoService.GetById(id);

            if (id != todoDTO.Id)
            {
                NotificarError("Id", "O ID informado não confere com o ID da categoria da despesa.");
                return CustomResponse();
            }

            var todo = _mapper.Map<Todo>(todoDTO);
            await _todoService.Update(todo);
            return CustomResponse("Categoria de despesas atualizada com sucesso!");
        }

        /// <summary>
        /// Excluir categoria de despesa
        /// </summary>
        /// <param name="id">Parâmetro para filtro da categoria a ser exclu´´ida</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _todoService.GetById(id);

            if (todo == null)
            {
                NotificarError("Categoria de Despesa", "A Categoria de despesa informada não existe.");
                return CustomResponse();
            }

            await _todoService.DeleteLogically(todo);

            return CustomResponse("Categoria de despesa excluida com sucesso!");
        }

    }
}
