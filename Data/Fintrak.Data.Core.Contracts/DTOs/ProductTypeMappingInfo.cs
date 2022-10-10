using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core.Contracts
{
    public class ProductTypeMappingInfo
    {
        public ProductTypeMapping ProductTypeMapping { get; set; }
        public Product Product { get; set; }
        public ProductType ProductType { get; set; }
    }
}