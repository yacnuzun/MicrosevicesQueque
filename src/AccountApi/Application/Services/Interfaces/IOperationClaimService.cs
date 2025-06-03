using AccountApi.Domain.Entities;
using AccountApi.Dto_s;
using Shared.Helpers.ResponseModels.GenericResultModels;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace AccountApi.Application.Services.Interfaces
{
    public interface IOperationClaimService
    {
        Task<IResult> Add(ClaimDto operationClaim);
        Task<IDataResult<OperationClaim>> GetOperation(string operation);
    }
}
