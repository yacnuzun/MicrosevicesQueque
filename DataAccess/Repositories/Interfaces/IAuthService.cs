using Shared.DTO_s;
using Shared.Entities;
using Shared.Helpers.ResponseModels.GenericResultModels;
using Shared.Helpers.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Repositories.Interfaces
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto, string role);
        IResult UserExists(string userTaxId);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
