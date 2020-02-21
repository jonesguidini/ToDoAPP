using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using APP.Domain.Contracts.Managers;
using APP.Domain.EntitiesConfig;
using Microsoft.AspNetCore.Identity;


namespace APP.Business.Config
{
    public class Notifiable
    {
        private readonly INotificationManager notificationManager;

        public Notifiable(INotificationManager _notificationManager)
        {
            notificationManager = _notificationManager;
        }

        /// <summary>
        /// Notificar errors vindo do ValidationResult
        /// </summary>
        /// <param name="validationResult"></param>
        /// <returns></returns>
        protected virtual async Task Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                await Notify(error.PropertyName, error.ErrorMessage);
            }
        }

        /// <summary>
        /// Notificar erros vindo do IdentityResult
        /// </summary>
        /// <param name="validationResult"></param>
        /// <returns></returns>
        protected virtual async Task Notify(IdentityResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                await Notify(error.Code, error.Description);
            }
        }

        /// <summary>
        /// Notificar erros manualmente
        /// </summary>
        /// <param name="key"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual Task Notify(string key, string message)
        {
            return notificationManager.Handle(new Notification(key, message));
        }

        /// <summary>
        /// Retorna se existe errors, ou seja, está invalido
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsValid()
        {
            return notificationManager.IsValid();
        }
    }
}
