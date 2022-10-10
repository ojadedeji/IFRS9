using Fintrak.Shared.SystemCore.Entities;
using System;
using System.Linq;

namespace Fintrak.Data.SystemCore.Contracts
{
    public class UserRoleInfo
    {
        public UserRole UserRole { get; set; }
        public UserSetup UserSetup { get; set; }
        public Role Role { get; set; }
        public Solution Solution { get; set; }
    }
}