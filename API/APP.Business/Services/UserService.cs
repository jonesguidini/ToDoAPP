using APP.Domain.Contracts.FluentValidation;
using APP.Domain.Contracts.Managers;
using APP.Domain.Contracts.Repositories;
using APP.Domain.Contracts.Services;
using APP.Domain.Entities;
using AutoMapper;

namespace APP.Business.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        public UserService(IUserRepository _repository, INotificationManager _gerenciadorNotificacoes, IMapper _mapper, IFluentValidation<User> _fluentValidation, IAuthService _authService) : base(_repository, _gerenciadorNotificacoes, _mapper, _fluentValidation, _authService)
        {
        }
    }
}
