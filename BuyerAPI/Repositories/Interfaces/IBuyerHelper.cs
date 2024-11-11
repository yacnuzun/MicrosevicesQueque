using BuyerAPI.Dto_s;
using BuyerAPI.Events;
using Shared.Dto_s;
using Shared.Helpers.ResponseModels.GenericResultModels;
using System.IdentityModel.Tokens.Jwt;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace BuyerAPI.Repositories.Interfaces
{
    public interface IBuyerHelper
    {
        public Task<BillEvent> CreateABill(CreateBillDTO dto);
        public Task<IDataResult<JwtDto>> CheckUser(string token);
        public Task<IDataResult<List<BillListingDTO>>> GetBills(string buyerTaxId);
    }
}
