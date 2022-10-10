using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class ExpenseMappingInfo
    {
        public ExpenseMapping ExpenseMapping { get; set; }
        public ExpenseBasis ExpenseBasis { get; set; }
        public Team Team { get; set; }
      

    }
}