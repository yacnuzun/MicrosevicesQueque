using AccountApi.Domain.Entities;
using AccountApi.Dto_s;
using AccountApi.Infrastructure.Helpers.JWT;
using Shared.Helpers.ResponseModels.GenericResultModels;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace AccountApi.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IDataResult<User>> Register(UserForRegisterDto userForRegisterDto);
        Task<IDataResult<User>> Login(UserForLoginDto userForLoginDto);
        Task<IDataResult<User>> CheckUserLogin(string userTaxId, string role);
        Task<IResult> UserExists(string userTaxId);
        Task<IDataResult<AccessToken>> CreateAccessToken(User user);
    }
}
