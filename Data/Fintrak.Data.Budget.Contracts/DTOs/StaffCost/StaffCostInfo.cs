using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class StaffCostInfo
    {
        public StaffCost StaffCost { get; set; }
        public TeamDefinition TeamDefinition { get; set; }
        public Team Team { get; set; }
        public Grade Grade { get; set; }
        public PayClassification PayClassification { get; set; }
    }
}