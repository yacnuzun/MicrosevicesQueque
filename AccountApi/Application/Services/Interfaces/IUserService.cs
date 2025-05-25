using AccountApi.Domain.Entities;
using Shared.Helpers.ResponseModels.GenericResultModels;

namespace AccountApi.Application.Services.Interfaces
{
    public interface IUserService
    {
        void Add(User user);
        Task<IDataResult<List<User>>> GetAll();
        Task<IDataResult<User>> GetById(int id);
        Task<IDataResult<List<OperationClaim>>> GetClaims(User user);
        Task<IDataResult<User>> GetByUserTaxId(string userTaxId);
    }
}
