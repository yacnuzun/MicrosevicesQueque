using AccountApi.Application.Services.Interfaces;
using AccountApi.Domain.Entities;
using AccountApi.Domain.Enums;
using AccountApi.Dto_s;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountApi.WebApi.Controllers
{
    public class MailTemplateController : ControllerBase
    {
        private readonly ITemplateMailService _templateMailService;
        private readonly IValidator<TemplateAddDto> _validatorAdd;
        private readonly IValidator<TemplateUpdateDto> _validatorUpdate;

        public MailTemplateController(ITemplateMailService templateMailService, 
            IValidator<TemplateAddDto> validatorAdd, 
            IValidator<TemplateUpdateDto> validatorUpdate)
        {
            _templateMailService = templateMailService;
            _validatorAdd = validatorAdd;
            _validatorUpdate = validatorUpdate;
        }
        [Authorize(Roles = UserRolesConst.Admin)]
        [HttpPost("templateadd")]
        public async Task<IActionResult> AddTemplate(TemplateAddDto dto)
        {
            var isValidResult = await _validatorAdd.ValidateAsync(dto);
            if (!isValidResult.IsValid)
            {
                return BadRequest(isValidResult.Errors);
            }
            var result = await _templateMailService.Add(dto);
            if (!result.Success) 
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [Authorize(Roles = UserRolesConst.Admin)]
        [HttpPost("templateUpdate")]
        public async Task<IActionResult> UpdateTemplate(TemplateUpdateDto dto)
        {
            var isValidResult = await _validatorUpdate.ValidateAsync(dto);
            if (!isValidResult.IsValid)
            {
                return BadRequest(isValidResult.Errors);
            }
            var result = await _templateMailService.Update(dto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}
