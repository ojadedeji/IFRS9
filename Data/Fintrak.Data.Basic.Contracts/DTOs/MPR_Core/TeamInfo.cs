using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class TeamInfo
    {
        public Team Team { get; set; }
        public Team Parent { get; set; }
    }
}