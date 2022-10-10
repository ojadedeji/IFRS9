using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class ProductGroupInfo
    {
        public ProductGroup ProductGroup { get; set; }
        public ProductGroup Parent { get; set; }
    }
}