using Shared.Abstract;

namespace Shared.Dto_s
{
    public class BillResponseDto:IDTO
    {
        public string TermDate { get; set; }
        public decimal InvoiceCost { get; set; }
        public string BuyerTaxID { get; set; }
        public string InvoiceNumber { get; set; }
        public string SuplierTaxID { get; set; }
        public Status InovoiceStatus { get; set; }
    }
}
