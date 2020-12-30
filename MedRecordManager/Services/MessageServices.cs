﻿using PVAMCommon;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedRecordManager.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713


    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public Task SendAdminEmailAsync(string email, string subject, string message)
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
            return smtpClient.SendMailAsync("webadmin@cmucs.com", new List<string>() { email }, null, subject, message, null);
        }

        public Task SendEmailAsync(string fromEmail, string email, string subject, string message, string filename)
        {
            var smtpClient = new SmtpMailer("smtp.gmail.com", 587, "webadmin@cmucs.com", "Zsxdcf12!");
            return smtpClient.SendMailAsync(fromEmail, new List<string>() { email }, null, subject, message, filename);
        }

        public Task SendEmailAsync(string fromEmail, string email, string subject, string message, byte[] fileContent, string filename)
        {
            var smtpClient = new SmtpMailer("smtp.gmail.com", 587, "webadmin@cmucs.com", "Zsxdcf12!");
            return smtpClient.SendMailAsync(fromEmail, new List<string>() { email }, null, subject, message, fileContent, filename);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
