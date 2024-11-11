using AccountApi.Dto_s;
using AccountApi.Entities;
using AccountApi.Helpers.JWT;
using Shared.Helpers.ResponseModels.GenericResultModels;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace AccountApi.Repositories.Interfaces
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IDataResult<User> CheckUserLogin(string userTaxId, string role);
        IResult UserExists(string userTaxId);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
