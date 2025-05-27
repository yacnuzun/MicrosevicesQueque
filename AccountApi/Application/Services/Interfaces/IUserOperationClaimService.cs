using AccountApi.Domain.Entities;
using AccountApi.Domain.Enums;
using Shared.Helpers.ResponseModels.GenericResultModels;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace AccountApi.Application.Services.Interfaces
{
    public interface IUserOperationClaimService
    {
        Task<IResult> AddAsync(UserOperationClaim userOperationClaim);
        Task<IResult> AddWithoutCommitAsync(UserOperationClaim userOperationClaim);
    }
}
