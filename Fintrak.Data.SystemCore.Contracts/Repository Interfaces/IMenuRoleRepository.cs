
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.SystemCore.Entities;

namespace Fintrak.Data.SystemCore.Contracts
{
    public interface IMenuRoleRepository : IDataRepository<MenuRole>
    {
        IEnumerable<MenuRoleInfo> GetMenuRoleInfo();

        IEnumerable<MenuRoleInfo> GetMenuRoleInfo(string loginID);
    }
}
