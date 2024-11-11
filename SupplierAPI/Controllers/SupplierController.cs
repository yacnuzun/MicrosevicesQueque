using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SupplierAPI.Entities;
using SupplierAPI.Repositories.Interfaces;

namespace SupplierAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SupplierController : ControllerBase
    {
        readonly ISupplierHelper _suplierHelper;
        private static HttpClient client = new HttpClient();
        readonly string role = nameof(Supplier);
        private readonly IBus _bus;

        public SupplierController(ISupplierHelper suplierHelper, IBus bus)
        {
            _suplierHelper = suplierHelper;
            _bus = bus;
        }

        [HttpPost("paymentrequest")]
        [Authorize]
        public async Task<bool> EarlypPaymentRequest(string invoice)
        {
            //check to user claims
            var userRequest = await _suplierHelper.CheckUser(HttpContext.Request.Headers.Authorization.ToString());

            if (!userRequest.Success)
            {
                return false;
            }

            var request = await _suplierHelper.CreateAEarlyTask(invoice);

            if (!request.Success)
            {
                return false;
            }

            _bus.Publish(request.Data);

            return true;
        }

        [HttpGet("listingBills")]
        [Authorize]
        public async Task<IActionResult> ListingBills()
        {
            //check to user claims
            var userRequest = await _suplierHelper.CheckUser(HttpContext.Request.Headers.Authorization.ToString());

            if (!userRequest.Success)
            {
                return Ok(userRequest.Message);
            }

            #region bunun yerine üstte checkUser metodu yazýldý..
            //var jwtToken = _tokenHelper.ValidateTokenGetClaims(HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ",""));

            //var token = jwtToken.Claims.FirstOrDefault(s => s.Type.Contains("nameidentifier")).Value;
            #endregion

            var request = await _suplierHelper.GetBillswithSupplier(userRequest.Data.NameIdentifier);

            if (request == null)
            {
                return Ok(request.Message);
            }

            return Ok(request.Data);
        }
    }
}
