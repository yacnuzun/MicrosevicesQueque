using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class Bill:IEntity
    {
        public int BillID { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal InvoiceCost { get; set; }
        public string TermDate { get; set; }
        public string BuyerTaxID { get; set; }
        public string SuplierTaxID { get; set; }
        public Status InovoiceStatus { get; set; }
    }

    public enum Status
    {
        New ,
        Usage,
        Paid
    }
}
