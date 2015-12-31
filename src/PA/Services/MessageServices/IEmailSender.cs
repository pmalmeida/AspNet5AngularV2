using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PA.Services.MessageServices
{
    public interface IEmailSender
    {
        void Send(string email, string subject, string message);
    }
}
