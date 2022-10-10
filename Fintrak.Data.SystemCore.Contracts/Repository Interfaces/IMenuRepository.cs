
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.SystemCore.Entities;

namespace Fintrak.Data.SystemCore.Contracts
{
    public interface IMenuRepository : IDataRepository<Menu>
    {
        IEnumerable<MenuInfo> GetMenuInfoByLoginID(string loginID);
        IEnumerable<MenuInfo> GetMenuInfo();
    }
}
