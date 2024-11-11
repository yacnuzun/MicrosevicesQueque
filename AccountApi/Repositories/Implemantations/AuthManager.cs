using AccountApi.Dto_s;
using AccountApi.Entities;
using AccountApi.Helpers.JWT;
using AccountApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Constant;
using Shared.Helpers.ResponseModels.GenericResultModels;
using Shared.Helpers.Security.Hashing;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace AccountApi.Repositories.Implemantations
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
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
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
        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByUserTaxId(userForLoginDto.UserTaxID);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(string userTaxId)
        {
            if (_userService.GetByUserTaxId(userTaxId) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccesResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IDataResult<User> CheckUserLogin(string userTaxId, string role)
        {
            var userToCheck = _userService.GetByUserTaxId(userTaxId);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            var listclaims = _userService.GetClaims(userToCheck);

            if (!listclaims.Exists(l => l.Name.ToLower().Contains(role.ToLower())))
            {
                return new ErrorDataResult<User>(Messages.AccessWarning);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }
    }
}
