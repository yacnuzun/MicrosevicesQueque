using Microsoft.EntityFrameworkCore;
using Shared.Constant;
using Shared.DTO_s;
using Shared.Entities;
using Shared.Entities.DbConnectionContext;
using Shared.Events;
using Shared.Helpers.ResponseModels.GenericResultModels;
using Shared.Interfaces;
using Shared.Repositories.Interfaces;
using System.Net.Http.Json;

namespace Shared.Repositories.Implemantations
{
    public class SupplierHelper : ISupplierHelper
    {
        private static HttpClient client = new HttpClient();

        readonly IUserService _userService;

        public SupplierHelper(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IDataResult<EarlyPaymentEvent>> CreateAEarlyTask(string invoiceNumber)
        {
            using (var context = new SupplyChainDbContext())
            {
                var data = context.Bills.FirstOrDefault(b => b.InvoiceNumber == invoiceNumber) ;

                if (data == null)
                {
                    return new ErrorDataResult<EarlyPaymentEvent>(Messages.FailedProccess);
                }

                data.InovoiceStatus = Status.Usage;

                var updatedEntity = context.Entry(data);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();

                var request = await client.GetFromJsonAsync<bool>("https://localhost:7007/Financial/earlypaymentrequest?invoiceNumber="+invoiceNumber);

                if (!request)
                {
                    return new ErrorDataResult<EarlyPaymentEvent>(Messages.FailedProccess);
                }

                return new SuccessDataResult<EarlyPaymentEvent>(EarlyPaymentEvent.GetViewModel(data),Messages.SuccessProccess);
            }


            
        }

        public async Task<IDataResult<List<BillListingDTO>>> GetBillswithSupplier(string supplierTaxId)
        {
            var result = _userService.GetByUserTaxId(supplierTaxId);

            if (result is not null)
            {
                using (var context = new SupplyChainDbContext())
                {
                    var list = context.Bills.Where(s => s.SuplierTaxID == supplierTaxId).ToList();

                    return new SuccessDataResult<List<BillListingDTO>>(list.Select(BillListingDTO.GetViewModel).ToList(), Messages.SuccessProccess);

                }

            }
            return new ErrorDataResult<List<BillListingDTO>>(Messages.FailedProccess);


        }
    }
}
