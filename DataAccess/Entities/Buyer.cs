using Shared.Interfaces;

namespace Shared.Entities
{
    public class Buyer : IEntity
    {
        public string BuyerID { get; set; }
        public string TaxID { get; set; }
        public string BuyerName { get; set; }
    }
}
