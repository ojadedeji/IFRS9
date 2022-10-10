using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class ProductGroupEntryInfo
    {
        public ProductGroupEntry ProductGroupEntry { get; set; }
        public ProductGroup ProductGroup { get; set; }
        public TeamDefinition TeamDefinition { get; set; }
        public Team Team { get; set; }
        public Currency Currency { get; set; }
    }
}