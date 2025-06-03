using AccountApi.Domain.Entities;
using AccountApi.Infrastructure.Data;
using AccountApi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Persistance.Implamantations;

namespace AccountApi.Infrastructure.Repositories.Implemantations
{
    public class UserRepository : EfRepository<User, AccountDbContext>, IUserRepository
    {
        public UserRepository(AccountDbContext context) : base(context)
        {
        }
        public async Task<List<OperationClaim>> GetClaims(User user)
        {
            var result = from operationClaim in Context.OperationClaims
                         join userOperationClaim in Context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                         where userOperationClaim.UserId == user.Id
                         select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
            return await result.ToListAsync();

        }
    }
}
