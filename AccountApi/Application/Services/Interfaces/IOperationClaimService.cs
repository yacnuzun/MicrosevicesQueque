using AccountApi.Dto_s;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace AccountApi.Application.Services.Interfaces
{
    public interface IOperationClaimService
    {
        Task<IResult> Add(ClaimDto operationClaim);
        Task<IResult> GetOperation(string operation);
    }
}
