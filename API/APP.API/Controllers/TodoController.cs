using APP.Domain.Contracts.Managers;
using APP.Domain.Contracts.Services;
using APP.Domain.DTOs;
using APP.Domain.Entities;
using APP.Domain.VMs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APP.API.Controllers
{
    //[Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/todos")]
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
            var todoCategories = await _todoService.GetAll();
            var listTodoVM = _mapper.Map<IEnumerable<TodoVM>>(todoCategories);
            return CustomResponse(listTodoVM);
        }

        /// <summary>
        /// Filtrar tarefas por nomes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("search/{title}")]
        [ProducesResponseType(typeof(IEnumerable<TodoVM>), StatusCodes.Status200OK)]
        public async Task<IActionResult> FilterTodos(string title)
        {
            var filterCategories = await _todoService.Search(x => x.Title.ToLower().Contains(title.ToLower()));
            var listTodoVM = _mapper.Map<IEnumerable<TodoVM>>(filterCategories);
            return CustomResponse(listTodoVM);
        }

        /// <summary>
        /// Retornar Tarefa filtrado pelo parametro 'id'
        /// </summary>
        /// <param name="id">Parâmetro para filtro por ID </param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(TodoVM), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var todo = await _todoService.GetById(id);
            TodoVM todoVM = _mapper.Map<TodoVM>(todo);
            return CustomResponse(todoVM);
        }

        /// <summary>
        /// Criar nova Tarefa
        /// </summary>
        /// <param name="todoDTO">Objeto informado para cadastro do registro</param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(TodoDTO todoDTO)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var todo = _mapper.Map<Todo>(todoDTO);
            todo.IsDone = false;

            await _todoService.Add(todo);
            return CustomResponse("Tarefa cadastrada com sucesso");
        }

        /// <summary>
        /// Atualizar Tarefa
        /// </summary>
        /// <param name="id">Parâmetro para filtro da categoria a ser alterada</param>
        /// <param name="todoDTO">Objeto da categoria a ser alterada </param>
        /// <returns></returns>
        [HttpPut]
        [Route("update/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(int id, TodoDTO todoDTO)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var todoBanco = await _todoService.GetById(id);

            if (id != todoDTO.Id)
            {
                NotificarError("Id", "O ID informado não confere com o ID da categoria da despesa.");
                return CustomResponse();
            }

            var todo = _mapper.Map<Todo>(todoDTO);
            await _todoService.Update(todo);
            return CustomResponse("Tarefas atualizada com sucesso!");
        }


        /// <summary>
        /// Atualizar Status da Tarefa , se estiver 'Done' vai desfazer e vice versa
        /// </summary>
        /// <param name="id">Parâmetro para filtro da categoria a ser alterada</param>
        /// <param name="todoDTO">Objeto da categoria a ser alterada </param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateStatus/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateStatus(int id)
        {
            if (id == 0)
            {
                NotificarError("Id", "O ID informado não existe.");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var todoBanco = await _todoService.GetById(id);

            todoBanco.IsDone = todoBanco.IsDone == null ? true : !todoBanco.IsDone;

            await _todoService.Update(todoBanco);
            return CustomResponse("O status da tarefa foi atualizada com sucesso!");
        }

        /// <summary>
        /// Excluir Tarefa
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
                NotificarError("Tarefa", "A Tarefa informada não existe.");
                return CustomResponse();
            }

            //await _todoService.DeleteLogically(todo);
            await _todoService.Delete(id);

            return CustomResponse("Tarefa excluida com sucesso!");
        }

    }
}
