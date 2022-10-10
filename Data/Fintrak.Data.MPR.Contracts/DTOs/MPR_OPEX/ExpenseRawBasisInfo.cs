using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class ExpenseRawBasisInfo
    {
        public ExpenseRawBasis ExpenseRawBasis { get; set; }
        public ExpenseBasis ExpenseBasis  { get; set; }
        public CostCentre CostCentre { get; set; }
    }
}