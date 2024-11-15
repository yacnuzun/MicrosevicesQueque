using Shared.Abstract;
using Shared.Dto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static EarlyPaymentEvent GetViewModel(PaymentRequestDto model)
        {
            return new EarlyPaymentEvent
            {
                TermDate = model.TermDate,
                InvoiceCost = model.InvoiceCost,
                BuyerTaxID = model.BuyerTaxID,
                InovoiceNumber = model.InvoiceNumber,
                SuplierTaxID = model.SuplierTaxID,
                InvoiceStatus = model.InvoiceStatus
            };
        }
    }
}
