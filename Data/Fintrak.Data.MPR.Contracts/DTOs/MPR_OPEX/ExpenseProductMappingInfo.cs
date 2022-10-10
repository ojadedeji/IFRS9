using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class ExpenseProductMappingInfo
    {
        public ExpenseProductMapping ExpenseProductMapping { get; set; }
        public Product Product { get; set; }
        public ExpenseBasis ExpenseBasis { get; set; }

    }
}