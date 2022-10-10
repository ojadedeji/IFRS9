using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core.Contracts
{
    public class FinancialTypeInfo
    {
        public FinancialType FinancialType { get; set; }
        public FinancialType Parent { get; set; }
    }
}