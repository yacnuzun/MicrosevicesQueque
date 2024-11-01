using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO_s;
using Shared.Entities;
using Shared.Entities.DbConnectionContext;
using Shared.Events;
using Shared.Helpers.Security.JWT;
using Shared.Repositories.Interfaces;
using SupplierAPI.Helpers.Quartz;

namespace SupplierAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupplierController : ControllerBase
    {
        readonly ISupplierHelper _suplierHelper;
        readonly IAuthService _authService;
        readonly ITokenHelper _tokenHelper;
        private static HttpClient client = new HttpClient();
        readonly string role = nameof(Supplier);
        private readonly IBus _bus;

        public SupplierController(ISupplierHelper suplierHelper, IAuthService authService, ITokenHelper tokenHelper, IBus bus)
        {
            _suplierHelper = suplierHelper;
            _authService = authService;
            _tokenHelper = tokenHelper;
            _bus = bus;
        }

        
        [HttpPost("login")]
        public async Task<ActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto, role);
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


        [HttpPost("paymentrequest")]
        [Authorize]
        public async Task<bool> EarlypPaymentRequest(string invoice)
        {
            var request = await _suplierHelper.CreateAEarlyTask(invoice);

            if (!request.Success)
            {
                return false;
            }

            _bus.Publish(request.Data);

            return true;
        }

        [HttpPost("listingBills")]
        [Authorize]
        public async Task<IActionResult> ListingBills()
        {
            var jwtToken = _tokenHelper.ValidateTokenGetClaims(HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ",""));

            var token = jwtToken.Claims.FirstOrDefault(s => s.Type.Contains("nameidentifier")).Value;

            var request = await _suplierHelper.GetBillswithSupplier(token);

            if (request == null)
            {
                return Ok(request.Message);
            }

            return Ok(request.Data);
        }
    }
}
