using BuyerAPI.Entities;
using Shared.Abstract;

namespace BuyerAPI.Events
{
    public partial class BillEvent : IEvent
    {
        public string InovoiceNumber { get; set; }
        public string TermDate { get; set; }
        public string BuyerTaxID { get; set; }
        public string SuplierTaxID { get; set; }
        public decimal InvoiceCost { get; set; }
        public Status InvoiceStatus { get; set; }
        public static BillEvent GetViewModel(Bill model)
        {
            return new BillEvent
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
