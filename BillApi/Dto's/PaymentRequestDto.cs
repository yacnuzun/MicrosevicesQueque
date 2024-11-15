﻿using BillApi.Entities;
using Shared.Abstract;

namespace BillApi.Dto_s
{
    public class PaymentRequestDto:IDTO
    {
        public string InovoiceNumber { get; set; }
        public string TermDate { get; set; }
        public string BuyerTaxID { get; set; }
        public string SuplierTaxID { get; set; }
        public decimal InvoiceCost { get; set; }
        public Status InvoiceStatus { get; set; }
        
        public static PaymentRequestDto GetViewModel(Bill model)
        {
            return new PaymentRequestDto
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
