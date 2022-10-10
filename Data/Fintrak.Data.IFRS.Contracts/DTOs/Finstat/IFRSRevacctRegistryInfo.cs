using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.IFRS.Entities;

namespace Fintrak.Data.IFRS.Contracts
{
    public class IFRSRevacctRegistryInfo
    {
        public IFRSRevacctRegistry IFRSRevacctRegistry { get; set; }
        public FinancialType FinType { get; set; }
        public FinancialType FinSubType { get; set; }
        public IFRSRevacctRegistry Parent { get; set; }
    }
}