using AccountApi.Application.Services.Interfaces;
using AccountApi.Domain.Entities;
using AccountApi.Dto_s;
using AccountApi.Infrastructure.Helpers.JWT;
using Shared.Constant;
using Shared.Helpers.ResponseModels.GenericResultModels;
using Shared.Helpers.Security.Hashing;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace AccountApi.Application.Services.Implementations
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        private readonly ILogger<AuthManager> _log;

        public AuthManager(IUserService userService,
            ITokenHelper tokenHelper,
            ILogger<AuthManager> log)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _log = log;
        }
        public async Task<IDataResult<User>> Register(UserForRegisterDto userForRegisterDto)
        {
            try
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
                var user = new User
                {
                    UserTaxID = userForRegisterDto.UserTaxId,
                    UserName = userForRegisterDto.UserName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Email = userForRegisterDto.Email,
                    Status = true
                };
                _userService.Add(user, userForRegisterDto.Role);

                return new SuccessDataResult<User>(user, Messages.UserRegistered);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                throw;
            }

        }
        public async Task<IDataResult<User>> Login(UserForLoginDto userForLoginDto)
        {
            try
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
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                throw;
            }
            
        }

        public async Task<IResult> UserExists(string userTaxId)
        {
            try
            {
                if (_userService.GetByUserTaxId(userTaxId) != null)
                {
                    return new ErrorResult(Messages.UserAlreadyExists);
                }
                return new SuccesResult();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                throw;
            }
            
        }

        public async Task<IDataResult<AccessToken>> CreateAccessToken(User user)
        {
            var claims = await _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public async Task<IDataResult<User>> CheckUserLogin(string userTaxId, string role)
        {
            try
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
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                throw;
            }
            
        }
    }
}
