using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Fintrak.Data.Core.Contracts
{
    public interface IDataExportRepository : IDataRepository<DataExport>
    {
        void DataExport(string actionName, SqlParameter[] parameters);

    }
}

