using PA.Services.MessageServices;
using System.Threading.Tasks;

namespace PA.Services.MessageServices
{

    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public void Send(string email, string subject, string message)
        {
            //return Task.Run(() => {return 1;});

            var m = new SmtpMailer();

            m.Send(new SmtpMailer.SmtpSettings()
            {
                Host = "smtp.gmail.com",
                Username = "itmassive.test@gmail.com",                                
                Password = "itmassive2015",
                Port = 587
            }
                , "itmassive.test@gmail.com"
                , email
                , subject
                , message);
        }

        public Task SendSmsAsync(string number, string message)
        {
            return Task.FromResult(0);
        }
    }
}
