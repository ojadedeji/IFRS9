using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class BudgetingLevelInfo
    {
        public BudgetingLevel BudgetingLevel { get; set; }
        public Module Module { get; set; }
        public TeamDefinition TeamDefinition { get; set; }
    }
}