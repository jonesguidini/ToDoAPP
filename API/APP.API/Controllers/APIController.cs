using APP.Domain.Contracts.Managers;
using APP.Domain.EntitiesConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

namespace APP.API.Controllers
{
    public abstract class APIController : ControllerBase
    {
        private readonly INotificationManager notificationManager;

        public APIController(INotificationManager _notificationManager)
        {
            notificationManager = _notificationManager;
        }

        protected bool OperacaoValida()
        {
            return notificationManager.IsValid();
        }

        protected ActionResult CustomResponse(Object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = notificationManager.GetNotifications().Select(n => new { Key = n.Key, Message = n.Message })
            });
        }

        // validação de notificação de erro
        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErrorModelInvalida(modelState);

            return CustomResponse();
        }

        protected void NotificarErrorModelInvalida(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var error in errors)
            {
                var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotificarError("Error:", errorMessage);
            }
        }

        protected void NotificarError(string key, string errorMessage)
        {
            notificationManager.Handle(new Notification(key, errorMessage));
        }

        protected ActionResult VerificaSeObjetoEstaNuloRetornaNotificacao(object result, string key, string message)
        {
            if (result == null)
            {
                NotificarError(key, message);
                return CustomResponse();
            }

            return null;
        }
    }
}
