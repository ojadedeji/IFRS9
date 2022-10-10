using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class SecondaryLockLevelInfo
    {
        public SecondaryLockLevel SecondaryLockLevel { get; set; }
        public Module Module { get; set; }
        public TeamDefinition TeamDefinition { get; set; }
    }
}