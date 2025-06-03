using Shared.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto_s
{
    public class UserForLoginAccessDto : IDTO
    {
        public string Role { get; set; }
    }
}
