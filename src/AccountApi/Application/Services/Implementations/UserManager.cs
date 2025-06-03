using AccountApi.Application.Services.Interfaces;
using AccountApi.Domain.Entities;
using AccountApi.Domain.Enums;
using AccountApi.Infrastructure.Repositories.Interfaces;
using Shared.Helpers.ResponseModels.GenericResultModels;
using Shared.Persistance.Interfaces;

namespace AccountApi.Application.Services.Implementations
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserOperationClaimService _userOperationClaimService;
        private readonly IOperationClaimService _operationClaim;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserManager> _logger;

        public UserManager(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            ILogger<UserManager> logger,
            IUserOperationClaimService userOperationClaimService,
            IOperationClaimService operationClaim)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userOperationClaimService = userOperationClaimService;
            _operationClaim = operationClaim;
        }

        public async Task<IDataResult<List<OperationClaim>>> GetClaims(User user)
        {
            try
            {
                var result = await _userRepository.GetClaims(user);
                if (result == null)
                {
                    return new ErrorDataResult<List<OperationClaim>>();
                }
                return new SuccessDataResult<List<OperationClaim>>(result);
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.InnerException}/{ex.Message}/{ex.Source}");
                throw;
            }
        }
        public async void Add(User user, UserRoles role)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await _userRepository.AddAsync(user);

                await _unitOfWork.CommitAsync();

                var resultOperation = await _operationClaim.GetOperation(role.ToString());

                if (!resultOperation.Success || resultOperation.Data == null)
                {
                    return;
                }

                var addResult = await _userOperationClaimService.AddWithoutCommitAsync(new UserOperationClaim { OperationClaimId = resultOperation.Data.Id, UserId = user.Id });


                if (!addResult.Success || addResult == null) return;

                await _unitOfWork.CommitAsync();

                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError($"{ex.InnerException}/{ex.Message}/{ex.Source}");
                throw;
            }

        }
        public async Task<IDataResult<List<User>>> GetAll()
        {
            try
            {
                var result = await _userRepository.ListAsync();
                if (result == null)
                {
                    return new ErrorDataResult<List<User>>();
                }
                return new SuccessDataResult<List<User>>(result.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.InnerException}/{ex.Message}/{ex.Source}");
                throw;
            }

        }

        public async Task<IDataResult<User>> GetById(int id)
        {
            try
            {
                var result = await _userRepository.GetAsync(u => u.Id == id && u.Status == true);
                if (result == null)
                {
                    return new ErrorDataResult<User>();
                }
                return new SuccessDataResult<User>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.InnerException}/{ex.Message}/{ex.Source}");
                throw;
            }
        }

        public async Task<IDataResult<User>> GetByUserTaxId(string userTaxId)
        {
            try
            {
                var result = await _userRepository.GetAsync(u => u.UserTaxID == userTaxId && u.Status == true);
                if (result == null)
                {
                    return new ErrorDataResult<User>();
                }
                return new SuccessDataResult<User>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.InnerException}/{ex.Message}/{ex.Source}");
                throw;
            }


        }

        public async Task<IDataResult<User>> GetExistUser(string userTaxId, string email, string userName)
        {
            try
            {
                var entity = await _userRepository.GetAsync(u => u.UserTaxID == userTaxId || u.Email == email || u.UserName == userName);
                if (entity == null)
                {
                    return new ErrorDataResult<User>();
                }
                return new SuccessDataResult<User>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
    }
}
