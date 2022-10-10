using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class StaffCostInfo
    {
        public StaffCost StaffCost { get; set; }
        public CostCentre CostCentre { get; set; }

    }
}