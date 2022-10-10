using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class FeeGroupEntryInfo
    {
        public FeeGroupEntry FeeGroupEntry { get; set; }
        public FeeGroup FeeGroup { get; set; }
        public TeamDefinition TeamDefinition { get; set; }
        public Team Team { get; set; }
    }
}