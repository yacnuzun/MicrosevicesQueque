using Shared.Helpers.ResponseModels.GenericResultModels;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;
using Shared.Persistance.Interfaces;
using AccountApi.Dto_s;
using AccountApi.Application.Services.Interfaces;
using AccountApi.Domain.Entities;
using AccountApi.Infrastructure.Repositories.Interfaces;

namespace AccountApi.Application.Services.Implementations
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OperationClaim> _logger;

        public OperationClaimManager(IOperationClaimRepository operationClaimRepository, IUnitOfWork unitOfWork, ILogger<OperationClaim> logger)
        {
            _operationClaimRepository = operationClaimRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult> Add(ClaimDto operationClaim)
        {
            try
            {
                var isController = await GetOperation(operationClaim.Role.ToString());
                if (isController.Success)
                {
                    return new ErrorResult();
                }
                var entityresult = new OperationClaim { Name = operationClaim.Role.ToString() };
                await _operationClaimRepository.AddAsync(entityresult);
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

        public async Task<IResult> GetOperation(string operation)
        {
            var result = await _operationClaimRepository.GetAsync(o => o.Name == operation);
            if (result == null)
            {
                return new ErrorResult();
            }
            return new SuccesResult();
        }
    }
}
