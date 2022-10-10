using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class ProductInfo
    {
        public Product Product { get; set; }
        public ProductGroup ProductGroup { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public ProductClassification ProductClassification { get; set; }
        public RevenueCaption RevenueCaption { get; set; }
    }
}