using AccountApi.Application.Services.Interfaces;
using AccountApi.Domain.Entities;
using AccountApi.Dto_s;
using AccountApi.Infrastructure.Repositories.Interfaces;
using Shared.Helpers.ResponseModels.GenericResultModels;
using Shared.Persistance.Interfaces;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace AccountApi.Application.Services.Implementations
{
    public class FailureLogManager : IFailureLogService
    {
        private readonly IEfFailureLogDal _failureLogDal;
        private readonly ILogger<FailureLogManager> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public FailureLogManager(IEfFailureLogDal failureLogDal, ILogger<FailureLogManager> logger, IUnitOfWork unitOfWork)
        {
            _failureLogDal = failureLogDal;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> ExistFailure(string mail)
        {
             var result = await _failureLogDal.GetAsync(f=>f.Email == mail);
            if (result != null)
            {
                return new SuccesResult();
            }
            return new ErrorResult();
        }

        public async Task LogFailureAsync(FailureLogDto dto)
        {
            try
            {
                var entity = new FailureLog
                {
                    FailedConstrait = dto.FailedConstrait,
                    Email = dto.Email,
                    FailedAt = dto.FailedAt,
                    FullName = dto.FullName,
                    Reason = dto.Reason,
                };
                await _failureLogDal.AddAsync(entity);

                var result = await _unitOfWork.CommitAsync();
                if (result <= 0)
                {
                    _logger.LogError("Log failure eklenemedi");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
