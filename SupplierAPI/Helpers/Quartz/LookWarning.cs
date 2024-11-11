using Microsoft.EntityFrameworkCore;
using Quartz;
using SupplierAPI.Entities.DbConectionContext;

namespace SupplierAPI.Helpers.Quartz
{
    public class LookWarning : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            using (var dbContext = new SuplierDbContext())
            {
                var list = dbContext.QueueMessages.ToList();
                foreach (var item in list.Where(l=> l.IsRead is not true).ToList())
                {
                    Console.WriteLine($"{item.TermDate} tarihinde {item.BuyerTaxID} taxid numaralı firma tarafından {item.InvoiceNumber} numaralı fatura oluşturulmuştur.");

                    item.IsRead = true;

                    var updatedEntity = dbContext.Entry(item);
                    updatedEntity.State = EntityState.Modified;
                    dbContext.SaveChanges();
                    
                }
            }

        }
    }
}
