using AccountApi.Dto_s;
using AccountApi.Helpers.JWT;
using AccountApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Shared.Constant;
using Shared.Dto_s;
using Shared.Helpers.ResponseModels.GenericResultModels;
using System.Data;

namespace AccountApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        readonly IAuthService _authService;
        readonly ITokenHelper _tokenHelper;

        public AccountController(IAuthService authService, ITokenHelper tokenHelper)
        {
            _authService = authService;
            _tokenHelper = tokenHelper;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }


            return Ok(result.Data);
        }

        [HttpPost("loginAcces")]
        [Authorize]
        public async Task<ActionResult> LoginAccess(UserForLoginAccessDto role)
        {
            var jwtToken = _tokenHelper.ValidateTokenGetClaims(HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", ""));

            if (jwtToken is null)
            {
                return Ok(Messages.FailedProccess);
            }

            var result = _authService.CheckUserLogin(jwtToken.Claims.FirstOrDefault(s => s.Type.Contains("nameidentifier")).Value, role.Role);

            if (!result.Success)
            {
                return Ok(Messages.FailedProccess);
            }

            JwtDto jwtDto = JwtDto.GetViewModel(jwtToken);

            return Ok(jwtDto);
        }

    }
}
