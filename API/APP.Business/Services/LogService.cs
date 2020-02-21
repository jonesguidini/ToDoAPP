using AutoMapper;
using APP.Domain.Contracts.FluentValidation;
using APP.Domain.Contracts.Managers;
using APP.Domain.Contracts.Repositories;
using APP.Domain.Contracts.Services;
using APP.Domain.Entities;

namespace APP.Business.Services
{
    public class LogService : ServiceBase<Log>, ILogService
    {
        public LogService(ILogRepository _repository, INotificationManager _gerenciadorNotificacoes, IMapper _mapper, IFluentValidation<Log> _fluentValidation, IAuthService _authService) : base(_repository, _gerenciadorNotificacoes, _mapper, _fluentValidation, _authService)
        {
        }
    }
}
