﻿using Shared.Persistance.Entities;

namespace SupplierAPI.Entities
{
    public class Supplier : IEntity
    {
        public int SupplierID { get; set; }
        public string TaxID { get; set; }
        public string SupplierName { get; set; }
    }
}
