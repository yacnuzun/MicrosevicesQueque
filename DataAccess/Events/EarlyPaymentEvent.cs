using Shared.Abstract;
using Shared.Entities;

namespace Shared.Events
{
    public partial class EarlyPaymentEvent : IEvent
    {
        public string InovoiceNumber { get; set; }
        public string TermDate { get; set; }
        public string BuyerTaxID { get; set; }
        public string SuplierTaxID { get; set; }
        public decimal InvoiceCost { get; set; }
        public Status InvoiceStatus { get; set; }
        public static EarlyPaymentEvent GetViewModel(Bill model)
        {
            return new EarlyPaymentEvent
            {
                TermDate = model.TermDate,
                InvoiceCost = model.InvoiceCost,
                BuyerTaxID = model.BuyerTaxID,
                InovoiceNumber = model.InvoiceNumber,
                SuplierTaxID = model.SuplierTaxID,
                InvoiceStatus = model.InovoiceStatus
            };
        }
    }
}