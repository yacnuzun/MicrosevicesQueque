using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class QueueMessage:IEntity
    {
        public int QueueMessageID { get; set; }
        public Guid? QueueGUID { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal InvoiceCost { get; set; }
        public string TermDate { get; set; }
        public string BuyerTaxID { get; set; }
        public string SuplierTaxID { get; set; }
        public bool IsRead { get; set; }
    }
}
