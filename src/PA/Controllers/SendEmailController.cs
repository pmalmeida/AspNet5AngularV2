using Microsoft.AspNet.Mvc;
using PA.Services.MessageServices;

namespace PA.Controllers
{
    [Route("api/[controller]")]
    public class SendEmailController : Controller
    {
        private readonly IEmailSender _emailSender;

        public SendEmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost]
        public void Post([FromBody]EmailMessage mail)
        {
            _emailSender.Send(mail.To, mail.Subject, mail.Message);            
        }          
    }

    public class EmailMessage
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
