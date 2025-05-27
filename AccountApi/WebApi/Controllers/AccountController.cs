using AccountApi.Application.Services.Interfaces;
using AccountApi.Dto_s;
using AccountApi.Infrastructure.Helpers.JWT;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Shared.Constant;
using Shared.Dto_s;
using Shared.Events;
using Shared.Helpers.ResponseModels.GenericResultModels;
using System.Data;

namespace AccountApi.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenHelper _tokenHelper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IValidator<UserForRegisterDto> _validator;
        public AccountController(IAuthService authService, 
            ITokenHelper tokenHelper, 
            IPublishEndpoint publishEndpoint, 
            IValidator<UserForRegisterDto> validator)
        {
            _authService = authService;
            _tokenHelper = tokenHelper;
            _publishEndpoint = publishEndpoint;
            _validator = validator;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = await _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = await _authService.CreateAccessToken(userToLogin.Data);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }


            return Ok(result.Data);
        }

        [HttpPost("loginaccess")]
        [Authorize]
        public async Task<ActionResult> LoginAccess(UserForLoginAccessDto role)
        {
            var jwtToken = _tokenHelper.ValidateTokenGetClaims(HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", ""));

            if (jwtToken is null)
            {
                return Ok(Messages.FailedProccess);
            }

            var result = await _authService.CheckUserLogin(jwtToken.Claims.FirstOrDefault(s => s.Type.Contains("nameidentifier")).Value, role.Role);

            if (!result.Success)
            {
                return Ok(Messages.FailedProccess);
            }

            JwtDto jwtDto = JwtDto.GetViewModel(jwtToken);

            return Ok(jwtDto);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var isValid = await _validator.ValidateAsync(userForRegisterDto);
            if (!isValid.IsValid)
            {
                return BadRequest(isValid.Errors);
            }
            var result = await _authService.Register(userForRegisterDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            //RabbitMQ Kullanýlacak
            await _publishEndpoint.Publish(
                        new UserRegisteredEvent
                        {
                            Email = result.Data.Email,
                            FullName = result.Data.UserName
                        }
                );
            return Ok(result.Data);
        }

    }
}
