using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class IFRSRegistryInfo
    {
        public IFRSRegistry IFRSRegistry { get; set; }
        public FinancialType FinType { get; set; }
        public FinancialType FinSubType { get; set; }
        public IFRSRegistry Parent { get; set; }
    }
}