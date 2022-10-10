using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class TeamInfo
    {
        public Team Team { get; set; }
        public Team Parent { get; set; }
    }
}