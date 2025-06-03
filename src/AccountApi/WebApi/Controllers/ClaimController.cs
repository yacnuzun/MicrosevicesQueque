using AccountApi.Application.Services.Interfaces;
using AccountApi.Domain.Enums;
using AccountApi.Dto_s;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountApi.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClaimController : ControllerBase
    {
        private readonly IOperationClaimService _operationClaimService;
        private readonly IValidator<ClaimDto> _validator;
        public ClaimController(IOperationClaimService operationClaimService, IValidator<ClaimDto> validator)
        {
            _operationClaimService = operationClaimService;
            _validator = validator;
        }

        //[Authorize(Roles = UserRolesConst.Admin)]
        [HttpPost("addoperationclaim")]
        public async Task<IActionResult> AddOperationClaim(ClaimDto operationClaim)
        {
            var isValidate = await _validator.ValidateAsync(operationClaim);
            if (!isValidate.IsValid)
            {
                return BadRequest(isValidate.Errors);
            }
            
            var result = await _operationClaimService.Add(operationClaim);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}
