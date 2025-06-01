using AccountApi.Domain.Entities;
using Shared.Persistance.Interfaces;

namespace AccountApi.Infrastructure.Repositories.Interfaces
{
    public interface IEfFailureLogDal : IRepository<FailureLog>
    {
    }
}
