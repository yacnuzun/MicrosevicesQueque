using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers.Mailing
{
    public interface IMailSenderStrategy
    {
        Task<bool> SendAsync(EmailMessage message);
    }
}
