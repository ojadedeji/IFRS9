using System;
using System.Linq;
using Fintrak.Shared.SystemCore.Entities;

namespace Fintrak.Data.SystemCore.Contracts
{
    public class MenuRoleInfo
    {
        public MenuRole MenuRole { get; set; }
        public Menu Menu { get; set; }
        public Role Role { get; set; }
        public Solution Solution { get; set; }
    }
}