using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace FinancialAPI.Repositories.Interfaces
{
    public interface IFinancialHelper
    {
        public Task<IResult> EarlypPaymentRequest(string invoiceNumber);
    }
}
