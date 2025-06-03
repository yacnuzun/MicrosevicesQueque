using AccountApi.Domain.Entities;
using AccountApi.Infrastructure.Data;
using AccountApi.Infrastructure.Repositories.Interfaces;
using Shared.Persistance.Implamantations;

namespace AccountApi.Infrastructure.Repositories.Implemantations
{
    public class OperationClaimRepository : EfRepository<OperationClaim, AccountDbContext>, IOperationClaimRepository
    {
        public OperationClaimRepository(AccountDbContext context) : base(context)
        {
        }
    }
}
