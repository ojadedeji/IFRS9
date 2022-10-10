using Fintrak.Shared.SystemCore.Entities;
using System;
using System.Linq;

namespace Fintrak.Data.SystemCore.Contracts
{
    public class RoleInfo
    {
        public Role Role { get; set; }
        public Solution Solution { get; set; }
    }
}