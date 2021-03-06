using APP.Data.Context;
using APP.Domain.Contracts.Managers;
using APP.Domain.Contracts.Repositories;
using APP.Domain.Entities;

namespace APP.Data.Repositories
{
    public class LogRepository : RepositoryBase<Log>, ILogRepository
    {
        public LogRepository(SQLContext DataContext, INotificationManager _gerenciadorNotificacoes) : base(DataContext, _gerenciadorNotificacoes)
        {
        }
    }
}