using PVAMCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedRecordManager.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public  Task SendAdminEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            var smtpClient = new SmtpMailer("smtp.gmail.com", 587, "webadmin@cmucs.com", "Zsxdcf12!");

           return smtpClient.SendMailAsync("admin", new List<string>() { email }, null, subject, message, null);
        }

        public Task SendEmailAsync(string fromEmail, string toemail, string subject, string message)
        {
            // Plug in your email service here to send an email.
            var smtpClient = new SmtpMailer("smtp.gmail.com", 587, "webadmin@cmucs.com", "Zsxdcf12!");

            return smtpClient.SendMailAsync(fromEmail, new List<string>() { toemail }, null, subject, message, null);
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpClient = new SmtpMailer("smtp.gmail.com", 587, "webadmin@cmucs.com", "Zsxdcf12!");
            return smtpClient.SendMailAsync("admin", new List<string>() { email }, null, subject, message, null);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
