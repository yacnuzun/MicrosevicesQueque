using BillApi.Dto_s;
using BillApi.Entities;
using BillApi.Entities.DbConnectionContext;
using BillApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Constant;
using Shared.Helpers.ResponseModels.GenericResultModels;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace BillApi.Repositories.Implemantations
{
    public class BillManager : IBillService
    {
        public async Task<IDataResult<BillResponseDto>> CreateABill(CreateBillDTO dto)
        {
            using (var context = new BillDbContext())
            {
                Random random = new Random();

                int billID = random.Next(0, 100);
                int invoiceNum = random.Next(0, 100000);

                var newBill = new Bill
                {
                    BillID = billID,
                    BuyerTaxID = dto.BuyerTaxID,
                    SuplierTaxID = dto.SuplierTaxID,
                    InovoiceStatus = Status.New,
                    InvoiceCost = dto.InvoiceCost,
                    InvoiceNumber = invoiceNum.ToString(),
                    TermDate = string.IsNullOrEmpty(dto.TermDate) ? DateTime.Now.ToShortDateString() : dto.TermDate
                };

                var addedEntity = context.Entry(newBill);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();

                return new SuccessDataResult<BillResponseDto>(BillResponseDto.GetViewModel(newBill));

            }
        }

        public async Task<IDataResult<List<BillListingDTO>>> GetBillDtowithBuyerID(string buyerTaxId)
        {
            using (var context = new BillDbContext())
            {
                var list = context.Bills.Where(b => b.BuyerTaxID == buyerTaxId).ToList();
                if (list != null)
                {
                    var dtoList = list.Select(BillListingDTO.GetViewModel).ToList();
                    return new SuccessDataResult<List<BillListingDTO>>(dtoList);

                }

                return new ErrorDataResult<List<BillListingDTO>>();
            }
        }
        
        public async Task<IDataResult<List<BillListingDTO>>> GetBillDtowithSupplierID(string supplierTaxId)
        {
            using (var context = new BillDbContext())
            {
                var list = context.Bills.Where(b => b.SuplierTaxID == supplierTaxId).ToList();
                if (list != null)
                {
                    var dtoList = list.Select(BillListingDTO.GetViewModel).ToList();
                    return new SuccessDataResult<List<BillListingDTO>>(dtoList);

                }

                return new ErrorDataResult<List<BillListingDTO>>(Messages.FailedProccess);
            }
        }

        public async Task<IDataResult<PaymentRequestDto>> CreatePaymentRequest(string invoiceNumber)
        {
            using (var context = new BillDbContext())
            {
                var data = context.Bills.FirstOrDefault(b => b.InvoiceNumber == invoiceNumber);

                if (data == null)
                {
                    return new ErrorDataResult<PaymentRequestDto>(Messages.FailedProccess);
                }

                data.InovoiceStatus = Status.Usage;

                var updatedEntity = context.Entry(data);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();

                return new SuccessDataResult<PaymentRequestDto>(PaymentRequestDto.GetViewModel(data), Messages.SuccessProccess);
            }
        }

        public async Task<IResult> GetPaymentResponse(string invoiceNumber)
        {
            using (var context = new BillDbContext())
            {
                var entity = context.Bills.FirstOrDefault(s => s.InvoiceNumber == invoiceNumber);

                entity.InovoiceStatus = Status.Paid;

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
