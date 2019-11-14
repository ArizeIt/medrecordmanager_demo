using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedRecordManager.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);

        Task SendEmailAsync(string fromEmail, string email, string subject, string message);

        Task SendEmailAsync(string fromEmail, string email, string subject, string message, string filename);

        Task SendEmailAsync(string fromEmail, string email, string subject, string message, byte[] fileContent, string filename);
    }
}
