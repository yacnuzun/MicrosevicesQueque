using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Quartz;
using Shared.Entities;
using Shared.Entities.DbConnectionContext;
using SupplierAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SupplierAPI.Helpers.Quartz
{
    public class LookWarning : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            using (var dbContext = new SupplyChainDbContext())
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
