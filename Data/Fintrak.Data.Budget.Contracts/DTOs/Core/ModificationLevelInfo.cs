using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class ModificationLevelInfo
    {
        public ModificationLevel ModificationLevel { get; set; }
        public Module Module { get; set; }
        public TeamDefinition TeamDefinition { get; set; }
    }
}