using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class FeeSharedExemptionInfo
    {
        public FeeSharedExemption FeeSharedExemption { get; set; }
        public FeeItem FeeItem { get; set; }
    }
}