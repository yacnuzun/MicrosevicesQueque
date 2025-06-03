using AccountApi.Domain.Entities;
using AccountApi.Dto_s;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace AccountApi.Application.Services.Interfaces
{
    public interface IFailureLogService
    {
        Task LogFailureAsync(FailureLogDto dto);
        Task<IResult> ExistFailure(string mail);
    }
}
