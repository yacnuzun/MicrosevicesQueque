using AccountApi.Entities;
using Shared.Helpers.ResponseModels.GenericResultModels;

namespace AccountApi.Repositories.Interfaces
{
    public interface IUserService
    {
        public void Add(User user);
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(int id);
        List<OperationClaim> GetClaims(User user);
        User GetByUserTaxId(string userTaxId);
    }
}
