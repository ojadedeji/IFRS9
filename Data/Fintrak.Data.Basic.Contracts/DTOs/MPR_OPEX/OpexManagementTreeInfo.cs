using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class OpexManagementTreeInfo
    {
        public OpexManagementTree OpexManagementTree { get; set; }
        public CostCentre CostCentre { get; set; }
        public TeamDefinition TeamDefinition { get; set; }
        public Team Team { get; set; }
        public TeamDefinition AccountOfficerDefinition { get; set; }
        public Team AccountOfficerMis { get; set; }

    }
}