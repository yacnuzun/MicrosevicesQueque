using AccountApi.Application.Services.Interfaces;
using AccountApi.Domain.Entities;
using AccountApi.Dto_s;
using AccountApi.Infrastructure.Repositories.Interfaces;
using Shared.Helpers.ResponseModels.GenericResultModels;
using Shared.Persistance.Interfaces;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace AccountApi.Application.Services.Implementations
{
    public class EmailTemplateManager : ITemplateMailService
    {
        private readonly IEfMailTemplateDal _efMailTemplateDal;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EmailTemplateManager> _logger;

        public EmailTemplateManager(IEfMailTemplateDal efMailTemplateDal, 
            IUnitOfWork unitOfWork, 
            ILogger<EmailTemplateManager> logger)
        {
            _efMailTemplateDal = efMailTemplateDal;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult> Add(TemplateAddDto emailTemplate)
        {
            try
            {
                var entity = new EmailTemplate
                {
                    Body = emailTemplate.Body,
                    IsActive = emailTemplate.IsActive,
                    Subject = emailTemplate.Subject,
                    TemplateCode = emailTemplate.TemplateCode.ToString()
                };

                await _efMailTemplateDal.AddAsync(entity);

                var result = await _unitOfWork.CommitAsync();

                if (result <= 0)
                {
                    return new ErrorResult();
                }

                return new SuccesResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

        public async Task<IDataResult<EmailTemplate>> GetMailTemplate(string mailCode)
        {
            var result = await _efMailTemplateDal.GetAsync(m => m.TemplateCode == mailCode);
            if (result == null)
            {
                return new ErrorDataResult<EmailTemplate>();
            }
            return new SuccessDataResult<EmailTemplate>(result);
        }

        public async Task<IResult> Update(TemplateUpdateDto emailTemplate)
        {
            try
            {
                var entity = new EmailTemplate
                {
                    Id = emailTemplate.TemplateId,
                    Body = emailTemplate.Body,
                    IsActive = emailTemplate.IsActive,
                    Subject = emailTemplate.Subject,
                    TemplateCode = emailTemplate.TemplateCode.ToString()
                };
                _efMailTemplateDal.Update(entity);

                var result = await _unitOfWork.CommitAsync();

                if (result <= 0)
                {
                    return new ErrorResult();
                }
                return new SuccesResult();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
