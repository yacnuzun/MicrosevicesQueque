using System.Text.Json.Serialization;
using Shared.Abstract;
using Shared.Entities;

namespace Shared.Helpers.ResponseModels.CustomResponseModels
{
    public partial class BillEvent
    {
        public class BillResponse : IEvent
        {
            [JsonPropertyName("invoiceNumber")]
            public string InovoiceNumber { get; set; }
            [JsonPropertyName("termDate")]
            public string TermDate { get; set; }
            [JsonPropertyName("buyerTaxId")]
            public string BuyerTaxID { get; set; }
            [JsonPropertyName("supplierTaxId")]
            public string SuplierTaxID { get; set; }
            [JsonPropertyName("invoiceCost")]
            public decimal InvoiceCost { get; set; }
            public static BillResponse GetViewModel(Bill model)
            {
                return new BillResponse
                {
                    TermDate = model.TermDate,
                    InvoiceCost = model.InvoiceCost,
                    BuyerTaxID = model.BuyerTaxID,
                    InovoiceNumber = model.InvoiceNumber,
                    SuplierTaxID = model.SuplierTaxID
                };
            }
        }
    }
}
