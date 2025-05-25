using AccountApi.Application.Services.Interfaces;
using AccountApi.Domain.Entities;
using AccountApi.Infrastructure.Repositories.Interfaces;
using Shared.Helpers.ResponseModels.GenericResultModels;
using Shared.Persistance.Interfaces;

namespace AccountApi.Application.Services.Implementations
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserManager> _logger;

        public UserManager(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            ILogger<UserManager> logger)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
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
        public async void Add(User user)
        {
            try
            {
                await _userRepository.AddAsync(user);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
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

    }
}
