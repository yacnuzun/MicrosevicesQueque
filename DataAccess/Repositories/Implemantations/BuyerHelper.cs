using Microsoft.EntityFrameworkCore;
using Shared.Constant;
using Shared.DTO_s;
using Shared.Entities;
using Shared.Entities.DbConnectionContext;
using Shared.Events;
using Shared.Helpers.ResponseModels.GenericResultModels;
using Shared.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Repositories.Implemantations
{
    public class BuyerHelper : IBuyerHelper
    {
        public async Task<BillEvent> CreateABill(CreateBillDTO dto)
        {
            Random random = new Random();

            int billID = random.Next(0, 100);
            int invoiceNum = random.Next(0, 100000);



            using (var context = new SupplyChainDbContext())
            {
                var newBill = new Bill
                {
                    BillID = billID,
                    BuyerTaxID = dto.BuyerTaxID,
                    SuplierTaxID = dto.SuplierTaxID,
                    InovoiceStatus = Status.New,
                    InvoiceCost = dto.InvoiceCost,
                    InvoiceNumber = invoiceNum.ToString(),
                    TermDate = string.IsNullOrEmpty(dto.TermDate)? dto.TermDate:DateTime.Now.ToShortDateString()
                };

                var addedEntity = context.Entry(newBill);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
                
                return await Task.FromResult(BillEvent.GetViewModel(newBill));

            }

        }

        public async Task<IDataResult<List<BillListingDTO>>> GetBills(string buyerTaxId)
        {
            using (var context = new SupplyChainDbContext())
            {
                var list = context.Bills.Where(b => b.BuyerTaxID == buyerTaxId).ToList();
                if (list != null)
                {
                    var dtoList = list.Select(BillListingDTO.GetViewModel).ToList();
                    return new SuccessDataResult<List<BillListingDTO>>(dtoList);

                }

                return new ErrorDataResult<List<BillListingDTO>>(Messages.FailedProccess);
            }
        }
    }
}
