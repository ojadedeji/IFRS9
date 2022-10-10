using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class OpexAbcExemptionInfo
    {
        public OpexAbcExemption OpexAbcExemption { get; set; }
        public CostCentre CostCentre { get; set; }
    }
}