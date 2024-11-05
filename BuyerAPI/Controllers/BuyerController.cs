using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Constant;
using Shared.DTO_s;
using Shared.Entities;
using Shared.Events;
using Shared.Helpers;
using Shared.Helpers.Security.JWT;
using Shared.Repositories.Interfaces;
using System.Data;
using System.Security.Claims;
using System.Text.Json;

namespace BuyerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuyerController : ControllerBase
    {
        readonly IBuyerHelper _buyerHelper;
        readonly IBus _publishEndpoint;
        readonly IAuthService _authService;
        readonly ITokenHelper _tokenHelper;
        readonly string role = nameof(Buyer);

        public BuyerController(IBuyerHelper buyerHelper,
            IBus publishEndpoint,
            IAuthService authService
,
            ITokenHelper tokenHelper)
        {
            _buyerHelper = buyerHelper;
            _publishEndpoint = publishEndpoint;
            _authService = authService;
            _tokenHelper = tokenHelper;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto,role);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Data);
        }

        

        [HttpPost("createabill")]
        [Authorize]
        public async Task<bool> CreateABill(CreateBillDTO dto)
        {
            var response = await _buyerHelper.CreateABill(dto);

            await _publishEndpoint.Publish(response);

            return await Task.FromResult(true);
        }

        [HttpGet("getbill")]
        [Authorize]
        public async Task<IActionResult> GetBills()
        {

            var jwtToken = _tokenHelper.ValidateTokenGetClaims(HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ",""));

            var token = jwtToken.Claims.FirstOrDefault(s => s.Type.Contains("nameidentifier")).Value;

            var list = await _buyerHelper.GetBills(token);

            if (!list.Success)
            {
                return Ok(list.Message);
            }

            return Ok(list.Data);
        }
    }
}
