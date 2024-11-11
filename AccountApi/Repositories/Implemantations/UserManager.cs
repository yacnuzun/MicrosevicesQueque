using AccountApi.Entities;
using AccountApi.Entities.DbConnectionContext;
using AccountApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Helpers.ResponseModels.GenericResultModels;

namespace AccountApi.Repositories.Implemantations
{
    public class UserManager : IUserService
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new AccountDbContext())
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
            using (var context = new AccountDbContext())
            {
                var addedEntity = context.Entry(user);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }


        }
        public IDataResult<List<User>> GetAll()
        {
            using (var context = new AccountDbContext())
            {
                return new SuccessDataResult<List<User>>(context.Set<User>().ToList());
            }
        }

        public IDataResult<User> GetById(int id)
        {
            using (var context = new AccountDbContext())
            {
                return new SuccessDataResult<User>(context.Set<User>().SingleOrDefault(u => u.Id == id));
            }
        }

        public User GetByUserTaxId(string userTaxId)
        {
            using (var context = new AccountDbContext())
            {
                return context.Set<User>().SingleOrDefault(u => u.UserTaxID == userTaxId);
            }
        }

    }
}
