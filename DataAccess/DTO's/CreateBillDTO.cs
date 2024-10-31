using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO_s
{
    public class CreateBillDTO
    {
        public string TermDate { get; set; }
        public string BuyerTaxID { get; set; }
        public string SuplierTaxID { get; set; }
        public decimal InvoiceCost { get; set; }
        public static CreateBillDTO GetViewModel(Bill model)
        {
            return new CreateBillDTO
            {
                TermDate = model.TermDate,
                InvoiceCost = model.InvoiceCost,
                BuyerTaxID = model.BuyerTaxID,
                SuplierTaxID = model.SuplierTaxID,
            };
        }
    }
}
