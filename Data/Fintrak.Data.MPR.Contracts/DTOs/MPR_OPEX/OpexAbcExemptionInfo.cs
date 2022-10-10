using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class OpexAbcExemptionInfo
    {
        public OpexAbcExemption OpexAbcExemption { get; set; }
        public CostCentre CostCentre { get; set; }
    }
}