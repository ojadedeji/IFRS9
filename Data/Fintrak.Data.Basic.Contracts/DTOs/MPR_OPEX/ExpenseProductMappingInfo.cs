using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class ExpenseProductMappingInfo
    {
        public ExpenseProductMapping ExpenseProductMapping { get; set; }
        public Product Product { get; set; }
        public ExpenseBasis ExpenseBasis { get; set; }

    }
}