using AccountApi.Domain.Entities;
using AccountApi.Infrastructure.Data;
using AccountApi.Infrastructure.Repositories.Interfaces;
using Shared.Persistance.Implamantations;

namespace AccountApi.Infrastructure.Repositories.Implemantations
{
    public class UserOperationClaimRepository : EfRepository<UserOperationClaim, AccountDbContext>, IUserOperationClaimRepository
    {
        public UserOperationClaimRepository(AccountDbContext context) : base(context)
        {
        }
    }
}
