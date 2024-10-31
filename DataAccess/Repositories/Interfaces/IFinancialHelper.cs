using Shared.Helpers.ResponseModels.GenericResultModels;

namespace Shared.Repositories.Interfaces
{
    public interface IFinancialHelper
    {
        public Task<IResult> EarlypPaymentRequest(string invoiceNumber);
    }
}
