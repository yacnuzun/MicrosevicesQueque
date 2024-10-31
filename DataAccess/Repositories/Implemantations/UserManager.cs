using Microsoft.EntityFrameworkCore;
using Shared.Entities;
using Shared.Entities.DbConnectionContext;
using Shared.Helpers.ResponseModels.GenericResultModels;
using Shared.Repositories.Interfaces;

namespace Shared.Repositories.Implemantations
{
    public class UserManager : IUserService
    {
        public UserManager()
        {
        }

        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new SupplyChainDbContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }
        public void Add(User user)
        {
            using (var context = new SupplyChainDbContext())
            {
                var addedEntity = context.Entry(user);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }


        }
        public IDataResult<List<User>> GetAll()
        {
            using (var context = new SupplyChainDbContext())
            {
                return new SuccessDataResult<List<User>>(context.Set<User>().ToList());
            }
        }

        public IDataResult<User> GetById(int id)
        {
            using (var context = new SupplyChainDbContext())
            {
                return new SuccessDataResult<User>(context.Set<User>().SingleOrDefault(u => u.Id == id));
            }
        }

        public User GetByUserTaxId(string userTaxId)
        {
            using (var context = new SupplyChainDbContext())
            {
                return context.Set<User>().SingleOrDefault(u => u.UserTaxID == userTaxId);
            }
        }

    }
}
