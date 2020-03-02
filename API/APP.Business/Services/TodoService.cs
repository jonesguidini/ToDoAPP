using APP.Domain.Contracts.FluentValidation;
using APP.Domain.Contracts.Managers;
using APP.Domain.Contracts.Repositories;
using APP.Domain.Contracts.Services;
using APP.Domain.Entities;
using AutoMapper;

namespace APP.Business.Services
{
    public class TodoService : ServiceBase<Todo>, ITodoService
    {
        public TodoService(ITodoRepository _repository, INotificationManager _gerenciadorNotificacoes, IMapper _mapper, IFluentValidation<Todo> _fluentValidation, IAuthService _authService) : base(_repository, _gerenciadorNotificacoes, _mapper, _fluentValidation, _authService)
        {
        }
    }
}
