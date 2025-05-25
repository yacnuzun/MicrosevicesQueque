using AccountApi.Domain.Entities;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace AccountApi.Application.Services.Interfaces
{
    public interface IUserOperationClaimService
    {
        Task<IResult> Add(UserOperationClaim userOperationClaim);
    }
}
