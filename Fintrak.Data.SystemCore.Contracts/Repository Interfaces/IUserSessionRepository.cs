
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.SystemCore.Entities;
using Fintrak.Shared.SystemCore.Framework;

namespace Fintrak.Data.SystemCore.Contracts
{
    public interface IUserSessionRepository : IDataRepository<UserSession>
    {
        IEnumerable<UserSessionInfo> GetUserSessions();
    }
}
