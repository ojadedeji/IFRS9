using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class TeamInfo
    {
        public Team Team { get; set; }
        public TeamDefinition TeamDefinition { get; set; }
        public Team Parent { get; set; }
        public TeamDefinition ParentDefinition { get; set; }
    }
}