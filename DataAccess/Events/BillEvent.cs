using Shared.Abstract;
using Shared.Dto_s;

namespace Shared.Events
{
    public partial class BillEvent : IEvent
    {
        public string InovoiceNumber { get; set; }
        public string TermDate { get; set; }
        public string BuyerTaxID { get; set; }
        public string SuplierTaxID { get; set; }
        public decimal InvoiceCost { get; set; }
        public Status InvoiceStatus { get; set; }

        public static BillEvent GetViewModel(BillResponseDto billResponseDto)
        {
            return new BillEvent
            {
                BuyerTaxID = billResponseDto.BuyerTaxID,
                InovoiceNumber = billResponseDto.InvoiceNumber,
                InvoiceCost = billResponseDto.InvoiceCost,
                InvoiceStatus = billResponseDto.InovoiceStatus,
                SuplierTaxID = billResponseDto.SuplierTaxID,
                TermDate = billResponseDto.TermDate
            };
        }
    }

}
