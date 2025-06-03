using BillApi.Bussiness.Interfaces;
using BillApi.Dto_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Constant;

namespace BillApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillController : ControllerBase
    {
        readonly IBillService _billService;

        public BillController(IBillService billService)
        {
            _billService = billService;
        }

        [HttpPost("createabill")]
        [Authorize]
        public async Task<IActionResult> CreateABill(CreateBillDTO dto)
        {
            var result = await _billService.CreateABill(dto);

            return Ok(result.Data);
        }
        
        [HttpPost("paymentarequest")]
        [Authorize]
        public async Task<IActionResult> PaymentRequest(PaymentRequestControllerDto dto)
        {
            var result = await _billService.CreatePaymentRequest(dto.InvoiceNumber);

            return Ok(result.Data);
        }
        
        [HttpGet("paymentresponse")]
        public async Task<IActionResult> PaymentResponse(string invoiceNumber)
        {
            var result = await _billService.GetPaymentResponse(invoiceNumber);

            return Ok(result.Success);
        }
        
        [HttpGet("getbillbuyer")]
        [Authorize]
        public async Task<IActionResult> GetBillBuyer(string buyerTaxId)
        {
            var result = await _billService.GetBillDtowithBuyerID(buyerTaxId);

            return Ok(result.Data);
        }

        [HttpGet("getbillsupplier")]
        [Authorize]
        public async Task<IActionResult> GetBillSupplier(string supplierTaxId)
        {
            var result = await _billService.GetBillDtowithSupplierID(supplierTaxId);

            return Ok(result.Data);
        }
    }
}
