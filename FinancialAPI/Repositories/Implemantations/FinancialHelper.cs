using FinancialAPI.Entities.DbConnectionContext;
using FinancialAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Helpers.ResponseModels.GenericResultModels;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace FinancialAPI.Repositories.Implemantations
{
    public class FinancialHelper : IFinancialHelper
    {
        public async Task<IResult> EarlypPaymentRequest(string invoiceNumber)
        {
            using (var context = new FinancialDbContext())
            {
                var entity = context.Bills.FirstOrDefault(s => s.InvoiceNumber == invoiceNumber);

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
