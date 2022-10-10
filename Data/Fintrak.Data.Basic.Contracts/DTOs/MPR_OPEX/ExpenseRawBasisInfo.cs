using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class ExpenseRawBasisInfo
    {
        public ExpenseRawBasis ExpenseRawBasis { get; set; }
        public ExpenseBasis ExpenseBasis  { get; set; }
        public CostCentre CostCentre { get; set; }
    }
}