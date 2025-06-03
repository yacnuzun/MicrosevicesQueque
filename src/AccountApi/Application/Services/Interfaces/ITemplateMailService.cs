using AccountApi.Domain.Entities;
using AccountApi.Dto_s;
using Shared.Helpers.ResponseModels.GenericResultModels;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;
namespace AccountApi.Application.Services.Interfaces
{
    public interface ITemplateMailService
    {
        Task<IResult> Add(TemplateAddDto emailTemplate);
        Task<IResult> Update(TemplateUpdateDto emailTemplate);
        Task<IDataResult<EmailTemplate>> GetMailTemplate(string mailCode);
    }
}
