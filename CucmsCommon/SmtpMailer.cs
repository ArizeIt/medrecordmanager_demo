using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PVAMCommon
{
    public class SmtpMailer:IDisposable
    {
       private string Server { get; set; }
       private  string ClientUser { get;  set; }
        private int Port { get; set; }
        private string ClientPassword { get;  set; }
      
        public SmtpMailer(string smtp, int port, string username, string password)
        {
            Server  = smtp;
            Port = port;
            ClientUser = username;
            ClientPassword = password;
        }

        public async Task SendExceptionEmail(string fromAddress, string techAddresses, string body)
        {

            await SendMailAsync(fromAddress, new List<string> { techAddresses}, null, "Important: Integration Cought Exception(s)", body, null);
        }

        public async Task SendResultEmail(string fromAddress, List<string> toAddress, List<string> ccAddress, string body)
        {

            await SendMailAsync(fromAddress, toAddress, ccAddress, "Important: Integration Cought Exception(s)", body, null);
        }

        public async Task SendMailAsync(string fromAddress, List<string> toAddresses, List<string> ccAddresses, string subject, string body, string filePath)
        {
            var mail = new MailMessage()
            {
                To = { },
                CC = {}    
            };

            var smtpClient = new SmtpClient(Server)
            {
                Port = Port,
                Credentials = new System.Net.NetworkCredential(ClientUser, ClientPassword),
                EnableSsl = true
            };

            mail.From = new MailAddress(fromAddress);
            mail.Sender = new MailAddress(fromAddress);

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
            
            smtpClient.SendCompleted += (s, e) =>
             {
                 smtpClient.Dispose();
                 mail.Dispose();
             };

            await smtpClient.SendMailAsync(mail);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SmtpMailer() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
