using Shared.Dto_s;
using Shared.Helpers.ResponseModels.GenericResultModels;
using SupplierAPI.Dto_s;
using SupplierAPI.Events;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace SupplierAPI.Repositories.Interfaces
{
    public interface ISupplierHelper
    {
        public Task<IDataResult<EarlyPaymentEvent>> CreateAEarlyTask(string invoiceNumber);
        public Task<IDataResult<JwtDto>> CheckUser(string token);
        public Task<IDataResult<List<BillListingDTO>>> GetBillswithSupplier(string supplierTaxId);
    }
}
