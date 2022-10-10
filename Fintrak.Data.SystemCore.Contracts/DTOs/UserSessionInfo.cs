using System;
using System.Linq;
using Fintrak.Shared.SystemCore.Entities;

namespace Fintrak.Data.SystemCore.Contracts
{
    public class UserSessionInfo

    {
        public UserSession UserSession { get; set; }
        public UserSetup UserSetup { get; set; }
        public Database Database { get; set; }
    }
}