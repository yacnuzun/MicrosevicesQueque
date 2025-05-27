using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers.Mailing
{
    public class EmailMessage
    {
        public string Contacts { get; set; }
        public string SenderMail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
