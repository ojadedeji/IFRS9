using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class TeamPayClassificationInfo
    {
        public TeamPayClassification TeamPayClassification { get; set; }
        public TeamDefinition TeamDefinition { get; set; }
        public Team Team { get; set; }
        public PayClassification PayClassification { get; set; }
    }
}