using BillApi.Entities;
using BillApi.Entities.DbConnectionContext;
using BillApi.Repositories.Interfaces;
using Shared.Persistance.Implamantations;
using Shared.Persistance.Interfaces;

namespace BillApi.Repositories.Implemantations
{
    public class EfBillRepository : EfRepository<Bill, BillDbContext>, IBillRepository
    {
        public EfBillRepository(BillDbContext context) : base(context)
        {
        }
    }
}
