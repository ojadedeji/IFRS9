using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class DepreciationRateInfo
    {
        public DepreciationRate DepreciationRate { get; set; }
        public CapexCategory CapexCategory { get; set; }
    }
}