using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace PVAMCommon
{
    public  class Email
    {
       private string Server { get; set; }
       private  string ClientUser { get;  set; }
        private int Port { get; set; }
        private string ClientPassword { get;  set; }
      
        public Email(string smtp, int port, string username, string password)
        {
            Server  = smtp;
            Port = port;
            ClientUser = username;
            ClientPassword = password;
        }

        public void SendExceptionEmail(string fromAddress, string techAddresses, string body)
        {

            SendMail(fromAddress, new List<string> { techAddresses}, null, "Important: Integration Cought Exception(s)", body, null);
        }

        public void SendResultEmail(string fromAddress, List<string> toAddress, List<string> ccAddress, string body)
        {

            SendMail(fromAddress, toAddress, ccAddress, "Important: Integration Cought Exception(s)", body, null);
        }

        public void SendMail(string fromAddress, List<string> toAddresses, List<string> ccAddresses, string subject, string body, string filePath)
        {
            var mail = new MailMessage()
            {
                To = { },
                CC = {}    
            };

            var smtpServer = new SmtpClient(Server)
            {
                Port = Port,
                Credentials = new System.Net.NetworkCredential(ClientUser, ClientPassword),
                EnableSsl = true
            };

            mail.From = new MailAddress(fromAddress);
            if (toAddresses != null && toAddresses.Any())
            {
                foreach (var ad in toAddresses)
                {
                    mail.To.Add(ad);
                }
            }

            if (ccAddresses != null && ccAddresses.Any())
            {
                foreach (var ad in ccAddresses)
                {
                    mail.CC.Add(ad);
                }
            }

            mail.Subject = subject;
            mail.Body = body;

            if (!string.IsNullOrEmpty(filePath))
            {
                var attachment = new Attachment(filePath);
            
                mail.Attachments.Add(attachment);
                    
            }
            smtpServer.Send(mail);
            mail.Dispose();
        }
    }
}
