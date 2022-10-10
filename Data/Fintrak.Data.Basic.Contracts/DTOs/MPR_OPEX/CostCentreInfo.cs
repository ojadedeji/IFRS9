using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class CostCentreInfo
    {
        public CostCentre CostCentre { get; set; }
        public CostCentreDefinition CostCentreDefinition { get; set; }
        public CostCentre Parent { get; set; }

    }
}