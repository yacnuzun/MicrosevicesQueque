using Shared.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO_s
{
    public class UserForLoginDto : IDTO
    {
        public string UserName { get; set; }
        public string UserTaxID { get; set; }
        public string Password { get; set; }
    }
}
