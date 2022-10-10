using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Core.Contracts
{
    public interface IDataExportRoleRepository : IDataRepository<DataExportRole>
    {
        IEnumerable<DataExportRoleInfo> GetDataExportRoles();
        IEnumerable<DataExportRoleInfo> GetDataExportRoleByDataExport(int uploadId);
    }
}
