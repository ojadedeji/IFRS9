using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core.Contracts
{
    public class ChartOfAccountInfo
    {
        public ChartOfAccount ChartOfAccount { get; set; }
        public ChartOfAccount Parent { get; set; }
        public FinancialType FinancialType { get; set; }
    }
}