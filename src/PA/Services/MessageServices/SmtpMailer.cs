using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PA.Services.MessageServices
{
    public class SmtpMailer
    {
        public class SmtpSettings
        {
            public string Host { get; set; }
            public int Port { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public void Send(SmtpSettings settings, string from, string to, string subject, string body)
        {
            try
            {
                var fromAddress = new MailAddress(from);
                var toAddress = new MailAddress(to);
                string fromPassword = settings.Password;
                
                var smtp = new SmtpClient
                {
                    Host = settings.Host,
                    Port = settings.Port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(settings.Username, settings.Password)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    smtp.Send(message);
                    //return smtp.SendMailAsync(message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
