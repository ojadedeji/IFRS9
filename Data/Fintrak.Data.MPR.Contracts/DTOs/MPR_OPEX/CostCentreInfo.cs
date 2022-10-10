using System;
using System.Linq;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class CostCentreInfo
    {
        public CostCentre CostCentre { get; set; }
        public CostCentreDefinition CostCentreDefinition { get; set; }
        public CostCentre Parent { get; set; }

    }
}