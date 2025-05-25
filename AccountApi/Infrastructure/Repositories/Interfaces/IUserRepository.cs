using AccountApi.Domain.Entities;
using Shared.Persistance.Interfaces;

namespace AccountApi.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<OperationClaim>> GetClaims(User user);
    }
}
