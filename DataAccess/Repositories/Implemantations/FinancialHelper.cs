using Microsoft.EntityFrameworkCore;
using Shared.Constant;
using Shared.Entities.DbConnectionContext;
using Shared.Helpers.ResponseModels.GenericResultModels;
using Shared.Interfaces;
using Shared.Repositories.Interfaces;

namespace Shared.Repositories.Implemantations
{
    public class FinancialHelper : IFinancialHelper
    {
        public async Task<IResult> EarlypPaymentRequest(string invoiceNumber)
        {
            using(var context = new SupplyChainDbContext())
            {
                var entity = context.Bills.FirstOrDefault(s => s.InvoiceNumber == invoiceNumber) ;

                entity.InovoiceStatus = Entities.Status.Paid;

                if (entity != null)
                {
                    var updatedEntity = context.Entry(entity);
                    updatedEntity.State = EntityState.Modified;
                    context.SaveChanges();

                    return new SuccesResult();

                }
                return new ErrorResult();
            }

        }
    }
}
