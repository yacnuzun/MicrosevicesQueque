using Shared.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO_s
{
    public class UserForRegisterDto : IDTO
    {
        public string UserTaxId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
