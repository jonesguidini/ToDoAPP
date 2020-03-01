namespace APP.Domain.Contracts.Services
{
    public interface IEmailMessage
    {
        void SendEmail(string emailTo, string SendCopyTo, string subject, string message, string templateName);
    }
}
