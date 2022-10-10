using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class ExpenseMappingInfo
    {
        public ExpenseMapping ExpenseMapping { get; set; }
        public ExpenseBasis ExpenseBasis { get; set; }
        public Team Team { get; set; }
      

    }
}