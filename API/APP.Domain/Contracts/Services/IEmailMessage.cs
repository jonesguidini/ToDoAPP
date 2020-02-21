using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APP.Domain.Contracts.Services
{
    public interface IEmailMessage
    {
        void SendEmail(string emailTo, string SendCopyTo, string subject, string message, string templateName);
    }
}
