using Shared.Entities;
using Shared.Helpers.ResponseModels.GenericResultModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Repositories.Interfaces
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
