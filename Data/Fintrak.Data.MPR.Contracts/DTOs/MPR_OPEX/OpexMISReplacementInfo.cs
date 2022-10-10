using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class OpexMISReplacementInfo
    {
        public OpexMISReplacement OpexMISReplacement { get; set; }
        public CostCentre CostCentre { get; set; }

    }
}