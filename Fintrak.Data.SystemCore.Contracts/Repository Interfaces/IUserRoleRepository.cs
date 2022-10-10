
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.SystemCore.Entities;

namespace Fintrak.Data.SystemCore.Contracts
{
    public interface IUserRoleRepository : IDataRepository<UserRole>
    {
        IEnumerable<UserRoleInfo> GetUserRoleInfo(string solutionName, string loginID, List<string> groupNames);
        IEnumerable<UserRoleInfo> GetUserRoleInfo();
    }
}
