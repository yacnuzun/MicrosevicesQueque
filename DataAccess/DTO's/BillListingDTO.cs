using Shared.Constant;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO_s
{
    public class BillListingDTO
    {
        public string InvoiceNumber { get; set; }
        public string Buyer { get; set; }
        public string Supplier { get; set; }
        public Status Status { get; set; }
        public decimal InvoiceCost { get; set; }

        public static BillListingDTO GetViewModel(Bill model)
        {
            return new BillListingDTO
            {
                InvoiceNumber = model.InvoiceNumber,
                Buyer = model.BuyerTaxID,
                Supplier = model.SuplierTaxID,
                InvoiceCost = model.InvoiceCost,
                Status = model.InovoiceStatus
            };
        }
    }
}
