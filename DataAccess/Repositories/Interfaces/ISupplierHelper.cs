using Shared.DTO_s;
using Shared.Events;
using Shared.Helpers.ResponseModels.GenericResultModels;

namespace Shared.Repositories.Interfaces
{
    public interface ISupplierHelper
    {
        public Task<IDataResult<EarlyPaymentEvent>> CreateAEarlyTask(string invoiceNumber);
        public Task<IDataResult<List<BillListingDTO>>> GetBillswithSupplier(string supplierTaxId);
    }
}
