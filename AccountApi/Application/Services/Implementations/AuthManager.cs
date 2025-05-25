using AccountApi.Application.Services.Interfaces;
using AccountApi.Domain.Entities;
using AccountApi.Dto_s;
using AccountApi.Infrastructure.Helpers.JWT;
using Microsoft.EntityFrameworkCore;
using Shared.Constant;
using Shared.Helpers.ResponseModels.GenericResultModels;
using Shared.Helpers.Security.Hashing;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace AccountApi.Application.Services.Implementations
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }
        public async Task<IDataResult<User>> Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                UserTaxID = userForRegisterDto.UserTaxId,
                UserName = userForRegisterDto.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }
        public async Task<IDataResult<User>> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = await _userService.GetByUserTaxId(userForLoginDto.UserTaxID);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessfulLogin);
        }

        public async Task<IResult> UserExists(string userTaxId)
        {
            if (_userService.GetByUserTaxId(userTaxId) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccesResult();
        }

        public async Task<IDataResult<AccessToken>> CreateAccessToken(User user)
        {
            var claims = await _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public async Task<IDataResult<User>> CheckUserLogin(string userTaxId, string role)
        {
            var userToCheck = await _userService.GetByUserTaxId(userTaxId);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            var listclaims = await _userService.GetClaims(userToCheck.Data);

            if (!listclaims.Data.Exists(l => l.Name.ToLower().Contains(role.ToLower())))
            {
                return new ErrorDataResult<User>(Messages.AccessWarning);
            }

            return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessfulLogin);
        }
    }
}
