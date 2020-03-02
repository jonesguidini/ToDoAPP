using APP.Business.Config;
using APP.Domain.Contracts.Managers;
using APP.Domain.Contracts.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace APP.Business.Services
{
    public class EmailService : Notifiable, IEmailMessage
    {
        private IConfigurationSection emailSettings;

        public EmailService(INotificationManager _notificationManager) : base(_notificationManager)
        {
            // seta onde fica a configuração do email
            emailSettings = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build()
                .GetSection("EmailSettings");
        }

        public async void SendEmail(string emailTo, string SendCopyTo, string subject, string message, string templateName)
        {
            try
            {
                if (!String.IsNullOrEmpty(templateName))
                {
                    string templatePath = "APP.API/Helpers/TemplatesEmail/" + templateName + ".html";
                    string projectRootPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
                    message = File.ReadAllText(Path.Combine(projectRootPath, templatePath));
                }

                MailMessage email = new MailMessage
                {
                    From = new MailAddress(emailSettings["UsernameEmail"], emailSettings["FromEmail"]),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                    Priority = MailPriority.High
                };

                email.To.Add(emailTo);

                if (!String.IsNullOrEmpty(SendCopyTo))
                    email.Bcc.Add(SendCopyTo);

                using (SmtpClient smtp = new SmtpClient(emailSettings["Dominio"], Int32.Parse(emailSettings["Porta"])))
                {
                    smtp.Credentials = new NetworkCredential(emailSettings["UsernameEmail"], emailSettings["UsernamePassword"]);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(email);
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                await Notify("Error", errorMessage);
            }
        }
    }
}
