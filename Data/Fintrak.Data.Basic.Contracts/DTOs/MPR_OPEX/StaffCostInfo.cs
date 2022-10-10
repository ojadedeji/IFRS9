using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class StaffCostInfo
    {
        public StaffCost StaffCost { get; set; }
        public CostCentre CostCentre { get; set; }

    }
}