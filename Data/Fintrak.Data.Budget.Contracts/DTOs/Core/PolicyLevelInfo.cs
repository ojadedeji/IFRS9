using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class PolicyLevelInfo
    {
        public PolicyLevel PolicyLevel { get; set; }
        public Policy Policy { get; set; }
        public Module Module { get; set; }
        public TeamDefinition TeamDefinition { get; set; }
    }
}