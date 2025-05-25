using Shared.Helpers.ResponseModels.GenericResultModels;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;
using Shared.Persistance.Interfaces;
using AccountApi.Application.Services.Interfaces;
using AccountApi.Domain.Entities;
using AccountApi.Infrastructure.Repositories.Interfaces;

namespace AccountApi.Application.Services.Implementations
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimRepository _userClaimRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserOperationClaim> _logger;

        public UserOperationClaimManager(IUserOperationClaimRepository userClaimRepository, IUnitOfWork unitOfWork, ILogger<UserOperationClaim> logger)
        {
            _userClaimRepository = userClaimRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult> Add(UserOperationClaim userOperationClaim)
        {
            try
            {
                await _userClaimRepository.AddAsync(userOperationClaim);
                var result = await _unitOfWork.CommitAsync();
                if (result < 0)
                {
                    return new ErrorResult();
                }
                return new SuccesResult();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);
                throw;
            }

        }
    }
}
