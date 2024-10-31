using Shared.DTO_s;
using Shared.Events;
using Shared.Helpers.ResponseModels.GenericResultModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Repositories.Interfaces
{
    public interface IBuyerHelper
    {
        public Task<BillEvent> CreateABill(CreateBillDTO dto);
        public Task<IDataResult<List<BillListingDTO>>> GetBills(string buyerTaxId);
    }
}
