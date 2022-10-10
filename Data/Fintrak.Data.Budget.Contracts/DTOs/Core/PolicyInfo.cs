using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class PolicyInfo
    {
        public Policy Policy { get; set; }
        public Module Module { get; set; }
    }
}