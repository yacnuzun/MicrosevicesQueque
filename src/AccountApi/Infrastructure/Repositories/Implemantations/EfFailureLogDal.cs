using AccountApi.Domain.Entities;
using AccountApi.Infrastructure.Data;
using AccountApi.Infrastructure.Repositories.Interfaces;
using Shared.Persistance.Implamantations;

namespace AccountApi.Infrastructure.Repositories.Implemantations
{
    public class EfFailureLogDal : EfRepository<FailureLog, AccountDbContext>, IEfFailureLogDal
    {
        public EfFailureLogDal(AccountDbContext context) : base(context)
        {
        }
    }
}
