using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class PayStructureInfo
    {
        public PayStructure PayStructure { get; set; }
        public Grade Grade { get; set; }
        public PayClassification PayClassification { get; set; }
    }
}