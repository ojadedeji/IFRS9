using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class OpexManagementTreeInfo
    {
        public OpexManagementTree OpexManagementTree { get; set; }
        public CostCentre CostCentre { get; set; }
        public TeamDefinition TeamDefinition { get; set; }
        public Team Team { get; set; }
        public TeamDefinition AccountOfficerDefinition { get; set; }
        public Team AccountOfficerMis { get; set; }
        public SetUp SetUp { get; set; }


    }
}